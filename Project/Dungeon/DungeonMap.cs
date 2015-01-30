using System.Drawing;

namespace Project.Dungeon
{
	/// <summary>
	/// DungeonMap represents a single map in a dungeon.
	/// For our purposes, the whole game is a single dungeon.
	/// Maps are 14x14 grids of Tiles.
	/// </summary>
	class DungeonMap
	{
		public readonly int MapID;
		public readonly MapData MapData;
		public readonly Game Game;

		private readonly Tile[,] tiles = new Tile[MapData.DimX, MapData.DimY];

		public DungeonMap(int mapId, Game game)
		{
			MapID = mapId;
			Game = game;

			MapData = MapData.GetMapData(mapId);

			for (int x = 0; x < MapData.DimX; x++)
			{
				for (int y = 0; y < MapData.DimY; y++)
				{
					var tile = new Tile(this, new Point(x, y));
					tiles[x, y] = tile;
				}
			}

			// Create a new set of initial TileObjects from the XML,
			// and place them where they need to be.
			foreach (var tileObject in MapData.GetTileObjects())
			{
				var loc = tileObject.InitialLocation;
				tileObject.SetLocation(tiles[loc.X, loc.Y]);
			}
		}

		public void Draw(Graphics graphics)
		{
			foreach (var tile in tiles)
			{
				tile.Draw(graphics);
			}
		}

		public Tile GetTile(Point point)
		{
			if (point.X >= 0 && point.Y >= 0 && point.X < MapData.DimX && point.Y < MapData.DimY)
				return tiles[point.X, point.Y];

			return null;
		}
	}
}
