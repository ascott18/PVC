using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	abstract class DungeonSprite : TileObject, ISprite
	{
		public void Draw(Graphics graphics)
		{
			throw new NotImplementedException();
		}

		public void Interact()
		{
			throw new NotImplementedException();
		}

		public DungeonSprite(Point loc) : base(loc)
		{
			
		}
	}
}
