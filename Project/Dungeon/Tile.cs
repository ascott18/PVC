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

		public readonly int X;
		public readonly int Y;

		public Tile(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}

		public IInteractible TileObject { get; set; }

		public void Draw(Graphics graphics)
		{
			
		}
	}
}
