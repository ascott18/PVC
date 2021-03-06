﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Project.Controls;
using Project.Spells;
using Project.Sprites;

namespace Project
{
	/// <summary>
	///     CombatSession represents a single fight between one Party and one MonsterPack.
	///     It holds state information that is specific to the battle, like targeting,
	///     timing control, and spell queueing. It acts as the controller for the Monster "AI",
	///     and for performing the actual casts of queued spells and autocasting spells for Heroes.
	/// </summary>
	public class CombatSession
	{
		public enum CombatState
		{
			/// <summary>
			///     The CombatSession has not yet started.
			/// </summary>
			New,

			/// <summary>
			///     The CombatSession is in progress.
			/// </summary>
			Active,

			/// <summary>
			///     The CombatSessions is paused, but not ended.
			/// </summary>
			Paused,

			/// <summary>
			///     Combat has concluded.
			/// </summary>
			Ended
		}

		/// <summary>
		///     Determines the amount of time that the combat loop will sleep for
		///     before running again. Prevents unnecessary CPU churn.
		/// </summary>
		private const int UpdateFrequency = 16;

		/// <summary>
		///     The MonsterPack participating in this combat.
		/// </summary>
		public readonly MonsterPack MonsterPack;

		/// <summary>
		///     The Party participating in this combat.
		/// </summary>
		public readonly Party Party;

		/// <summary>
		///     A collection of all the CombatSprites from both the Party and the MonsterPack.
		/// </summary>
		private readonly IEnumerable<CombatSprite> allSprites;

		private readonly Stopwatch gameTimer = new Stopwatch();
		private readonly List<Spell> spellQueue = new List<Spell>();

		/// <summary>
		///     Maps each CombatSprite to the sprite that it is currently targeting.
		/// </summary>
		private readonly Dictionary<CombatSprite, CombatSprite> targets = new Dictionary<CombatSprite, CombatSprite>();

		private Thread combatLoop;

		/// <summary>
		///     Creates a new CombatSession with the given Party and MonsterPack as combatants.
		/// </summary>
		/// <param name="party">The Party that is fighting</param>
		/// <param name="monsterPack">The MonsterPack being fought.</param>
		public CombatSession(Party party, MonsterPack monsterPack)
		{
			State = CombatState.New;

			Party = party;
			MonsterPack = monsterPack;

			allSprites = Party.Members.Concat(MonsterPack.Members);
		}

		/// <summary>
		///     The state of the CombatSession.
		/// </summary>
		public CombatState State { get; private set; }

		/// <summary>
		///     Gets the winner of this CombatSession.
		///     Will be null until the session is ended.
		/// </summary>
		public DungeonSprite Winner { get; private set; }

		/// <summary>
		///     Fires when the state of the CombatSession changes.
		/// </summary>
		public event CombatEvent StateChanged;

		/// <summary>
		///     Start the CombatSession, starting the game timer and initiating the combat loop.
		///     Can only ever be called once on a given CombatSession.
		/// </summary>
		public void StartCombat()
		{
			if (State != CombatState.New)
				throw new InvalidOperationException("Can't start an already-started combat session.");

			AutoAcquireTargets();
			gameTimer.Restart();

			// Start the thread for the game loop.
			// This will drive the update cycles for checking spell cast completion, etc.
			combatLoop = new Thread(CombatLoop);
			combatLoop.Name = "combatLoop";
			combatLoop.Start(this);

			foreach (var sprite in allSprites)
			{
				sprite.HealthChanged += sprite_HealthChanged;
			}

			State = CombatState.Active;

			if (StateChanged != null) StateChanged(this);
		}

		private void CombatLoop(object session)
		{
			var sess = session as CombatSession;
			var timer = new Stopwatch();
			timer.Start();

			while (sess.State != CombatState.Ended)
			{
				var start = timer.ElapsedMilliseconds;

				// Don't bother invoking if we're paused.
				// There is checking for this in the handler, too,
				// but check here as well so we don't waste time.
				if (sess.State == CombatState.Active)
				{
					// This throws errors if we try and invoke on the window
					// after it has been disposed (e.g. the user closed it).
					if (MainWindow.Window.IsDisposed)
						return;

					// We must call Invoke on something from the main thread.
					try
					{
						var handler = new MethodInvoker(() =>
						{
							try
							{
								CombatTimerOnTick();
							}
							catch (Exception ex)
							{
								if (Debugger.IsAttached)
									Debugger.Break();
								else
									throw ex;
							}
						});

						MainWindow.Window.Invoke(handler);
					}
					catch (ObjectDisposedException)
					{
						// If its already disposed, return and let the thread die.
						// Checking for Window.IsDisposed seems to accomplish nothing,
						// because this exception always still happens. An always-occuring race condition?
						return;
					}
				}

				// We sleep here so we aren't updating any more than we need to.
				var elapsed = timer.ElapsedMilliseconds - start;
				var wait = UpdateFrequency - (int)elapsed;
				if (wait > 0)
					Thread.Sleep(wait);
			}
		}

		private void sprite_HealthChanged(CombatSprite sender)
		{
			// If all party members are no longer active, the player lost.
			if (!Party.Members.Any(sprite => sprite.IsActive))
				EndCombat(false, false);

			// If all monsters are no longer active, the player won.
			if (!MonsterPack.Members.Any(sprite => sprite.IsActive))
				EndCombat(true, false);
		}

		/// <summary>
		///     Pause the CombatSession.
		/// </summary>
		public void PauseCombat()
		{
			gameTimer.Stop();

			State = CombatState.Paused;

			if (StateChanged != null) StateChanged(this);
		}

		/// <summary>
		///     Resume the CombatSession from a paused state.
		/// </summary>
		public void ResumeCombat()
		{
			if (State != CombatState.Paused)
				return;

			gameTimer.Start();

			State = CombatState.Active;

			if (StateChanged != null) StateChanged(this);
		}

		/// <summary>
		///     End the combat session.
		/// </summary>
		public void EndCombat(bool partyVictory, bool retreated)
		{
			Debug.WriteLine("Combat Ended");

			foreach (var sprite in allSprites)
			{
				sprite.HealthChanged -= sprite_HealthChanged;
			}

			if (State != CombatState.Active && State != CombatState.Paused)
				return;

			gameTimer.Stop();

			Winner = partyVictory ? (DungeonSprite)Party : MonsterPack;

			if (partyVictory)
			{
				var loot = MonsterPack.GetLoot().ToList();
				Party.AddInventoryItemRange(loot);

				var endingDialog = new CombatCompleteDialog(this);
				endingDialog.SetItems(loot);
				endingDialog.Owner = MainWindow.Window;
				endingDialog.ShowDialog();
			}
			else
			{
				foreach (var monster in MonsterPack)
					monster.Health = monster.MaxHealth;
			}

			if (!retreated)
			{
				// Restore 10% health to each hero if combat ended
				// naturally (user didnt click retreat).
				foreach (var hero in Party.Members.Cast<Hero>())
					hero.Health += hero.MaxHealth / 10;
			}


			State = CombatState.Ended;

			if (StateChanged != null) StateChanged(this);
		}

		/// <summary>
		///     Gets the current value of the game timer, in seconds.
		/// </summary>
		/// <returns>The current game time, in seconds.</returns>
		public double GetTime()
		{
			return (double)gameTimer.ElapsedMilliseconds / 1000;
		}

		/// <summary>
		///     Acquire a target for all actors in the CombatSession.
		/// </summary>
		public void AutoAcquireTargets()
		{
			foreach (var sprite in allSprites)
			{
				GetTarget(sprite);
			}
		}

		/// <summary>
		///     Attempt to acquire a target for the given CombatSprite.
		///     If the CombatSprite already has a valid target, that target is returned.
		///     Otherwise, a new target is chosen and returned.
		/// </summary>
		/// <param name="sprite">The sprite to acquire a target for.</param>
		/// <returns>The target of the given sprite. Returns null if there are no valid targets.</returns>
		public CombatSprite GetTarget(CombatSprite sprite)
		{
			if (MonsterPack.Contains(sprite))
				return GetTarget(sprite, MonsterPack, Party);

			if (Party.Contains(sprite))
				return GetTarget(sprite, Party, MonsterPack);

			throw new ArgumentException("Couldn't find the sprite in either group of combatants.", "sprite");
		}

		private CombatSprite GetTarget(CombatSprite sprite, DungeonSprite allies, DungeonSprite enemies)
		{
			CombatSprite target;
			targets.TryGetValue(sprite, out target);

			if (target == null || !target.IsActive)
			{
				var oldTarget = target;

				// We have to do IndexOf manually for IReadOnlyLists.
				var index = -1;
				for (int i = 0; i < allies.Count(); i++)
				{
					if (allies.Members[i] == sprite)
					{
						index = i;
						break;
					}
				}

				var aliveEnemies = enemies.Where(enemy => enemy.IsActive);

				if (!aliveEnemies.Any())
				{
					target = null;
				}
				else
				{
					var targetIndex = index % aliveEnemies.Count();
					target = aliveEnemies.ElementAt(targetIndex);
				}

				targets[sprite] = target;

				if (target != oldTarget && TargetsChanged != null)
					TargetsChanged(this);
			}

			return target;
		}

		/// <summary>
		///     Fires when the target of one of the combatants in this CombatSession changes.
		/// </summary>
		public event CombatEvent TargetsChanged;


		private void CombatTimerOnTick()
		{
			// Make sure the game in in progress before trying to update here.
			// Don't update if paused, or if ended.
			if (State != CombatState.Active)
				return;

			if (Update != null) Update(this);

			// Attempt to cast any queued spells.
			for (int i = 0; i < spellQueue.Count;)
			{
				var spell = spellQueue[i];

				if (spell.CanCast && spell.Start(this))
					spellQueue.RemoveAt(i);
				else
					i++;
			}

			// Attempt to cast any autocasting spells.
			foreach (var sprite in allSprites)
			{
				foreach (var spell in sprite.Spells)
				{
					if (spell.IsAutoCast)
						spell.Start(this);
				}
			}

			// Delegate out to each monster to let them decide what to do.
			foreach (var monster in MonsterPack)
			{
				monster.DoAction(this);
			}
		}

		/// <summary>
		///     Fires each time a game update is performed.
		///     This should happen approximately every CombatSession.UpdateFrequency milliseconds.
		/// </summary>
		public event CombatEvent Update;


		/// <summary>
		///     Queues a spell to be cast automatically.
		///     If the spell is already queued, it moves the spell to the top of the queue.
		/// </summary>
		/// <param name="spell">The spell to queue.</param>
		public void QueueSpell(Spell spell)
		{
			if (spellQueue.Contains(spell))
			{
				spellQueue.Remove(spell);
				spellQueue.Insert(0, spell);
			}
			else
			{
				spellQueue.Add(spell);
			}
		}

		/// <summary>
		///     Immediately casts a spell, canceling the spell owner's current cast
		///     and starting the given spell immediately, if possible.
		///     If the given spell can't be cast due to its cooldown, no action is taken.
		/// </summary>
		/// <param name="spell">The spell to cast.</param>
		public void CastSpellImmediately(Spell spell)
		{
			if (!spell.CanCast)
				return;

			var owner = spell.Owner;
			if (owner.CurrentCast != null)
				owner.CurrentCast.Cancel();

			spell.Start(this);
		}

		/// <summary>
		///     Returns all of the spells queued that are owned by a given CombatSprite,
		///     in the order that they are queued in.
		/// </summary>
		/// <param name="owner">The spell owner to query for.</param>
		/// <returns>An IEnumerable of spells queued for the given owner.</returns>
		public IEnumerable<Spell> GetQueuedSpells(CombatSprite owner)
		{
			return spellQueue.Where(spell => spell.Owner == owner);
		}
	}

	/// <summary>
	///     Represents a generic event for a CombatSession.
	/// </summary>
	/// <param name="sender">The CombatSession for which the event fired.</param>
	public delegate void CombatEvent(CombatSession sender);
}
