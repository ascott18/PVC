using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Combat;

namespace Project.Shared.Items
{
	public partial class CombatArena : UserControl
	{
		private Party party;
		private MonsterPack monsterPack;

		private readonly CombatSpriteContainer[] heroContainers = new CombatSpriteContainer[Party.MaxHeroes];
		private readonly CombatSpriteContainer[] monsterContainers = new CombatSpriteContainer[MonsterPack.MaxMonsters];

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

		internal MonsterPack MonsterPack
		{
			get { return monsterPack; }
			set { monsterPack = value; }
		}

		internal Party Party
		{
			get { return party; }
			set
			{
				party = value;
				for (int i = 0; i < heroContainers.Length; i++)
				{
					if (party.Heroes.Count > i)
						heroContainers[i].Sprite = party.Heroes[i];
					else
						heroContainers[i].Sprite = null;
				}
			}
		}
	}
}
