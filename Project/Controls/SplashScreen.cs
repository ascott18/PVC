using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	public partial class SplashScreen : Form
	{
		public SplashScreen()
		{
			InitializeComponent();
		}

		private void newGameButton_Click(object sender, EventArgs e)
		{
			Hide();
			MainWindow.Window.CreateGame();

			MainWindow.Window.Show();
		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void debugStart_Click(object sender, EventArgs e)
		{
			MainWindow.Window.IsDebug = true;
			Hide();

			Party p = new Party(new Point(7, 7));
			int mapID = Convert.ToInt32(debugStartMapID.Text);
			p.AddHero(new Hero(p, 1));
			p.AddHero(new Hero(p, 2));
			p.AddHero(new Hero(p, 3));
			MainWindow.Window.CreateGame(p, mapID);


			MainWindow.Window.Show();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.F10)
			{
				debugStart.Visible = true;
				debugStartMapID.Visible = true;
				debugStartMapID.Focus();
				AcceptButton = debugStart;
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
