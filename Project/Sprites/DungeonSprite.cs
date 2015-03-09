using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Project.Dungeon;

namespace Project.Sprites
{
	public abstract class DungeonSprite : TileObject, IEnumerable<CombatSprite>
	{
		protected DungeonSprite(Point loc) : base(loc)
		{
		}

		public IReadOnlyList<CombatSprite> Members { get; protected set; }

		public override void Interact(Game game)
		{
			throw new NotImplementedException();
		}


		public IEnumerator<CombatSprite> GetEnumerator()
		{
			return Members.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) Members).GetEnumerator();
		}
	}
}
