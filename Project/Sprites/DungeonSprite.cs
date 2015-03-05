using System;
using System.Collections.Generic;
using System.Drawing;
using Project.Dungeon;

namespace Project.Sprites
{
	public abstract class DungeonSprite : TileObject
	{
		protected DungeonSprite(Point loc) : base(loc)
		{
		}

		public IReadOnlyList<CombatSprite> Members { get; protected set; }

		public override void Interact(Game game)
		{
			throw new NotImplementedException();
		}
	}
}
