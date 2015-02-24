using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	internal class SpellSelfHealing : Spell
	{
		private readonly int healing;

		protected SpellSelfHealing(XElement data) : base(data)
		{
			healing = (int)data.Attribute("healing");

			StateChanged += SpellTargetedHealing_StateChanged;
		}

		private void SpellTargetedHealing_StateChanged(Spell sender, CastState oldState)
		{
			CombatSprite target;
			switch (State)
			{
				case CastState.Starting:
					target = Owner;
					if (target == null) // if target is null, there are no valid targets.
						Cancel();
					break;

				case CastState.Finishing:
					target = Owner;
					if (target != null)
						target.Health += healing;
					break;
			}
		}


		[XmlData.XmlParserAttribute("SelfHealing")]
		public static Spell Create(XElement data)
		{
			return new SpellSelfHealing(data);
		}

		public override string GetTooltip()
		{
			var s = base.GetTooltip();
			var sb = new StringBuilder(s);

			sb.AppendLine("");
			sb.AppendLine("Self Heal");
			sb.Append("Amount: ");
			sb.AppendLine(healing.ToString());

			return TooltipCache = sb.ToString();
		}
	}
}
