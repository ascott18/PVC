using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;

namespace Project.Spells
{
	class AuraDamageOverTime : AuraHealthOverTime
	{
		public AuraDamageOverTime(XElement data) : base(data)
		{
			AmountTotal = AmountRemaining = (int)data.Attribute("damage");
			Action = Spell.DealUnblockableDamage;
		}

		[XmlData.XmlParserAttribute("DamageOverTime")]
		public static Aura Create(XElement data)
		{
			return new AuraDamageOverTime(data);
		}

		public override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine(String.Format("Damage: {0:D1} over {1:F1} sec", AmountTotal, Duration));
		}
	}
}
