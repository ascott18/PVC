using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;

namespace Project.Spells
{
	internal class AuraHealOverTime : AuraHealthOverTime
	{
		public AuraHealOverTime(XElement data)
			: base(data)
		{
			AmountTotal = AmountRemaining = (int)data.Attribute("healing");
			Action = Spell.Heal;
		}

		[XmlData.XmlParserAttribute("HealOverTime")]
		public static Aura Create(XElement data)
		{
			return new AuraHealOverTime(data);
		}

		public override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine(String.Format("Heal: {0:D1} over {1:F1} sec", AmountTotal, Duration));
		}
	}
}
