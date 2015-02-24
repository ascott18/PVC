using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
			MainWindow.Window.Show();
		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
