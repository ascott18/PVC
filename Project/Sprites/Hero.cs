using System;
using System.IO;
using System.Xml.XPath;
using Project.Data;
using Project.Items;

namespace Project.Sprites
{
	class Hero : CombatSprite
	{
		public readonly int HeroID;

		public override int MinHealth { get { return 1; } }

		public bool IsRetreated { get { return Health == 1; } }

		public override bool IsActive { get { return !IsRetreated; } }

		public Hero(int heroId)
		{
			HeroID = heroId;

			var xDoc = XmlData.GetDocument("Heroes");
			var monsterElement = xDoc.XPathSelectElement(String.Format("Heroes/Hero[@id='{0}']", heroId));

			ParseCommonAttributes(monsterElement);

			// Parse the items that the hero is equipped with by default,
			// and equip them on the hero.
			foreach (var xElement in monsterElement.Elements("Item"))
			{
				var item = Item.GetItem(int.Parse(xElement.Attribute("id").Value));
				if (!(item is ItemEquippable))
					throw new InvalidDataException("Heroes can't have non-equippable items");

				Equip(item as ItemEquippable);
			}

			EquipmentChanged += Hero_EquipmentChanged;
			RecalculateAttributes();
		}



		#region Equipment

		/// <summary>
		/// Holds the items that the hero has equipped. Each index is the integer representation of a ItemEquippable.SlotId.
		/// </summary>
		private readonly ItemEquippable[] equipment = new ItemEquippable[ItemEquippable.MaxSlotId];

		void Hero_EquipmentChanged(CombatSprite sender)
		{
			sender.RecalculateAttributes();
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

			if (EquipmentChanged != null) EquipmentChanged(this);

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

			if (EquipmentChanged != null) EquipmentChanged(this);

			return oldItem;
		}

		public event SpriteEvent EquipmentChanged;

		#endregion


		/// <summary>
		/// Recalculates the hero's current attributes,
		/// taking into account base attributes and attributes from equipment.
		/// </summary>
		public override sealed void RecalculateAttributes()
		{
			var attr = BaseAttributes;
			foreach (var itemEquippable in equipment)
			{
				if (itemEquippable != null)
					attr = attr + itemEquippable.Attributes;
			}

			Attributes = attr;
		}
	}
}
