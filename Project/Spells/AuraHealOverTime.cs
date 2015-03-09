using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;

namespace Project.Spells
{
	class AuraHealOverTime : AuraHealthOverTime
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
	}
}
