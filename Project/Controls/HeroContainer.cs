using System.Windows.Forms;

namespace Project.Controls
{
	internal partial class HeroContainer : CombatSpriteContainer
	{
		public HeroContainer()
		{
			InitializeComponent();

			InitializeSpellContainers(12);

			AttributesContainer.Dock = DockStyle.Right;
		}
	}
}
