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
    internal class ItemCoin : Item
    {
        public readonly int Value;
        
        public ItemCoin(int itemId, string name,int value) : base(itemId, name)
        {
            this.Value = value;
        }

        [XmlData.XmlParserAttribute("Coin")]
        public static Item ParseItem(XElement itemElement)
        {
            var id = int.Parse(itemElement.Attribute("id").Value);
            var name = itemElement.Attribute("name").Value;
            var value = int.Parse(itemElement.Attribute("value").Value);
            return new ItemCoin(id, name, value);
        }

        public override string GetTooltip()
        {
            var s = base.GetTooltip();
            s += "\nCoin Item";

            return s;
        }
    }
}
