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

		private Tile[,] tiles = new Tile[DIM_X,DIM_Y];

		public void Draw(Graphics graphics)
		{
			foreach (var tile in tiles)
			{
				tile.Draw(graphics);
			}
		}
	}
}
