using System;
using System.Collections.Generic;
using System.Drawing;
using Project.Dungeon;

namespace Project.Sprites
{
	abstract class DungeonSprite : TileObject
	{
		public IReadOnlyList<CombatSprite> Members { get; protected set; } 

		public override void Interact(Game game)
		{
			throw new NotImplementedException();
		}

		protected DungeonSprite(Point loc) : base(loc)
		{
			
		}
	}
}
