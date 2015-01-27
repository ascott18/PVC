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
				}
			}
		}
	}
}
