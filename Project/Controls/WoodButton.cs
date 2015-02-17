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
			BackgroundImage = Resources.wood;
			Font = new Font("Arial", 12);
			ForeColor = ColorTranslator.FromHtml("#fbf4a4");
			FlatStyle = FlatStyle.Flat;
		}
	}
}
