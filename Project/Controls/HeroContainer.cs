using System.Drawing;
using System.Windows.Forms;

namespace Project.Controls
{
	internal partial class HeroContainer : CombatSpriteContainer
	{
		public HeroContainer()
		{
			InitializeComponent();

			InitializeSpellContainers(12);

			targetImage.Location = new Point(185, 13);

			AttributesContainer.Dock = DockStyle.Right;
		}
	}
}
