using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Spells;

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
