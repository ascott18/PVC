using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Project.Data;
using Project.Items;

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

			if (total >= 100)
				MessageBox.Show("YOU WIN!!!!!");
			else
			{
			    MessageBox.Show("You do not have enough change!");
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
