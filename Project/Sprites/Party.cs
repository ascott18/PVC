using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project.Items;

namespace Project.Sprites
{
	public class Party : DungeonSprite, IEnumerable<Hero>
	{
		public const int MaxHeroes = 3;
		public readonly IReadOnlyList<Item> Inventory;

		private readonly List<Hero> heroes = new List<Hero>(MaxHeroes);
		private readonly List<Item> inventory = new List<Item>();


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

		public void RemoveInventoryItem(Item item)
		{
			if (inventory.Remove(item))
				if (InventoryChanged != null) InventoryChanged(this);
		}

		public void AddInventoryItem(Item item)
		{
			inventory.Add(item);
			if (InventoryChanged != null) InventoryChanged(this);
		}

		public void AddInventoryItemRange(IEnumerable<Item> items)
		{
			inventory.AddRange(items);
			if (InventoryChanged != null) InventoryChanged(this);
		}

		public event PartyEvent InventoryChanged;


		public IEnumerator<Hero> GetEnumerator()
		{
			return heroes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) heroes).GetEnumerator();
		}
	}

	public delegate void PartyEvent(Party party);
}
