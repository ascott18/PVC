﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
	class CombatSession
	{
		public readonly Party Party;
		public readonly MonsterPack MonsterPack;

		public readonly Dictionary<CombatSprite, CombatSprite> Targets = new Dictionary<CombatSprite, CombatSprite>(); 

		public CombatSession(Party party, MonsterPack monsterPack)
		{
			Party = party;
			MonsterPack = monsterPack;
		}

		private readonly Stopwatch _gameTimer = new Stopwatch();
		private readonly Timer _combatTimer = new Timer();

		public event CombatEvent Started;
		public event CombatEvent Paused;
		public event CombatEvent Resumed;
		public event CombatEvent Ended;
		public void StartCombat()
		{
			AutoAcquireTargets();
			_gameTimer.Restart();
			_combatTimer.Interval = 100;
			_combatTimer.Tick += CombatTimerOnTick;
			_combatTimer.Start();

			if (Started != null) Started(this);
		}

		public void PauseCombat()
		{
			_gameTimer.Stop();
			_combatTimer.Stop();

			if (Paused != null) Paused(this);
		}

		public void ResumeCombat()
		{
			_gameTimer.Start();
			_combatTimer.Start();

			if (Resumed != null) Resumed(this);
		}

		public void EndCombat()
		{
			_gameTimer.Stop();
			_combatTimer.Stop();
			
			if (Ended != null) Ended(this);
		}


		public double GetTime()
		{
			return (double)_gameTimer.ElapsedMilliseconds / 1000;
		}

		public void AutoAcquireTargets()
		{
			foreach (var sprite in Party.Members.Concat(MonsterPack.Members))
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

			if (target == null || target.Health == 0)
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

				var aliveEnemies = enemies.Members.Where(enemy => enemy.Health > 0);

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


		private void CombatTimerOnTick(object sender, EventArgs eventArgs)
		{
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
