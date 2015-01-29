using System;
using System.Windows.Forms;

namespace Project.Combat
{
	public partial class CombatSpriteContainer : UserControl
	{
		private CombatSprite sprite;

		public CombatSpriteContainer()
		{
			InitializeComponent();
		}

		internal CombatSprite Sprite
		{
			get { return sprite; }
			set
			{
				if (sprite != null)
				{
					sprite.HealthChanged -= sprite_HealthChanged;
				}

				sprite = value;
				if (sprite == null)
				{
					Hide();
				}
				else
				{
					Show();
					image.Image = sprite.Image;
					nameText.Text = sprite.Name;
					sprite.HealthChanged += sprite_HealthChanged;
					sprite_HealthChanged(sprite);
				}
			}
		}

		void sprite_HealthChanged(CombatSprite sender)
		{
			healthText.Text = String.Format("{0}/{1}", sender.Health, sender.MaxHealth);
		}
	}
}
