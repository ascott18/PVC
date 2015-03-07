using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	class AuraStatMult : Aura
	{
		public readonly AttributesMultiplier Multiplier;

		protected AuraStatMult(XElement data) : base(data)
		{
			Multiplier = AttributesMultiplier.Parse(data.Element("Multiplier"));

			Applied += AuraStatMult_Applied;
			Removed += AuraStatMult_Removed;
		}

		void AuraStatMult_Applied(object sender, EventArgs e)
		{
			Target.RecalculatingAttributes += Target_RecalculatingAttributes;
			Target.RecalculateAttributes();
		}

		void Target_RecalculatingAttributes(CombatSprite sender, SpriteAttributesRecalcEventArgs args)
		{
			args.AttributesMultiplier += Multiplier;
		}

		void AuraStatMult_Removed(object sender, EventArgs e)
		{
			Target.RecalculatingAttributes -= Target_RecalculatingAttributes;
			Target.RecalculateAttributes();
		}

		[XmlData.XmlParserAttribute("StatMult")]
		public static Aura Create(XElement data)
		{
			return new AuraStatMult(data);
		}
	}
}
