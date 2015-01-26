using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Party : DungeonSprite
	{
		private readonly List<Hero> Heroes = new List<Hero>();
        private List<Item> _partyInventory = new List<Item>();


		public Party(Point loc) : base(loc)
		{
		}

		public void AddHero(Hero hero)
		{
			if (Heroes.Count == 0)
				Image = hero.Image;
			Heroes.Add(hero);
		}
	}
}
