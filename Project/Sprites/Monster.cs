using System;
using System.Xml.XPath;
using Project.Data;

namespace Project.Sprites
{
	class Monster : CombatSprite
	{
		public readonly int MonsterID;

		public Monster(int monsterId)
		{
			MonsterID = monsterId;

			var xDoc = XmlData.GetDocument("Monsters");
			var monsterElement = xDoc.XPathSelectElement(String.Format("Monsters/Monster[@id='{0}']", monsterId));

			ParseCommonAttributes(monsterElement);

			RecalculateAttributes();
		}

		public void DoAction(CombatSession arena)
		{
			if (CurrentCast == null)
			{
				foreach (var spell in Spells)
				{
					if (spell.Start(arena, this))
						return;
				}

			}
		}
	}
}
