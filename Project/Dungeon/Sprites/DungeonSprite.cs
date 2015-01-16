using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	abstract class DungeonSprite : TileObject
	{

		public override void Interact(Game game)
		{
			throw new NotImplementedException();
		}

		public DungeonSprite(Point loc) : base(loc)
		{
			
		}
	}
}
