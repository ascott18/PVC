using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Items
{
	internal class ItemFood : Item
	{
		public readonly int Amount;

		public ItemFood(int id, string name, int amount) : base(id, name)
		{
			Amount = amount;
		}


		[XmlData.XmlParserAttribute("Food")]
		public static Item ParseItem(XElement itemElement)
		{
			var id = int.Parse(itemElement.Attribute("id").Value);
			var name = itemElement.Attribute("name").Value;
			var amount = (int)itemElement.Attribute("amount");

			return new ItemFood(id, name, amount);
		}


		public override void Use(Hero hero)
		{
			hero.Health += Amount;
		}

		public override string GetTooltip()
		{
			var s = base.GetTooltip();
			var sb = new StringBuilder(s);

			sb.AppendLine();
			sb.AppendLine();
			sb.Append("Healing Amount: ");
			sb.AppendLine(Amount.ToString());

			return sb.ToString();
		}
	}
}
