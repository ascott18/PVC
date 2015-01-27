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

		private DungeonMap LoadDungeonMap(int mapID)
		{
			DungeonMap map;

			if (!maps.TryGetValue(mapID, out map))
			{
				map = maps[mapID] = new DungeonMap(mapID, this);
			}
			return map;
		}

		public void SetPartyLocation(int mapID, Point point)
		{
			DungeonMap map = LoadDungeonMap(mapID);

			Party.SetLocation(map.GetTile(point));
			CurrentMap = map;

		}

		internal Game(MainWindow window)
		{
			Window = window;

			// TODO: Temp code
			Party = new Party(new Point(7, 7));
			Party.AddHero(new Hero() { Image = Resources.milton, Name = "Milton" });
			Party.AddHero(new Hero() { Image = Resources.stu, Name = "Stu" });

			SetPartyLocation(1, Party.InitialLocation);

			window.combatArena1.Party = Party;
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
			{
				// We are trying to go off the edge of the current map if 
				// GetTile returned null. Try to go to an adjacent map.
				string dir = null;
				if (location.X < 0)
				{
					dir = "W";
					location.X = MapData.DIM_X-1;
				}
				else if (location.X >= MapData.DIM_X)
				{
					dir = "E";
					location.X = 0;
				}
				else if (location.Y < 0)
				{
					dir = "N";
					location.Y = MapData.DIM_Y-1;
				}
				else if (location.Y >= MapData.DIM_Y)
				{
					dir = "S";
					location.Y = 0;
				}

				if (dir == null)
					throw new Exception("Didn't determine out-of-bound direction");

				var newMapID = CurrentMap.MapData.GetAdjacentMapID(dir);

				// If there is no adjacent map defined, don't do anything.
				if (newMapID == null) return;

				// Peek at the adjacent map and see if we can actually go to that spot.
				var newMap = LoadDungeonMap((int)newMapID);
				destination = newMap.GetTile(location);

				if (destination.CanBeOccupied())
					SetPartyLocation((int)newMapID, location);

				return;
			}

			if (destination.CanBeOccupied())
				Party.SetLocation(destination);
			else if (destination.TileObject != null)
				destination.TileObject.Interact(this);
		}

		public void Redraw()
		{
			Window.dungeonContainer.Invalidate();
		}
	}
}
