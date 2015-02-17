using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public void SetItems(IEnumerable<Item> items)
		{
			itemFlowPanel.LoadItems(items);
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
        }
	}
}
