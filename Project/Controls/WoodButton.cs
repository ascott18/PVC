using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
