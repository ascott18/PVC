using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Project
{
	class Door : TileObject
	{
		public override void Interact()
		{
			throw new NotImplementedException();
		}


		public readonly Point DestinationPoint;
		public readonly int DestinationMapID;

		public Door(Point loc, Point dest, int mapID) : base(loc)
		{
			DestinationPoint = dest;
			DestinationMapID = mapID;
		}


		/// <summary>
		/// Parses an XML element and returns a Door object that represents it.	
		/// </summary>
		/// <param name="doorElement">The XElement to parse</param>
		/// <returns>The Door object parsed from the XML.</returns>
		[XmlParser("Door")]
		public static TileObject DoorXmlParser(XElement doorElement)
		{

			var loc = new Point(int.Parse(doorElement.Attribute("x").Value), int.Parse(doorElement.Attribute("y").Value));
			var dest = new Point(int.Parse(doorElement.Attribute("toX").Value), int.Parse(doorElement.Attribute("toY").Value));
			var destMapID = int.Parse(doorElement.Attribute("toMap").Value);

			return new Door(loc, dest, destMapID);
		}
	}
}
