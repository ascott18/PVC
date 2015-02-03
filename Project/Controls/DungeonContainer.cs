using System.Windows.Forms;

namespace Project.Controls
{
	/// <summary>
	///     DungeonContainer is the UserControl that contains the dungeon itself.
	/// </summary>
	public partial class DungeonContainer : UserControl
	{
		public DungeonContainer()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		private void DungeonContainer_Paint(object sender, PaintEventArgs e)
		{
			var parent = Parent as MainWindow;
			if (parent != null)
				parent.Game.CurrentMap.Draw(e.Graphics);
		}
	}
}
