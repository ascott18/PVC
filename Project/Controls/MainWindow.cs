using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Project.Dungeon;
using Project.Sprites;
using Timer = System.Threading.Timer;

namespace Project.Controls
{
	internal partial class MainWindow : Form
	{

		public bool IsDebug;

		// Implements Singleton.
		private static MainWindow instance;

		private MainWindow()
		{
			// We must set the instance inside the constructor so that
			// if the singleton object is accessed before the constructor
			// completes, we won't stack overflow (the variable won't normally
			// be set until after the constructor is done).
			instance = this;

			InitializeComponent();


			dungeonTimer = new Timer(DungeonTimerCallback, null, 0, 10);
		}

		private void DungeonTimerCallback(object state)
		{
			var needRedraw = false;
			if (Game != null && Game.CurrentMap != null)
			{
				foreach (var tile in Game.CurrentMap.Tiles.Where(tile => tile.NeedsRedraw))
				{
					tile.NeedsRedraw = false;
					needRedraw = true;
				}
			}

			if (needRedraw)
				dungeonContainer.Invalidate();
		}

		public void CreateGame()
		{
			Game = new Game();
		}

		public void CreateGame(Party party, int mapID)
		{
			Game = new Game(party, mapID);
		}

		internal Game Game { get; private set; }

		public static MainWindow Window
		{
			get
			{
				if (instance != null)
					return instance;
				return instance = new MainWindow();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return Game.ProcessKey(keyData) || base.ProcessCmdKey(ref msg, keyData);
		}

		private InventoryScreen inventoryScreen;
		private StatsScreen statsScreen;
		private Timer dungeonTimer;

		private void inventoryButton_Click(object sender, EventArgs e)
		{
			var inv = inventoryScreen = new InventoryScreen();
			inv.Party = Game.Party;
			inv.StartPosition = FormStartPosition.Manual;
			inv.Location = Location + new Size(Width, 0);
			inv.Show(this);

			var stats = statsScreen = new StatsScreen();
			stats.Party = Game.Party;
			stats.StartPosition = FormStartPosition.Manual;
			stats.Location = Location + new Size(Width, inv.Height);
			stats.Show(this);
		}

		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

	}
}
