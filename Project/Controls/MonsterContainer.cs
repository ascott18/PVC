using System.Windows.Forms;

namespace Project.Controls
{
	internal partial class MonsterContainer : CombatSpriteContainer
	{
		public MonsterContainer()
		{
			InitializeComponent();

			InitializeSpellContainers(77);

			AttributesContainer.Dock = DockStyle.Left;
		}
	}
}
