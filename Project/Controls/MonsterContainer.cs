using System;
using System.Drawing;
using System.Linq;
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
