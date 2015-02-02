using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
