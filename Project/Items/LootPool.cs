using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Project.Data;

namespace Project.Items
{
	/// <summary>
	///     Represents a pool of possible loot that can drop from a monster.
	///     LootPools are parsed from XML, and referred to by a unique poolID.
	///     Items are generated upon a call to GenerateLoot().
	/// </summary>
	internal class LootPool
	{
		private static readonly Dictionary<int, LootPool> pools = new Dictionary<int, LootPool>();

		private static readonly Random random = new Random();
		private readonly List<LootEvent> loot = new List<LootEvent>();

		private LootPool()
		{
		}

		public int Limit { get; private set; }

		private static LootPool Parse(XElement element)
		{
			var pool = new LootPool();

			var itemElements = element.Elements("Item");

			var limitAttribute = element.Attribute("limit");
			if (limitAttribute != null)
				pool.Limit = (int)limitAttribute;
			else
				pool.Limit = 0;

			foreach (var itemElement in itemElements)
			{
				var itemID = (int)itemElement.Attribute("id");
				var chance = (double)itemElement.Attribute("chance");

				pool.loot.Add(new LootEvent {Chance = chance, ItemID = itemID});
			}

			return pool;
		}

		/// <summary>
		///     Gets the loot pool with the specified ID.
		///     LootPools are cached as they are created.
		/// </summary>
		/// <param name="id">The ID of the LootPool to get.</param>
		/// <returns>The LootPool object parsed from XML.</returns>
		public static LootPool GetLootPool(int id)
		{
			LootPool pool;
			if (pools.TryGetValue(id, out pool))
				return pool;

			var xElement = XmlData.GetXElementByID("LootPools", id);

			if (xElement == null)
				throw new Exception("Could not find loot pool with id " + id);


			return pools[id] = Parse(xElement);
		}

		/// <summary>
		///     Rolls for loot from this loot pool, and returns all the items recieved.
		/// </summary>
		/// <returns>The loot that was successfully rolled for.</returns>
		public List<Item> GenerateLoot()
		{
			var items = new List<Item>();

			foreach (var lootEvent in loot)
			{
				var itemID = lootEvent.ItemID;
				var chance = lootEvent.Chance;

				var roll = random.NextDouble();
				if (roll < chance)
					items.Add(Item.GetItem(itemID));
			}

			if (Limit > 0)
			{
				items = items.OrderBy(_ => random.Next()).Take(Limit).ToList();
			}

			return items;
		}

		/// <summary>
		///     Parses all LootPool child elements of the given xelement.
		/// </summary>
		/// <param name="element">The parent element to parse loot pools from.</param>
		/// <returns>The list of LootPool objects parsed.</returns>
		public static List<LootPool> ParseLootPools(XElement element)
		{
			var lootPools = new List<LootPool>();

			var lootPoolElements = element.Elements("LootPool");
			foreach (var lootPoolElement in lootPoolElements)
			{
				var poolID = (int)lootPoolElement.Attribute("id");
				var pool = GetLootPool(poolID);
				lootPools.Add(pool);
			}

			return lootPools;
		}


		/// <summary>
		///     Generates the loot dropped by a set of LootPools.
		/// </summary>
		/// <returns>A List of Items dropped by the LootPools.</returns>
		public static List<Item> GetLoot(IEnumerable<LootPool> lootPools)
		{
			var items = new List<Item>();

			foreach (var lootPool in lootPools)
			{
				var loot = lootPool.GenerateLoot();
				items.AddRange(loot);
			}

			return items;
		}

		private struct LootEvent
		{
			public double Chance;
			public int ItemID;
		}
	}
}
