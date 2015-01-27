using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Project
{
	abstract class Item
	{
		public readonly int ItemID;

		public Item(int itemId)
		{
			ItemID = itemId;
		}


		public static Item GetItem(int itemID)
		{
			var methods = XmlData.XmlParsable<Item>.GetParsers();

			var itemsDoc = XmlData.GetDocument("Items");
			var itemElement = itemsDoc.XPathSelectElement(String.Format("Items/*[@id={0}]", itemID));

			var elementName = itemElement.Name.ToString();

			if (!methods.ContainsKey(elementName))
				throw new Exception("Missing parser for item type " + elementName);

			var parserMethod = methods[elementName];

			return parserMethod(itemElement); ;
		}
	}
}
