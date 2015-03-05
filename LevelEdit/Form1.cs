using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project;
using Project.Controls;
using Project.Dungeon;

namespace LevelEdit
{
	public partial class Form1 : Form
	{
		private DungeonMap map;
		public Form1()
		{
			InitializeComponent();

			DungeonContainer container = new DungeonContainer();
			container.Location = new Point(0, 0);
			container.Paint += container_Paint;
			Controls.Add(container);

			map = new DungeonMap(1, null);
		}

		void container_Paint(object sender, PaintEventArgs e)
		{
			if (map != null)
				map.Draw(e.Graphics);
		}
	}
}
