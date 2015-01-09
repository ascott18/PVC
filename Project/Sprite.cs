using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Sprite : TileObject
	{
		public readonly Image image;
		public virtual void Draw(Graphics graphics)
		{
			graphics.DrawImage(image, tile.X, tile.Y, Tile.DIM_X, Tile.DIM_Y);
		}
	}
}
