using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

			return itemContainer;
		}
	}
}
