using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Tile
	{
		public const int DIM_X = 50;
		public const int DIM_Y = 50;

		public readonly DungeonMap DungeonMap;
		public readonly int X;
		public readonly int Y;

		public Tile(DungeonMap dungeonMap, int x, int y)
		{
			X = x;
			Y = y;

			DungeonMap = dungeonMap;
			TileData = DungeonMap.MapData.GetTileData(x, y);
		}

		public TileObject TileObject { get; set; }
		public TileData TileData { get; private set; }

		public void Draw(Graphics graphics)
		{
			graphics.DrawImage(TileData.Image, X * DIM_X, Y * DIM_Y, DIM_X, DIM_Y);

			if (TileObject != null)
				graphics.DrawImage(TileObject.Image, X * DIM_X, Y * DIM_Y, DIM_X, DIM_Y);
		}
	}
}
