using System;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	public partial class CombatSpriteAttributesContainer : UserControl
	{
		private CombatSprite sprite;

		public CombatSpriteAttributesContainer()
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
					// nothing
				}
				else
				{
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
