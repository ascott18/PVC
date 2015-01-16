using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Properties;

namespace Project
{
	/// <summary>
	/// TileObject represends some entity that can occupy a Tile of a Map.
	/// This might include the player, an enemy, or a door, and can be extended to be anthing else.
	/// TileObjects are represented in Maps.xml inside the Objects element, and are constructed from XML
	/// using methods that are marked with XmlParserAttribute.
	/// </summary>
	class TileObject
	{
		public Point Location { get; private set; }
		public Image Image { get; private set; }

		public virtual void Interact()
		{
			throw new NotImplementedException();
		}


		protected TileObject(Point loc)
		{
			Image = Resources.lumbergh; // TODO: temp
			Location = loc;
		}

		/// <summary>
		/// An attribute to be used on methods that will parse an incoming
		/// XElement with name elementName that is a child of an Object element
		/// in Maps.xml. The method must return a new TileObject based on the data
		/// in the XElement.
		/// </summary>
		public class TileObjectXmlParserAttribute : Attribute
		{
			public readonly string ElementName;
			/// <summary>
			/// Declare a method as one that will take an incoming XElement, parse it,
			/// and return a new TileObject instance.
			/// </summary>
			/// <param name="elementName">The XElement.Name that this method will parse.</param>
			public TileObjectXmlParserAttribute(string elementName)
			{
				ElementName = elementName;
			}
		}
	}
}
