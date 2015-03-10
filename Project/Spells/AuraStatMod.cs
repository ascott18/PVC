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
	class AuraStatMod : Aura
	{
		public readonly Attributes Mod;

		protected AuraStatMod(XElement data)
			: base(data)
		{
			Mod = Attributes.Parse(data.Element("Attributes"));

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
			args.Attributes += Mod;
		}

		void AuraStatMult_Removed(object sender, EventArgs e)
		{
			Target.RecalculatingAttributes -= Target_RecalculatingAttributes;
			Target.RecalculateAttributes();
		}

		[XmlData.XmlParserAttribute("StatMod")]
		public static Aura Create(XElement data)
		{
			return new AuraStatMod(data);
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
				sb.Append((val > 0 ? "+" : "") + String.Format("{0:D1} {1}", val, name));
				appendComma = true;
			};

			fmt(Mod.Stamina, "Stamina");
			fmt(Mod.Block, "Block");
			fmt(Mod.Combo, "Combo");

			sb.AppendLine();
		}
	}
}
