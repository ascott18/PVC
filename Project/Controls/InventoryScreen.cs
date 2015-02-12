using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Items;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class InventoryScreen : UserControl
	{
		private Party party;

		private readonly List<HeroInventoryContainer> hiContainers = new List<HeroInventoryContainer>();
		private readonly List<ItemContainer> inventoryItemContainers = new List<ItemContainer>();

		public InventoryScreen()
		{
			InitializeComponent();

			//TODO: magic number right here needs to be a constant in the Party class.
			for (int i = 0; i < 3; i++)
			{
				var heroInventory = new HeroInventoryContainer();
				heroInventory.Location = new Point(10, 5 + (i * heroInventory.Height));
				hiContainers.Add(heroInventory);
				Controls.Add(heroInventory);
			}

			inventoryFlow.AllowDrop = true;
			inventoryFlow.DragEnter += inventoryFlow_DragEnter;
			inventoryFlow.DragDrop += inventoryFlow_DragDrop;
		}

		void inventoryFlow_DragDrop(object sender, DragEventArgs e)
		{
			// Handles the recieve of a drag of an ItemEquippable 
			var item = e.Data.GetData(typeof(ItemEquippable)) as ItemEquippable;
			if (item != null)
			{
				Party.AddInventoryItem(item);
			}
		}

		void inventoryFlow_DragEnter(object sender, DragEventArgs e)
		{
			// Notify that we can accept drags of ItemEquippable.
			var data = e.Data.GetData(typeof(ItemEquippable));
			if (data is ItemEquippable)
				e.Effect = DragDropEffects.Move;
		}


		public Party Party
		{
			get { return party; }
			set
			{
				if (party != null)
				{
					party.InventoryChanged -= Party_InventoryChanged;
					foreach (var heroInventoryContainer in hiContainers)
						heroInventoryContainer.Hero = null;
				}

				party = value;
				if (party != null)
				{
					for (int i = 0; i < hiContainers.Count; i++)
					{
						var heroInventoryContainer = hiContainers[i];
						Hero hero = null;
						if (i < party.Members.Count)
							hero = party.Members[i] as Hero;

						heroInventoryContainer.Hero = hero;
					}
					party.InventoryChanged += Party_InventoryChanged;
					Party_InventoryChanged(party);
				}
				
			}
		}

		void Party_InventoryChanged(Party party)
		{
			int i;
			for (i = 0; i < party.Inventory.Count; i++)
			{
				var item = party.Inventory[i];

				ItemContainer container;
				if (i >= inventoryItemContainers.Count)
					container = CreateInventoryItemContainer();
				else
					container = inventoryItemContainers[i];

				container.Item = item;
			}

			// Clean out item containers that don't have a matching item.
			for (; i < inventoryItemContainers.Count; i++)
			{
				var container = inventoryItemContainers[i];
				container.Item = null;
			}
		}

		private ItemContainer CreateInventoryItemContainer()
		{
			var itemContainer = new ItemContainer
			{
				Width = inventoryFlow.Width - 2,
				Anchor = AnchorStyles.Left | AnchorStyles.Top
			};
			inventoryFlow.Controls.Add(itemContainer);
			inventoryItemContainers.Add(itemContainer);
			itemContainer.MouseDown += InventoryItemContainer_MouseDown;

			return itemContainer;
		}

		void InventoryItemContainer_MouseDown(object sender, MouseEventArgs e)
		{
			// Handles dragging items out of our inventory.

			var container = sender as ItemContainer;
			var result = DoDragDrop(container.Item, DragDropEffects.Move);

			if (result == DragDropEffects.Move)
			{
				// We moved the item to somewhere. Remove it from our inventory.
				Party.RemoveInventoryItem(container.Item);
			}
		}
	}
}
