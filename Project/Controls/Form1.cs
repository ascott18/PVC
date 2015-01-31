using System.ComponentModel;
using System.Windows.Forms;

namespace Project.Controls
{
	public partial class MainWindow : Form
	{
		internal Game Game { get; private set; }

		// Implements Singleton.
		private static MainWindow instance;
		public static MainWindow Window
		{
			get
			{
				if (instance != null)
					return instance;
				return instance = new MainWindow();
			}
		}

		private MainWindow()
		{
			InitializeComponent();

			Game = new Game(this);
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return Game.ProcessKey(keyData) || base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
