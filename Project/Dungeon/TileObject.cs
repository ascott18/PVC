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

		private static readonly Dictionary<string, Func<XElement, TileObject>> Parsers = new Dictionary<string, Func<XElement, TileObject>>();

		public static TileObject ParseXmlTileObject(XElement element)
		{
			var elementName = element.Name.ToString();
			if (!Parsers.ContainsKey(elementName))
				return null;

			var parser = Parsers[elementName];

			return parser(element);
		}

		/// <summary>
		/// An attribute to be used on methods that will parse an incoming
		/// XElement with name elementName that is a child of an Object element
		/// in Maps.xml. The method must return a new TileObject based on the data
		/// in the XElement provided.
		/// </summary>
		public class XmlParserAttribute : Attribute
		{
			public readonly string ElementName;
			/// <summary>
			/// Declare a method as one that will take an incoming XElement, parse it,
			/// and return a new TileObject instance.
			/// </summary>
			/// <param name="elementName">The XElement.Name that this method will parse.</param>
			public XmlParserAttribute(string elementName)
			{
				ElementName = elementName;
			}
		}
	}
}
