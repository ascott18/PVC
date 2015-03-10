using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Items;

namespace Project.Controls
{
	internal partial class ItemContainer : UserControl
	{
		private Item item;
		private string noItemLabel;

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
				nameLabel.Text = Item.Name;
				nameLabel.ForeColor = Color.Black;
				toolTip.SetToolTip(nameLabel, Item.GetTooltip());
			}
		}
	}
}
