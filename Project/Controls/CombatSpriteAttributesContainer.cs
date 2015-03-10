using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Project.Dungeon;
using Project.Sprites;

namespace Project.Controls
{
	public partial class CombatSpriteAttributesContainer : UserControl
	{
		private static readonly ImageAttributes grayscaleAttributes;
		private CombatSprite sprite;

		static CombatSpriteAttributesContainer()
		{
			// From http://www.codeproject.com/Questions/147234/Converting-Bitmap-to-grayscale

			grayscaleAttributes = new ImageAttributes();
			var matrix = new ColorMatrix(
				new[]
				{
					new[]
					{
						.3f, .3f, .3f, 0, 0
					},
					new[]
					{
						.59f, .59f, .59f, 0, 0
					},
					new[]
					{
						.11f, .11f, .11f, 0, 0
					},
					new float[]
					{
						0, 0, 0, 1, 0
					},
					new float[]
					{
						0, 0, 0, 0, 1
					}
				});
			grayscaleAttributes.SetColorMatrix(matrix);
		}

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
					//image.Image = sprite.Image;
					nameText.Text = sprite.Name;
					sprite.HealthChanged += sprite_HealthChanged;
					sprite_HealthChanged(sprite);
				}
			}
		}

		private void sprite_HealthChanged(CombatSprite sender)
		{
			healthBar.Refresh();
		}

		private void healthBar_Paint(object sender, PaintEventArgs e)
		{
			if (sprite == null)
				return;

			var healthPct = ((double)sprite.Health / sprite.MaxHealth);
			var greenWidth = (int)(healthBar.Width * healthPct);
			var redWidth = healthBar.Width - greenWidth;

			e.Graphics.FillRectangle(new SolidBrush(Color.LightGreen),
			                         new Rectangle(0, 0, greenWidth, healthBar.Height));

			e.Graphics.FillRectangle(new SolidBrush(Color.LightCoral),
			                         new Rectangle(greenWidth, 0, redWidth, healthBar.Height));

			var format = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};

			e.Graphics.DrawString(sprite.Health.ToString(), Font, Brushes.Black, healthBar.ClientRectangle, format);
		}

		private void image_Paint(object sender, PaintEventArgs e)
		{
			if (sprite == null)
				return;

			if (Enabled)
				e.Graphics.DrawImage(sprite.Image, 0, 0, Tile.DimPixels, Tile.DimPixels);
			else
				e.Graphics.DrawImage(sprite.Image, new Rectangle(0, 0, 50, 50), 0, 0, Tile.DimPixels, Tile.DimPixels,
				                     GraphicsUnit.Pixel, grayscaleAttributes);
		}
	}
}
