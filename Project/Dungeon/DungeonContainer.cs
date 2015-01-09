using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
	public partial class DungeonContainer : UserControl
	{
		private DungeonMap currentMap;

		public DungeonContainer()
		{
			InitializeComponent();
		}

		DungeonMap CurrentMap
		{
			get { return currentMap; }
		}

		private void DungeonContainer_Paint(object sender, PaintEventArgs e)
		{
			currentMap.Draw(e.Graphics);
		}
	}
}
