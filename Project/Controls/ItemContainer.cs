using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Items;

namespace Project.Controls
{
	internal partial class ItemContainer : UserControl
	{
		private Item item;

		public ItemContainer()
		{
			InitializeComponent();
		}

		public Item Item
		{
			get { return item; }
			set
			{
				if (item != null)
				{
					label1.Text = null;
				}
				item = value;
				if (item == null)
				{
					Hide();
				}
				else
				{
					Show();
					label1.Text = item.Name;
				}
			}
		}
	}
}
