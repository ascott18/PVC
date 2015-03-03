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
			return XmlData.XmlParserParseByID<Item>("Items", itemID);
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
