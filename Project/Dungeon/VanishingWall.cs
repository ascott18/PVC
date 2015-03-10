using System;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using Project.Data;

namespace Project.Dungeon
{
	internal class VanishingWall : TileObject
	{
		protected VanishingWall(Point loc) : base(loc)
		{
		}

		public override void Interact(Game game)
		{
			CurrentTile.TileObject = null;
		}


		/// <summary>
		///     Parses an XML element and returns a wall object that represents it.
		/// </summary>
		/// <param name="xElement">The XElement to parse</param>
		/// <returns>The wall object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("VanishingWall")]
		public static TileObject VanishingWallXmlParser(XElement xElement)
		{
			var loc = new Point(int.Parse(xElement.Attribute("x").Value), int.Parse(xElement.Attribute("y").Value));

			return new VanishingWall(loc)
			{
				Image = XmlData.LoadImage(xElement.Attribute("texture").Value)
			};
		}
	}
}
