using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Project.Data;
using Project.Spells;
using Project.Sprites;

namespace Project.Items
{
	public class ItemEquippable : Item
	{
		public enum SlotID
		{
			Weapon,
			Head,
			Body,
			Feet,
		}

		public static readonly int NumSlots = Enum.GetValues(typeof(SlotID)).Length;

		public readonly SlotID Slot;

		public ItemEquippable(int itemId, string name, string slotName) : base(itemId, name)
		{
			if (!Enum.TryParse(slotName, true, out Slot))
				throw new InvalidDataException("Invalid slot for itemID " + itemId);
		}

		public Attributes Attributes { get; private set; }

		[XmlData.XmlParserAttribute("Equippable")]
		public static Item ParseItem(XElement itemElement)
		{
			var id = int.Parse(itemElement.Attribute("id").Value);
			var name = itemElement.Attribute("name").Value;
			var slot = itemElement.Attribute("slot").Value;

			return new ItemEquippable(id, name, slot)
			{
				Attributes = Attributes.Parse(itemElement.Element("Attributes"))
			};
		}

	    public override void Use(Hero hero)
	    {
	        hero.Equip(this);
	    }

		public override string GetTooltip()
		{
			var s = base.GetTooltip();
			var sb = new StringBuilder(s);

			sb.AppendLine();
			sb.AppendLine();
			sb.Append("Slot: ");
			sb.AppendLine(Slot.ToString());
			sb.AppendLine();

			sb.AppendLine(Attributes.ToString());

			return sb.ToString();
		}
	}
}
