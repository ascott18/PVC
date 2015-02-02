using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class CombatSpriteContainer : UserControl
	{
		protected readonly List<SpellContainer> SpellContainers = new List<SpellContainer>();
		private CombatSprite sprite;

		internal CombatSprite Sprite
		{
			get { return sprite; }
			set
			{
				if (sprite != null)
				{
					foreach (var spellContainer in SpellContainers)
						spellContainer.Spell = null;
				}

				sprite = value;
				if (sprite == null)
				{
					Hide();
				}
				else
				{
					Show();
					AttributesContainer.Sprite = sprite;

					for (int i = 0; i < Math.Min(SpellContainers.Count, sprite.Spells.Count); i++)
					{
						var spellContainer = SpellContainers[i];

						spellContainer.Spell = sprite.Spells[i];
					}
				}
			}
		}


		public CombatSpriteContainer()
		{
			InitializeComponent();
		}

		protected void InitializeSpellContainers(int x)
		{
			for (int i = 0; i < 3; i++)
			{
				var container = new SpellContainer
				{
					Location = new Point(x, 13 + (i * 17)),
					Name = "spellContainer" + i,
				};

				Controls.Add(container);
				SpellContainers.Add(container);
			}
			AttributesContainer.SendToBack();
		}
	}
}
