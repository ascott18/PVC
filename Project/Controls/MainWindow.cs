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
			// We must set the instance inside the constructor so that
			// if the singleton object is accessed before the constructor
			// completes, we won't stack overflow (the variable won't normally
			// be set until after the constructor is done).
			instance = this;

			InitializeComponent();

			Game = new Game();
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return Game.ProcessKey(keyData) || base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
