using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project
{
	class ItemEquippable : Item
	{
		public const int MAX_SLOTS = 3;

		public enum SlotID
		{
			Helm,
			Chest,
			Gloves,
			Boots,
		}

		public readonly SlotID Slot;
		public readonly string Name;

		public ItemEquippable(int itemID, string slotName, string name) : base(itemID)
		{
			Name = name;
			Slot = (SlotID) Enum.Parse(typeof (SlotID), slotName, true);
		}

		[ItemXmlParser("Equippable")]
		public static Item ParseItem(XElement itemElement)
		{
            var id = int.Parse(itemElement.Attribute("id").Value);
            var name = itemElement.Attribute("name").Value;
            var slot = itemElement.Attribute("slot").Value;

            return new ItemEquippable(id, slot, name);
		}
	}
}
