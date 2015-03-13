using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Project.Controls;
using Project.Data;
using Project.Items;
using Project.Properties;

namespace Project.Dungeon
{
	internal class VendingMachine : TileObject
	{
		private VendingMachine(Point loc) : base(loc)
		{
		}

		public override void Interact(Game game)
		{
			var total = game.Party.Inventory
			                .OfType<ItemCoin>()
			                .Aggregate(0, (sum, item) => sum + item.Value);

			const int requiredAmount = 150;
			if (total >= requiredAmount)
		    {
				MessageBox.Show(Resources.WinCondt_Yes, "Well Done");
                MainWindow.Window.Hide();
				new SplashScreen().Show();
		    }
		    else
		    {
				MessageBox.Show(String.Format(Resources.WinCondt_No, (double)requiredAmount/100), "Relief has not come yet");
		    }	

		}

		/// <summary>
		///     Parses an XML element and returns a VendingMachine object that represents it.
		/// </summary>
		/// <param name="doorElement">The XElement to parse</param>
		/// <returns>The Door object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("VendingMachine")]
		public static TileObject VendingMachineXmlParser(XElement machine)
		{
			var loc = new Point(int.Parse(machine.Attribute("x").Value), int.Parse(machine.Attribute("y").Value));

			return new VendingMachine(loc)
			{
				Image = XmlData.LoadImage(machine.Attribute("texture").Value)
			};
		}
	}
}
