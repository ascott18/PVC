using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Properties;

namespace Project.Controls
{
	public partial class WoodButton : Button
	{
		public WoodButton()
		{
			InitializeComponent();
			ForeColor = Color.Tan;
		}

		private void WoodButton_Paint(object sender, PaintEventArgs e)
		{
			var color = ForeColor;
			if (!Enabled)
				color = Color.Gray;

			e.Graphics.DrawRectangle(new Pen(color, 5), ClientRectangle);
		}
	}
}
