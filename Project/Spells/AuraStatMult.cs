using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

		public override void GetTooltip(StringBuilder sb)
		{
			sb.Append("Stats: ");
			
			bool appendComma = false;
			Action<int, string> fmt = (val, name) =>
			{
				if (val == 0) return;

				if (appendComma)
					sb.Append(", ");
				sb.Append(String.Format("{0:D1}% {1}", val, name));
				appendComma = true;
			};

			fmt(Multiplier.StaminaPercent, "Stamina");
			fmt(Multiplier.BlockPercent, "Block");
			fmt(Multiplier.ComboPercent, "Combo");

			sb.AppendLine();
		}
	}
}
