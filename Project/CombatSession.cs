﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
		private readonly IEnumerable<CombatSprite> AllSprites;

		/// <summary>
		/// Maps each CombatSprite to the sprite that it is currently targeting.
		/// </summary>
		private readonly Dictionary<CombatSprite, CombatSprite> Targets = new Dictionary<CombatSprite, CombatSprite>(); 

		public CombatSession(Game game, Party party, MonsterPack monsterPack)
		{
			Game = game;
			State = CombatState.New;
			
			Party = party;
			MonsterPack = monsterPack;

			AllSprites = Party.Members.Concat(MonsterPack.Members);
		}

		private readonly Game Game;
		private readonly Stopwatch _gameTimer = new Stopwatch();
		private Thread _combatLoop;

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
			_gameTimer.Restart();

			// Start the thread for the game loop.
			// This will drive the update cycles for checking spell cast completion, etc.
			_combatLoop = new Thread(CombatLoop);
			_combatLoop.Start(this);

			foreach (var sprite in AllSprites)
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
					if (Game.Window.IsDisposed)
						return;

					// We must call Invoke on something from the main thread.
					Game.Window.Invoke(handler);
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
			if (Party.Members.Cast<Hero>().All(sprite => sprite.IsRetreated))
			{
				EndCombat();
			}
			if (MonsterPack.Members.All(sprite => sprite.IsDead))
			{
				EndCombat();
			}


		}

		public void PauseCombat()
		{
			_gameTimer.Stop();

			State = CombatState.Paused;

			if (StateChanged != null) StateChanged(this);
		}

		public void ResumeCombat()
		{
			if (State != CombatState.Paused)
				return;

			_gameTimer.Start();

			State = CombatState.Acitve;

			if (StateChanged != null) StateChanged(this);
		}

		public void EndCombat()
		{
			Debug.WriteLine("Combat Ended");

			if (State != CombatState.Acitve && State != CombatState.Paused)
				return;

			_gameTimer.Stop();

			State = CombatState.Ended;

			_combatLoop.Join();

			if (StateChanged != null) StateChanged(this);
		}


		public double GetTime()
		{
			return (double)_gameTimer.ElapsedMilliseconds / 1000;
		}

		public void AutoAcquireTargets()
		{
			foreach (var sprite in AllSprites)
			{
				AutoAcquireTarget(sprite);
			}
		}

		
		public CombatSprite AutoAcquireTarget(CombatSprite sprite)
		{
			if (sprite is Monster)
				return AutoAcquireTarget(sprite, MonsterPack, Party);
			if (sprite is Hero)
				return AutoAcquireTarget(sprite, Party, MonsterPack);

			return null;
		}

		private CombatSprite AutoAcquireTarget(CombatSprite sprite, DungeonSprite allies, DungeonSprite enemies)
		{
			CombatSprite target;
			Targets.TryGetValue(sprite, out target);

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

				Targets[sprite] = target;
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

			foreach (var monster in MonsterPack.Members)
			{
				(monster as Monster).DoAction(this);
			}
		}

		public event CombatEvent Update;
	}

	internal delegate void CombatEvent(CombatSession sender);
}
