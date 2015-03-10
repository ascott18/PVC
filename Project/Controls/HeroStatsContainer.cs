using System;
using System.Linq;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	public partial class HeroStatsContainer : UserControl
	{
		private Hero hero;

		public HeroStatsContainer()
		{
			InitializeComponent();
		}

		internal Hero Hero
		{
			get { return hero; }
			set
			{
				if (hero != null)
				{
					csac.Sprite = null;
					hero.AttributesChanged -= HeroAttributesChanged;
				}

				hero = value;
				if (hero == null)
				{
					Hide();
				}
				else
				{
					Show();
					csac.Sprite = hero;
					hero.AttributesChanged += HeroAttributesChanged;
					HeroAttributesChanged(hero);
				}
			}
		}

		private void HeroAttributesChanged(CombatSprite sender)
		{
			statsLabel.Text = String.Format("Stamina: {0}\nBlock: {1}\nCombo: {2}",
			                                sender.Attributes.Stamina,
			                                sender.Attributes.Block,
			                                sender.Attributes.Combo
				);
		}
	}
}
