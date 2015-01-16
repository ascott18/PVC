using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Properties;

namespace Project
{
	class Game
	{
		public readonly MainWindow Window;

		public readonly Party Party;
		private readonly Dictionary<int, DungeonMap> maps = new Dictionary<int, DungeonMap>();

		internal DungeonMap CurrentMap { get; private set; }

		public void SetPartyLocation(int mapID, Point point)
		{
			DungeonMap map;

			if (!maps.TryGetValue(mapID, out map))
			{
				map = maps[mapID] = new DungeonMap(mapID);
			}

			Party.SetLocation(map.GetTile(point));
			CurrentMap = map;

		}

		internal Game(MainWindow window)
		{
			Window = window;

			// TODO: Temp code
			Party = new Party(new Point(7, 7));
			Party.AddHero(new Hero() { Image = Resources.milton });

			SetPartyLocation(1, Party.InitialLocation);
		}

		public bool ProcessKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Up:
				case Keys.W:
					TryMoveParty(new Size(0, -1));
					return true;
				case Keys.Down:
				case Keys.S:
					TryMoveParty(new Size(0, 1));
					return true;
				case Keys.Left:
				case Keys.A:
					TryMoveParty(new Size(-1, 0));
					return true;
				case Keys.Right:
				case Keys.D:
					TryMoveParty(new Size(1, 0));
					return true;
			}

			return false;
		}

		private void TryMoveParty(Size offset)
		{
			Point location = Party.CurrentTile.Location + offset;
			Tile destination = CurrentMap.GetTile(location);

			if (destination == null)
				return;

			if (destination.CanBeOccupied())
				Party.SetLocation(destination);
			else if (destination.TileObject != null)
				destination.TileObject.Interact(this);

			Redraw();
		}

		public void Redraw()
		{
			Window.dungeonContainer.Invalidate();
		}
	}
}
