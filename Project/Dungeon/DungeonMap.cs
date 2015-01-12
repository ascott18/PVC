using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class DungeonMap
	{
		public const int DIM_X = 14;
		public const int DIM_Y = 14;
		public readonly int MapID;
		public readonly MapData MapData;

		private Tile[,] tiles = new Tile[DIM_X,DIM_Y];

		public DungeonMap(int mapId)
		{
			MapID = mapId;
			MapData = MapData.GetMapData(mapId);

			for (int x = 0; x < DIM_X; x++)
			{
				for (int y = 0; y < DIM_Y; y++)
				{
					var tile = new Tile(this, new Point(x, y));
					tiles[x, y] = tile;
				}
			}
		}

		public void Draw(Graphics graphics)
		{
			foreach (var tile in tiles)
			{
				tile.Draw(graphics);
			}
		}
	}
}
