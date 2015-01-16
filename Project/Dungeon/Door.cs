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
	/// <summary>
	/// Represents a door in a dungeon. When the player interacts with a door,
	/// it will take the player to a certain point on a certain map.
	/// Created by parsing Maps.xml for Door elements.
	/// </summary>
	class Door : TileObject
	{
		public override void Interact(Game game)
		{
			game.SetPartyLocation(DestinationMapID, DestinationPoint);
		}


		public readonly Point DestinationPoint;
		public readonly int DestinationMapID;

		private Door(Point loc, Point dest, int mapID) : base(loc)
		{
			DestinationPoint = dest;
			DestinationMapID = mapID;
		}


		/// <summary>
		/// Parses an XML element and returns a Door object that represents it.	
		/// </summary>
		/// <param name="doorElement">The XElement to parse</param>
		/// <returns>The Door object parsed from the XML.</returns>
		[TileObjectXmlParser("Door")]
		public static TileObject DoorXmlParser(XElement doorElement)
		{

			var loc = new Point(int.Parse(doorElement.Attribute("x").Value), int.Parse(doorElement.Attribute("y").Value));
			var dest = new Point(int.Parse(doorElement.Attribute("toX").Value), int.Parse(doorElement.Attribute("toY").Value));
			var destMapID = int.Parse(doorElement.Attribute("toMap").Value);

			return new Door(loc, dest, destMapID)
			{
				Image = XMLData.LoadImage(doorElement.Attribute("texture").Value)
			};
		}
	}
}
