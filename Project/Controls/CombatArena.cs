using System;
using System.Drawing;
using System.Linq;
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
					spellContainer.MouseClick += spellContainer_MouseClick;
					spellContainer.MouseDoubleClick += spellContainer_MouseDoubleClick;
					
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

		internal CombatSession CombatSession
		{
			get { return combatSession; }
			set
			{
				if (combatSession != null)
				{
					// Stop old session if there was one
					PopulateContainers(heroContainers, null);
					PopulateContainers(monsterContainers, null);
					combatSession.TargetsChanged -= combatSession_TargetsChanged;
					combatSession.EndCombat(false);
				}

				combatSession = value;
				if (combatSession == null)
				{
					// nothing for now.
				}
				else
				{
					// Wire in the new session.
					PopulateContainers(heroContainers, combatSession.Party);
					PopulateContainers(monsterContainers, combatSession.MonsterPack);
					combatSession.TargetsChanged += combatSession_TargetsChanged;
				}
			}
		}

		void combatSession_TargetsChanged(CombatSession sender)
		{
			// Update the portraits that show each unit's target.
			var containers = Controls.OfType<CombatSpriteContainer>();
			foreach (var container in containers)
			{
				var sprite = container.Sprite;
				if (sprite == null) continue;

				var target = CombatSession.GetTarget(sprite);

				container.targetImage.Image = target != null ? target.Image : null;
			}
		}


		private void spellContainer_MouseClick(object sender, MouseEventArgs e)
		{
			var container = sender as SpellContainer;
			if (container == null) throw new InvalidCastException("sender was not a SpellContainer");

			switch (e.Button)
			{
				case MouseButtons.Left:
					// Queue the spell to be cast.
					if (!container.Spell.IsAutoCast) // don't queue spells that are autocasting.
						CombatSession.QueueSpell(container.Spell);
					break;
				case MouseButtons.Right:
					// Toggle autocast.
					container.Spell.IsAutoCast = !container.Spell.IsAutoCast;
					break;
			}
		}

		private void spellContainer_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var container = sender as SpellContainer;
			if (container == null) throw new InvalidCastException("sender was not a SpellContainer");

			CombatSession.CastSpellImmediately(container.Spell);
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

		private void retreatButton_Click(object sender, EventArgs e)
		{
			combatSession.EndCombat(false);
		}
	}
}
