using System.Drawing;
using System.Windows.Forms;

namespace Project.Controls
{
	internal partial class MonsterContainer : CombatSpriteContainer
	{
		public MonsterContainer()
		{
			InitializeComponent();

			InitializeSpellContainers(77);

			targetImage.Location = new Point(0, 13);

			AttributesContainer.Dock = DockStyle.Left;
		}
	}
}
