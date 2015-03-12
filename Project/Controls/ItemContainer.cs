using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Project.Items;

namespace Project.Controls
{
	internal partial class ItemContainer : UserControl
	{
		private Item item;
		private string noItemLabel;
		private int count;

		public ItemContainer()
		{
			InitializeComponent();

			SetStyle(ControlStyles.StandardClick, true);

			nameLabel.MouseDown += label_MouseDown;
		}

		public Item Item
		{
			get { return item; }
			set
			{
				item = value;
				count = 0;

				UpdateLabel();
			}
		}

		public IGrouping<int, Item> ItemGroup
		{
			set
			{
				item = value.First();
				count = value.Count();

				UpdateLabel();
			}
		}

		public string NoItemLabel
		{
			get { return noItemLabel; }
			set
			{
				noItemLabel = value;

				UpdateLabel();
			}
		}

		private void label_MouseDown(object sender, MouseEventArgs e)
		{
			OnMouseDown(e);
		}

		private void UpdateLabel()
		{
			if (Item == null)
			{
				if (string.IsNullOrEmpty(NoItemLabel))
				{
					Hide();
				}
				else
				{
					Show();
					nameLabel.Text = NoItemLabel;
					nameLabel.ForeColor = Color.Gray;
					toolTip.SetToolTip(nameLabel, NoItemLabel);
				}
			}
			else
			{
				Show();
				nameLabel.Text = (count != 0 ? "(" + count + ") " : "") + Item.Name;
				nameLabel.ForeColor = Color.Black;
				toolTip.SetToolTip(nameLabel, Item.GetTooltip());
			}
		}
	}
}
