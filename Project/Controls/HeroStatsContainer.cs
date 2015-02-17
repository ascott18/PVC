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

		void HeroAttributesChanged(CombatSprite sender)
		{
			statsLabel.Text = String.Format("Stamina: {0}\nStrength: {1}\nIntellect: {2}\nDexterity: {3}",
				sender.Attributes.Stamina,
				sender.Attributes.Strength,
				sender.Attributes.Intellect,
				sender.Attributes.Dexterity
			);
		}
	}
}
