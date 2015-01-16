using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
	public partial class MainWindow : Form
	{
		internal Game Game { get; private set; }
		
		public MainWindow()
		{
			InitializeComponent();

			Game = new Game(this);
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (Game.ProcessKey(keyData))
				return true;
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
