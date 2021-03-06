﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Items;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class InventoryScreen : Form
	{
		private readonly List<HeroInventoryContainer> hiContainers = new List<HeroInventoryContainer>();
		private Party party;


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
			inventoryFlow.ContainerMouseDown += InventoryItemContainer_MouseDown;
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

		private void inventoryFlow_DragDrop(object sender, DragEventArgs e)
		{
			// Handles the recieve of a drag of an ItemEquippable 
			var item = e.Data.GetData(typeof(ItemEquippable)) as ItemEquippable;
			if (item != null)
			{
				Debug.WriteLine("ToInventory {0}", item);
				Party.AddInventoryItem(item);
			}
		}

		private void inventoryFlow_DragEnter(object sender, DragEventArgs e)
		{
			// Notify that we can accept drags of ItemEquippable.
			var data = e.Data.GetData(typeof(ItemEquippable));
			if (party.Inventory.Contains(data))
				e.Effect = DragDropEffects.None;
			else if (data is ItemEquippable)
				e.Effect = DragDropEffects.Move;
		}


		private void Party_InventoryChanged(Party party)
		{
			inventoryFlow.LoadGroupedItems(party.Inventory);
		}

		private void InventoryItemContainer_MouseDown(ItemContainer container, MouseEventArgs e)
		{
			// Handles dragging items out of our inventory.
			var item = container.Item;

			var result = DoDragDrop(item, DragDropEffects.Move);

			if (result == DragDropEffects.Move)
			{
				// We moved the item to somewhere. Remove it from our inventory.
				Debug.WriteLine("FromInventory {0}", item);
				Party.RemoveInventoryItem(item);
			}
		}
	}
}
