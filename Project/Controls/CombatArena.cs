using System;
using System.Drawing;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	public partial class CombatArena : UserControl
	{
		private readonly HeroContainer[] heroContainers = new HeroContainer[Party.MaxHeroes];
		private readonly MonsterContainer[] monsterContainers = new MonsterContainer[MonsterPack.MaxMonsters];
		private CombatSession combatSession;

		public CombatArena()
		{
			InitializeComponent();

			// Build containers for each type.
			const int padding = 5;

			for (int i = 0; i < heroContainers.Length; i++)
			{
				var container = heroContainers[i] = new HeroContainer();
				container.Location = new Point(padding, padding + i * container.Size.Height);

				foreach (var spellContainer in container.SpellContainers)
				{
					spellContainer.Click += spellContainer_Click;
				}
			}
			Controls.AddRange(heroContainers);

			for (int i = 0; i < monsterContainers.Length; i++)
			{
				var container = monsterContainers[i] = new MonsterContainer();
				container.Location = new Point(Size.Width - padding - container.Size.Width, padding + i * container.Size.Height);
			}
			Controls.AddRange(monsterContainers);
		}

		void spellContainer_Click(object sender, System.EventArgs e)
		{
			var container = sender as SpellContainer;
			if (container == null) throw new InvalidCastException("sender was not SpellContainer");

			var parent = container.Parent as HeroContainer;
			if (parent == null) throw new ArgumentNullException("parent was not HeroContainer");

			container.Spell.Start(CombatSession, parent.Sprite);
		}

		internal CombatSession CombatSession
		{
			get { return combatSession; }
			set
			{
				// Stop old session if there was one
				PopulateContainers(heroContainers, null);
				PopulateContainers(monsterContainers, null);
				if (combatSession != null)
					combatSession.EndCombat();

				// Wire in the new session.
				combatSession = value;
				PopulateContainers(heroContainers, combatSession.Party);
				PopulateContainers(monsterContainers, combatSession.MonsterPack);
			}
		}

		private static void PopulateContainers(CombatSpriteContainer[] containers, DungeonSprite parent)
		{
			for (int i = 0; i < containers.Length; i++)
			{
				if (parent != null && parent.Members.Count > i)
					containers[i].Sprite = parent.Members[i];
				else
					containers[i].Sprite = null;
			}
		}
	}
}
