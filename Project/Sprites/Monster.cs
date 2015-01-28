using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Project
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
		}

		public void DoAction(CombatSession arena)
		{
			if (CurrentCast == null)
			{
				var spellToCast = Spells.First(spell => !spell.IsCasting);
				if (spellToCast != null)
					spellToCast.Start(arena, this);
			}
		}
	}
}
