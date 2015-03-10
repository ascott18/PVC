using System;
using System.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Items
{
	public abstract class Item
	{
		public readonly int ItemID;
		public readonly string Name;

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
