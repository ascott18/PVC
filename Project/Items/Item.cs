using System;
using System.Text;
using System.Xml.XPath;
using Project.Data;
using Project.Sprites;

namespace Project.Items
{
	internal abstract class Item
	{
		public readonly string Name;
		public readonly int ItemID;

		protected Item(int itemId, string name)
		{
			Name = name;
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

			return parserMethod(itemElement);
		}

	    public virtual void Use(Hero hero)
	    {
	        throw new NotImplementedException();
	    }

		public virtual string GetTooltip()
		{
			return Name;
		}
	}
}
