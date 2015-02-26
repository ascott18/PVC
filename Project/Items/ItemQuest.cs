using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Items
{
    internal class ItemQuest : Item
    {
        public ItemQuest(int itemId, string name) : base(itemId, name)
		{

		}

        [XmlData.XmlParserAttribute("Quest")]
        public static Item ParseItem(XElement itemElement)
        {
            var id = int.Parse(itemElement.Attribute("id").Value);
            var name = itemElement.Attribute("name").Value;
            return new ItemQuest(id, name);
        }

        public override string GetTooltip()
        {
            var s = base.GetTooltip();
            s += "\nQuest Item";

            return s;
        }
    }
}
