using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Project
{
	class Hero : CombatSprite
	{
		private readonly ItemEquippable[] equipment = new ItemEquippable[ItemEquippable.MAX_SLOTS];
		public readonly int HeroID;

		public Hero(int heroId)
		{
			HeroID = heroId;

			var xDoc = XmlData.GetDocument("Heroes");
			var monsterElement = xDoc.XPathSelectElement(String.Format("Heroes/Hero[@id='{0}']", heroId));

			ParseCommonAttributes(monsterElement);
		}
	}
}
