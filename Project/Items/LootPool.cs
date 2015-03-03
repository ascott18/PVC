using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;
using Project.Items;

namespace Project.Items
{
	/// <summary>
	///     Represents a pool of possible loot that can drop from a monster.
	///     LootPools are parsed from XML, and referred to by a unique poolID.
	///     Items are generated upon a call to GenerateLoot().
	/// </summary>
	internal class LootPool
	{
		private struct LootEvent
		{
			public int ItemID;
			public double Chance;
		}

		private static readonly Dictionary<int, LootPool> pools = new Dictionary<int, LootPool>();

		private readonly List<LootEvent> loot = new List<LootEvent>();

		static Random random = new Random();

		private LootPool() { }

		private static LootPool Parse(XElement element)
		{
			var pool = new LootPool();

			var itemElements = element.Elements("Item");
			foreach (var itemElement in itemElements)
			{
				var itemID = (int) itemElement.Attribute("id");
				var chance = (double) itemElement.Attribute("chance");

				pool.loot.Add(new LootEvent{Chance = chance, ItemID = itemID});
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

			return items;
		}
	}
}