using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Project
{
	class Hero : CombatSprite
	{
		private readonly ItemEquippable[] equipment = new ItemEquippable[ItemEquippable.MaxSlotId];
		public readonly int HeroID;

		public Hero(int heroId)
		{
			HeroID = heroId;

			var xDoc = XmlData.GetDocument("Heroes");
			var monsterElement = xDoc.XPathSelectElement(String.Format("Heroes/Hero[@id='{0}']", heroId));

			ParseCommonAttributes(monsterElement);

			foreach (var xElement in monsterElement.Elements("Item"))
			{
				var item = Item.GetItem(int.Parse(xElement.Attribute("id").Value));
				if (!(item is ItemEquippable))
					throw new InvalidDataException("Heroes can't have non-equippable items");

				Equip(item as ItemEquippable);
			}
		}

		/// <summary>
		/// Equips an item. Returns the previously equipped item in the slot if there was one.
		/// </summary>
		/// <param name="item">The item to equip.</param>
		/// <returns>The previously equipped item, or null if the slot was empty.</returns>
		public ItemEquippable Equip(ItemEquippable item)
		{
			var oldItem = equipment[(int)item.Slot];
			equipment[(int)item.Slot] = item;
			return oldItem;
		}

		/// <summary>
		/// Unequips the item from a slot, returning the item if there was one.
		/// </summary>
		/// <param name="slot">The slot to unequip.</param>
		/// <returns>The previously equipped item, or null if the slot was empty.</returns>
		public ItemEquippable Unequip(ItemEquippable.SlotID slot)
		{
			var oldItem = equipment[(int) slot];
			equipment[(int) slot] = null;
			return oldItem;
		}

		public override Attributes Attributes
		{
			get
			{
				var attr = BaseAttributes;
				foreach (var itemEquippable in equipment)
				{
					attr = itemEquippable.Attributes;
				}

				return attr;
			}
		}
	}
}
