using System;
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
	internal class CombatSession
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
			Acitve,

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
		private const int UpdateFrequency = 10;

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

			State = CombatState.Acitve;

			if (StateChanged != null) StateChanged(this);
		}

		private void CombatLoop(object session)
		{
			var sess = session as CombatSession;
			var timer = new Stopwatch();
			timer.Start();
			var handler = new MethodInvoker(CombatTimerOnTick);

			while (sess.State != CombatState.Ended)
			{
				var start = timer.ElapsedMilliseconds;

				// Don't bother invoking if we're paused.
				// There is checking for this in the handler, too,
				// but check here as well so we don't waste time.
				if (sess.State == CombatState.Acitve)
				{
					// This throws errors if we try and invoke on the window
					// after it has been disposed (e.g. the user closed it).
					if (MainWindow.Window.IsDisposed)
						return;

					// We must call Invoke on something from the main thread.
					try
					{
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
				var wait = UpdateFrequency - (int) elapsed;
				if (wait > 0)
					Thread.Sleep(wait);
			}
		}

		private void sprite_HealthChanged(CombatSprite sender)
		{
			// If all party members are no longer active, the player lost.
			if (!Party.Members.Any(sprite => sprite.IsActive))
				EndCombat();

			// If all monsters are no longer active, the player won.
			if (!MonsterPack.Members.Any(sprite => sprite.IsActive))
				EndCombat();

			//TODO: Pass something into EndCombat to signify the outcome.
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

			State = CombatState.Acitve;

			if (StateChanged != null) StateChanged(this);
		}

		/// <summary>
		///     End the combat session.
		/// </summary>
		public void EndCombat()
		{
			Debug.WriteLine("Combat Ended");

			if (State != CombatState.Acitve && State != CombatState.Paused)
				return;

			gameTimer.Stop();

			State = CombatState.Ended;

			if (StateChanged != null) StateChanged(this);
		}

		/// <summary>
		///     Gets the current value of the game timer, in seconds.
		/// </summary>
		/// <returns>The current game time, in seconds.</returns>
		public double GetTime()
		{
			return (double) gameTimer.ElapsedMilliseconds/1000;
		}

		/// <summary>
		///     Acquire a target for all actors in the CombatSession.
		/// </summary>
		public void AutoAcquireTargets()
		{
			foreach (var sprite in allSprites)
			{
				AutoAcquireTarget(sprite);
			}
		}


		/// <summary>
		///     Attempt to acquire a target for the given CombatSprite.
		///     If the CombatSprite already has a valid target, that target is returned.
		///     Otherwise, a new target is chosen and returned.
		/// </summary>
		/// <param name="sprite">The sprite to acquire a target for.</param>
		/// <returns>The target of the given sprite. Returns null if there are no valid targets.</returns>
		public CombatSprite AutoAcquireTarget(CombatSprite sprite)
		{
			if (MonsterPack.Members.Contains(sprite))
				return AutoAcquireTarget(sprite, MonsterPack, Party);

			if (Party.Members.Contains(sprite))
				return AutoAcquireTarget(sprite, Party, MonsterPack);

			throw new ArgumentException("Couldn't find the sprite in either group of combatants.", "sprite");
		}

		private CombatSprite AutoAcquireTarget(CombatSprite sprite, DungeonSprite allies, DungeonSprite enemies)
		{
			CombatSprite target;
			targets.TryGetValue(sprite, out target);

			if (target == null || !target.IsActive)
			{
				// We have to do IndexOf manually for IReadOnlyLists.
				var index = -1;
				for (int i = 0; i < allies.Members.Count; i++)
				{
					if (allies.Members[i] == sprite)
					{
						index = i;
						break;
					}
				}

				var aliveEnemies = enemies.Members.Where(enemy => enemy.IsActive);

				if (!aliveEnemies.Any())
				{
					target = null;
				}
				else
				{
					var targetIndex = index%aliveEnemies.Count();
					target = aliveEnemies.ElementAt(targetIndex);
				}

				targets[sprite] = target;
			}

			return target;
		}


		private void CombatTimerOnTick()
		{
			// Make sure the game in in progress before trying to update here.
			// Don't update if paused, or if ended.
			if (State != CombatState.Acitve)
				return;

			if (Update != null) Update(this);

			Debug.WriteLine(GetTime());

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
			foreach (var monster in MonsterPack.Members)
			{
				(monster as Monster).DoAction(this);
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
	internal delegate void CombatEvent(CombatSession sender);
}