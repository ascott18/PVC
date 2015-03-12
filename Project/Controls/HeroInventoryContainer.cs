using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Project.Items;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class HeroInventoryContainer : UserControl
	{
		private readonly List<ItemContainer> itemContainers = new List<ItemContainer>();
		private Hero hero;

		public HeroInventoryContainer()
		{
			InitializeComponent();

			for (int i = 0; i < ItemEquippable.NumSlots; i++)
			{
				var itemContainer = new ItemContainer
				{
					Width = equipmentFlow.Width - 2,
					Anchor = AnchorStyles.Left | AnchorStyles.Top
				};
				equipmentFlow.Controls.Add(itemContainer);
				itemContainers.Add(itemContainer);
				itemContainer.MouseDown += itemContainer_MouseDown;

				var slotName = Enum.GetName(typeof(ItemEquippable.SlotID), i);
				itemContainer.NoItemLabel = String.Format("<No {0}>", slotName);
			}

			AllowDrop = true;
			DragEnter += HeroInventoryContainer_DragEnter;
			DragDrop += HeroInventoryContainer_DragDrop;
		}

		public Hero Hero
		{
			get { return hero; }
			set
			{
				if (hero != null)
				{
					hero.EquipmentChanged -= hero_EquipmentChanged;
					foreach (var itemContainer in itemContainers)
						itemContainer.Item = null;
				}
				hero = value;
				if (hero == null)
				{
					Hide();
				}
				else
				{
					Show();
					hero.EquipmentChanged += hero_EquipmentChanged;
					hero_EquipmentChanged(hero);
					name.Text = hero.Name;
					image.Image = hero.Image;
				}
			}
		}

		private void itemContainer_MouseDown(object sender, MouseEventArgs e)
		{
			// Handles dragging items out of a hero's inventory.
			var container = sender as ItemContainer;
			var item = container.Item;

			if (item == null)
				return;

			var result = DoDragDrop(item, DragDropEffects.Move);

			if (result == DragDropEffects.Move)
			{
				Debug.WriteLine("UnEquip {0}", item);
				// We moved the item to somewhere. Remove it from our equipment.
				Hero.Unequip(container.Item as ItemEquippable);
			}
		}

		private void HeroInventoryContainer_DragDrop(object sender, DragEventArgs e)
		{
			// Handles the recieve of a drag of an ItemEquippable 
			var item = e.Data.GetData(typeof(ItemEquippable)) as ItemEquippable;
			if (item != null)
			{
				var oldItem = hero.Equip(item);
				Debug.WriteLine("Equip {0}, Unequip {1}", item, oldItem);
				if (oldItem != null)
					((Party)hero.Parent).AddInventoryItem(oldItem);
				return;
			}

			var itemfood = e.Data.GetData(typeof(ItemFood)) as ItemFood;
			if (itemfood != null)
			{
				hero.Health += itemfood.Amount;
			}
		}

		private void HeroInventoryContainer_DragEnter(object sender, DragEventArgs e)
		{
			// Notify that we can accept drags of Items.
			var data = e.Data.GetData(typeof(ItemEquippable));
			var datafood = e.Data.GetData(typeof(ItemFood));

			if (data is ItemEquippable)
				e.Effect = DragDropEffects.Move;
			else if (datafood is ItemFood)
				e.Effect = DragDropEffects.Move;
			else
				e.Effect = DragDropEffects.None;
		}

		private void hero_EquipmentChanged(CombatSprite sender)
		{
			for (int i = 0; i < itemContainers.Count; i++)
			{
				var itemContainer = itemContainers[i];
				var item = Hero.GetEquippedItem((ItemEquippable.SlotID)i);
				itemContainer.Item = item;
			}
		}
	}
}
