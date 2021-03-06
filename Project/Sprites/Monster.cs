﻿using System;
using System.Collections.Generic;
using System.Linq;
using Project.Data;
using Project.Items;

namespace Project.Sprites
{
	public class Monster : CombatSprite
	{
		private static readonly Random random = new Random();
		public readonly int MonsterID;
		private readonly List<LootPool> lootPools = new List<LootPool>();

		public Monster(MonsterPack pack, int monsterId) : base(pack)
		{
			MonsterID = monsterId;


			var monsterElement = XmlData.GetXElementByID("Monsters", monsterId);

			ParseCommonAttributes(monsterElement);

			lootPools = LootPool.ParseLootPools(monsterElement);

			RecalculateAttributes();
		}

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
		///     Generates the loot dropped by this monster based on the LootPool
		///     elements defined for it in XML.
		/// </summary>
		/// <returns>A List of Items dropped by this monster.</returns>
		public IEnumerable<Item> GetLoot()
		{
			return LootPool.GetLoot(lootPools);
		}
	}
}
