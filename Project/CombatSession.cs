using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
	class CombatSession
	{
		public readonly Party Party;
		public readonly MonsterPack MonsterPack;

		private readonly IEnumerable<CombatSprite> AllSprites;

		public readonly Dictionary<CombatSprite, CombatSprite> Targets = new Dictionary<CombatSprite, CombatSprite>(); 

		public CombatSession(Party party, MonsterPack monsterPack)
		{
			State = CombatState.New;
			
			Party = party;
			MonsterPack = monsterPack;

			AllSprites = Party.Members.Concat(MonsterPack.Members);
		}

		private readonly Stopwatch _gameTimer = new Stopwatch();
		private readonly Timer _combatTimer = new Timer();

		public event CombatEvent Started;
		public event CombatEvent Paused;
		public event CombatEvent Resumed;
		public event CombatEvent Ended;

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
			_combatTimer.Interval = 10;
			_combatTimer.Tick += CombatTimerOnTick;
			_combatTimer.Start();

			foreach (var sprite in AllSprites)
			{
				sprite.HealthChanged += sprite_HealthChanged;
			}

			State = CombatState.Acitve;

			if (Started != null) Started(this);
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
			_combatTimer.Stop();

			State = CombatState.Paused;

			if (Paused != null) Paused(this);
		}

		public void ResumeCombat()
		{
			if (State != CombatState.Paused)
				return;

			_gameTimer.Start();
			_combatTimer.Start();

			State = CombatState.Acitve;

			if (Resumed != null) Resumed(this);
		}

		public void EndCombat()
		{
			Debug.WriteLine("Combat Ended");

			if (State != CombatState.Acitve && State != CombatState.Paused)
				return;

			_gameTimer.Stop();
			_combatTimer.Stop();

			_combatTimer.Tick -= CombatTimerOnTick;

			State = CombatState.Ended;

			if (Ended != null) Ended(this);
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
