using System;
using System.Collections.Generic;
using System.Drawing;
using Project.Items;

namespace Project.Sprites
{
	class Party : DungeonSprite
	{
		public const int MaxHeroes = 3;

		private readonly List<Hero> heroes = new List<Hero>(MaxHeroes);

		private readonly List<Item> inventory = new List<Item>();
		public readonly IReadOnlyList<Item> Inventory;


		public Party(Point loc) : base(loc)
		{
			Members = heroes.AsReadOnly();
			Inventory = inventory.AsReadOnly();
		}

		public void AddHero(Hero hero)
		{
			if (heroes.Count > MaxHeroes)
				throw new Exception("Party has too many heroes");

			if (heroes.Count == 0)
				Image = hero.Image;

			heroes.Add(hero);
		}
	}
}
