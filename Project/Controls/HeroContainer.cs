using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project.Controls
{
	internal partial class HeroContainer : CombatSpriteContainer
	{
		public HeroContainer()
		{
			InitializeComponent();

			InitializeSpellContainers(12);

			targetImage.Location = new Point(196, 13);

			AttributesContainer.Dock = DockStyle.Right;
		}
	}
}
