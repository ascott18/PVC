using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Project.Data;
using Project.Items;

namespace Project.Sprites
{
	internal class Monster : CombatSprite
	{
		private List<LootPool> lootPools = new List<LootPool>(); 

		public readonly int MonsterID;

		public Monster(MonsterPack pack, int monsterId) : base (pack)
		{
			MonsterID = monsterId;

			var xDoc = XmlData.GetDocument("Monsters");
			var monsterElement = xDoc.XPathSelectElement(String.Format("Monsters/Monster[number(@id)='{0}']", monsterId));

			ParseCommonAttributes(monsterElement);

			var lootPoolElements = monsterElement.Elements("LootPool");
			foreach (var lootPoolElement in lootPoolElements)
			{
				var poolID = (int)lootPoolElement.Attribute("id");
				var pool = LootPool.GetLootPool(poolID);
				lootPools.Add(pool);
			}

			RecalculateAttributes();
		}

		static readonly Random random = new Random();

		public void DoAction(CombatSession session)
		{
			if (CurrentCast == null)
			{
				foreach (var spell in Spells.OrderBy(_ => random.Next()))
				{
					if (spell.Start(session))
						return;
				}
			}
		}

		/// <summary>
		/// Generates the loot dropped by this monster based on the LootPool
		/// elements defined for it in XML.
		/// </summary>
		/// <returns>A List of Items dropped by this monster.</returns>
		public List<Item> GetLoot()
		{
			var items = new List<Item>();

			foreach (var lootPool in lootPools)
			{
				var loot = lootPool.GenerateLoot();
				items.AddRange(loot);
			}

			return items;
		}
	}
}
