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
	/// <summary>
	/// DungeonContainer is the UserControl that contains the dungeon itself.
	/// Attach a DungeonMap to have the DungeonContainer display that map.
	/// </summary>
	public partial class DungeonContainer : UserControl
	{
		private DungeonMap currentMap;

		public DungeonContainer()
		{
			InitializeComponent();
			CurrentMap = new DungeonMap(1); //TODO: Temporary, for testing
		}

		DungeonMap CurrentMap
		{
			get { return currentMap; }
			set { currentMap = value; }
		}

		private void DungeonContainer_Paint(object sender, PaintEventArgs e)
		{
			currentMap.Draw(e.Graphics);
		}
	}
}
