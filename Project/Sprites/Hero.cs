﻿using System;
using System.IO;
using System.Linq;
using Project.Data;
using Project.Items;

namespace Project.Sprites
{
	public class Hero : CombatSprite
	{
		public readonly int HeroID;

		public Hero(Party party, int heroID) : base(party)
		{
			HeroID = heroID;

			var heroElement = XmlData.GetXElementByID("Heroes", heroID);

			ParseCommonAttributes(heroElement);

			// Parse the items that the hero is equipped with by default,
			// and equip them on the hero.
			foreach (var xElement in heroElement.Elements("Item"))
			{
				var item = Item.GetItem(int.Parse(xElement.Attribute("id").Value));
				if (!(item is ItemEquippable))
					throw new InvalidDataException("Heroes can't have non-equippable items");

				Equip(item as ItemEquippable);
			}

			EquipmentChanged += Hero_EquipmentChanged;
			RecalculatingAttributes += Hero_RecalculatingAttributes;
			RecalculateAttributes();
		}

		#region Equipment

		/// <summary>
		///     Holds the items that the hero has equipped. Each index is the integer representation of a ItemEquippable.SlotId.
		/// </summary>
		private readonly ItemEquippable[] equipment = new ItemEquippable[ItemEquippable.NumSlots];


		private void Hero_EquipmentChanged(CombatSprite sender)
		{
			sender.RecalculateAttributes();
		}

		/// <summary>
		///     Equips an item. Returns the previously equipped item in the slot if there was one.
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
		///     Unequips the item from a slot, returning the item if there was one.
		/// </summary>
		/// <param name="slot">The slot to unequip.</param>
		/// <returns>The previously equipped item, or null if the slot was empty.</returns>
		public ItemEquippable Unequip(ItemEquippable.SlotID slot)
		{
			var oldItem = equipment[(int)slot];
			equipment[(int)slot] = null;

			if (EquipmentChanged != null) EquipmentChanged(this);

			return oldItem;
		}

		/// <summary>
		///     Unequips the item, returning true if successful.
		/// </summary>
		/// <param name="item">The item to unequip.</param>
		/// <returns>True if the requested item was unequipped, otherwise false.</returns>
		public bool Unequip(ItemEquippable item)
		{
			var index = Array.IndexOf(equipment, item);

			if (index == -1) return false;

			equipment[index] = null;
			if (EquipmentChanged != null) EquipmentChanged(this);

			return true;
		}

		/// <summary>
		///     Gets the item equipped in the given slot.
		/// </summary>
		/// <param name="slot">The slot to query.</param>
		/// <returns>The item equipped in that slot.</returns>
		public ItemEquippable GetEquippedItem(ItemEquippable.SlotID slot)
		{
			return equipment[(int)slot];
		}

		public event SpriteEvent EquipmentChanged;

		#endregion

		public override int MinHealth
		{
			get { return 1; }
		}

		public bool IsRetreated
		{
			get { return Health == 1; }
		}

		public override bool IsActive
		{
			get { return !IsRetreated; }
		}


		private void Hero_RecalculatingAttributes(CombatSprite sender, SpriteAttributesRecalcEventArgs args)
		{
			foreach (var itemEquippable in equipment)
			{
				if (itemEquippable != null)
					args.Attributes += itemEquippable.Attributes;
			}
		}
	}
}
