using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Combat;

namespace Project
{
	public partial class CombatArena : UserControl
	{
		private readonly CombatSpriteContainer[] heroContainers = new CombatSpriteContainer[Party.MaxHeroes];
		private readonly CombatSpriteContainer[] monsterContainers = new CombatSpriteContainer[MonsterPack.MaxMonsters];
		private CombatSession _combatSession;

		public CombatArena()
		{
			InitializeComponent();

			// Build containers for each type.
			const int padding = 5;

			for (int i = 0; i < heroContainers.Length; i++)
			{
				var container = heroContainers[i] = new CombatSpriteContainer();
				container.Location = new Point(padding, padding + i * container.Size.Height);
			}
			Controls.AddRange(heroContainers);

			for (int i = 0; i < monsterContainers.Length; i++)
			{
				var container = monsterContainers[i] = new CombatSpriteContainer();
				container.Location = new Point(Size.Width - padding - container.Size.Width, padding + i * container.Size.Height);
			}
			Controls.AddRange(monsterContainers);
		}

		internal CombatSession CombatSession
		{
			get { return _combatSession; }
			set
			{
				// Stop old session if there was one
				PopulateContainers(heroContainers, null);
				PopulateContainers(monsterContainers, null);
				if (_combatSession != null)
					_combatSession.PauseCombat();

				// Wire in the new session.
				_combatSession = value;
				PopulateContainers(heroContainers, _combatSession.Party);
				PopulateContainers(monsterContainers, _combatSession.MonsterPack);
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
