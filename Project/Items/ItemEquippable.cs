using System;
using System.IO;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Items
{
	internal class ItemEquippable : Item
	{
		public enum SlotID
		{
			Weapon,
			Head,
			Body,
			Hands,
			Feet,
		}

		public static readonly int NumSlots = Enum.GetValues(typeof(SlotID)).Length;

		public readonly SlotID Slot;

		public ItemEquippable(int itemId, string name, string slotName) : base(itemId, name)
		{
			if (!Enum.TryParse(slotName, true, out Slot))
				throw new InvalidDataException("Invalid slot for itemID " + itemId);
		}

		public Attributes Attributes { get; private set; }

		[XmlData.XmlParserAttribute("Equippable")]
		public static Item ParseItem(XElement itemElement)
		{
			var id = int.Parse(itemElement.Attribute("id").Value);
			var name = itemElement.Attribute("name").Value;
			var slot = itemElement.Attribute("slot").Value;

			return new ItemEquippable(id, name, slot)
			{
				Attributes = Attributes.ParseAttributes(itemElement.Element("Attributes"))
			};
		}
	}
}
