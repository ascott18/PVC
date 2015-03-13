using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Project.Items;

namespace Project.Controls
{
	internal partial class ItemFlowPanel : FlowLayoutPanel
	{
		private readonly List<ItemContainer> itemContainers = new List<ItemContainer>();


		public ItemFlowPanel()
		{
			InitializeComponent();

			ClientSizeChanged += ItemFlowPanel_ClientSizeChanged;

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
		}

		private void ItemFlowPanel_ClientSizeChanged(object sender, EventArgs e)
		{
			foreach (var itemContainer in itemContainers)
			{
				itemContainer.Width = ClientSize.Width - 3;
			}
		}

		private ItemContainer GetInventoryItemContainer(int index)
		{
			if (index < itemContainers.Count)
				return itemContainers[index];

			var itemContainer = new ItemContainer
			{
				Width = ClientSize.Width - 3,
				Anchor = AnchorStyles.Left | AnchorStyles.Top
			};
			Controls.Add(itemContainer);
			itemContainers.Add(itemContainer);
			itemContainer.MouseDown += itemContainer_MouseDown;

			return itemContainer;
		}

		private void itemContainer_MouseDown(object sender, MouseEventArgs e)
		{
			if (ContainerMouseDown != null)
				ContainerMouseDown(sender as ItemContainer, e);
		}

		public event Action<ItemContainer, MouseEventArgs> ContainerMouseDown;

		private void ResetAllItemContainers()
		{
			foreach (var itemContainer in itemContainers)
			{
				itemContainer.Item = null;
				itemContainer.Hide();
			}
		}

		public void LoadItems(IEnumerable<Item> items)
		{
			SuspendLayout();

			ResetAllItemContainers();

			int index = 0;
			foreach (var item in OrderItems(items))
			{
				var container = GetInventoryItemContainer(index++);
				container.Item = item;
			}

			ResumeLayout(true);
		}

		private static IOrderedEnumerable<Item> OrderItems(IEnumerable<Item> items)
		{
			//return items.OrderBy(item => item.GetType().Name).ThenBy(item => item.Name);
			return items.OrderBy(item => item);
		}

		public void LoadGroupedItems(IEnumerable<Item> items)
		{
			SuspendLayout();

			ResetAllItemContainers();

			int index = 0;
			foreach (var item in OrderItems(items).GroupBy(item => item.ItemID))
			{
				var container = GetInventoryItemContainer(index++);
				container.ItemGroup = item;
			}

			ResumeLayout(true);
		}
	}
}
