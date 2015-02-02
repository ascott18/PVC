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
	class CombatSession
	{
		/// <summary>
		/// Determines the amount of time that the combat loop will sleep for
		/// before running again. Prevents unnecessary CPU churn.
		/// </summary>
		const int UpdateFrequency = 10;

		/// <summary>
		/// The Party participating in this combat.
		/// </summary>
		public readonly Party Party;

		/// <summary>
		/// The MonsterPack participating in this combat.
		/// </summary>
		public readonly MonsterPack MonsterPack;

		/// <summary>
		/// A collection of all the CombatSprites from both the Party and the MonsterPack.
		/// </summary>
		private readonly IEnumerable<CombatSprite> allSprites;

		/// <summary>
		/// Maps each CombatSprite to the sprite that it is currently targeting.
		/// </summary>
		private readonly Dictionary<CombatSprite, CombatSprite> targets = new Dictionary<CombatSprite, CombatSprite>(); 

		public CombatSession(Party party, MonsterPack monsterPack)
		{
			State = CombatState.New;
			
			Party = party;
			MonsterPack = monsterPack;

			allSprites = Party.Members.Concat(MonsterPack.Members);
		}

		private readonly Stopwatch gameTimer = new Stopwatch();
		private Thread combatLoop;

		public event CombatEvent StateChanged;

		public CombatState State { get; private set; }

		public enum CombatState
		{
			New,
			Acitve,
			Paused,
			Ended
		}

		public void StartCombat()
		{
			if (State != CombatState.New)
				throw new InvalidOperationException("Can't start an already-started combat session.");

			AutoAcquireTargets();
			gameTimer.Restart();

			// Start the thread for the game loop.
			// This will drive the update cycles for checking spell cast completion, etc.
			combatLoop = new Thread(CombatLoop);
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

				// We wait here so we aren't updating any more than we need to.
				var elapsed = timer.ElapsedMilliseconds - start;
				var wait = UpdateFrequency - (int)elapsed;
				if (wait > 0)
					Thread.Sleep(wait);
			}
		}

		private void sprite_HealthChanged(CombatSprite sender)
		{
			if (!Party.Members.Any(sprite => sprite.IsActive))
				EndCombat();

			if (!MonsterPack.Members.Any(sprite => sprite.IsActive))
				EndCombat();


		}

		public void PauseCombat()
		{
			gameTimer.Stop();

			State = CombatState.Paused;

			if (StateChanged != null) StateChanged(this);
		}

		public void ResumeCombat()
		{
			if (State != CombatState.Paused)
				return;

			gameTimer.Start();

			State = CombatState.Acitve;

			if (StateChanged != null) StateChanged(this);
		}

		public void EndCombat()
		{
			Debug.WriteLine("Combat Ended");

			if (State != CombatState.Acitve && State != CombatState.Paused)
				return;

			gameTimer.Stop();

			State = CombatState.Ended;

			//combatLoop.Join();

			if (StateChanged != null) StateChanged(this);
		}


		public double GetTime()
		{
			return (double)gameTimer.ElapsedMilliseconds / 1000;
		}

		public void AutoAcquireTargets()
		{
			foreach (var sprite in allSprites)
			{
				AutoAcquireTarget(sprite);
			}
		}

		
		public CombatSprite AutoAcquireTarget(CombatSprite sprite)
		{
			if (MonsterPack.Members.Contains(sprite))
				return AutoAcquireTarget(sprite, MonsterPack, Party);

			if (Party.Members.Contains(sprite))
				return AutoAcquireTarget(sprite, Party, MonsterPack);

			throw new Exception("Couldn't find the sprite in either group of combatants.");
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
					var targetIndex = index % aliveEnemies.Count();
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

			for (int i = 0; i < spellQueue.Count; )
			{
				var spell = spellQueue[i];

				if (spell.CanCast && spell.Start(this))
					spellQueue.RemoveAt(i);
				else
					i++;
			}

			foreach (var sprite in allSprites)
			{
				foreach (var spell in sprite.Spells)
				{
					if (spell.IsAutoCast)
						spell.Start(this);
				}
			}

			foreach (var monster in MonsterPack.Members)
			{
				(monster as Monster).DoAction(this);
			}
		}

		public event CombatEvent Update;


		private readonly List<Spell> spellQueue = new List<Spell>(); 
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

		public void CastSpellImmediately(Spell spell)
		{
			if (!spell.CanCast)
				return;

			var owner = spell.Owner;
			if (owner.CurrentCast != null)
				owner.CurrentCast.Cancel();

			spell.Start(this);
		}

		public IEnumerable<Spell> GetQueuedSpells(CombatSprite owner)
		{
			return spellQueue.Where(spell => spell.Owner == owner);
		}
	}

	internal delegate void CombatEvent(CombatSession sender);
}
