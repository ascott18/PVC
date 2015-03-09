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
	}
}
