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

namespace Project.Controls
{
	internal partial class ItemContainer : UserControl
	{
		private Item item;

		public ItemContainer()
		{
			InitializeComponent();

			SetStyle(ControlStyles.StandardClick, true);

			label1.MouseDown += label_MouseDown;
		}

		private void label_MouseDown(object sender, MouseEventArgs e)
		{
			OnMouseDown(e);
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
