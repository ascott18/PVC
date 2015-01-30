﻿using System;
using System.IO;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Items
{
	class ItemEquippable : Item
	{
		public const int MaxSlotId = 4;

		public enum SlotID
		{
			Weapon,
			Head,
			Body,
			Hands,
			Feet,
		}

		public readonly SlotID Slot;
		public readonly string Name;

		public ItemEquippable(int itemId, string slotName, string name) : base(itemId)
		{
			Name = name;

			if(!Enum.TryParse(slotName, true, out Slot))
				throw new InvalidDataException("Invalid slot for itemID " + itemId);
		}

		public Attributes Attributes { get; private set; }

		[XmlData.XmlParser("Equippable")]
		public static Item ParseItem(XElement itemElement)
		{
            var id = int.Parse(itemElement.Attribute("id").Value);
            var name = itemElement.Attribute("name").Value;
            var slot = itemElement.Attribute("slot").Value;

            return new ItemEquippable(id, slot, name)
            {
	            Attributes = Attributes.ParseAttributes(itemElement.Element("Attributes"))
            };
		}
	}
}
