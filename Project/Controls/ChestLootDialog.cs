using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Items;

namespace Project
{
	internal partial class ChestLootDialog : Form
	{
		public ChestLootDialog()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
		}

		public void SetItems(Image image, IEnumerable<Item> items)
		{
			chestImage.Image = image;
			itemFlowPanel.LoadItems(items);
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
