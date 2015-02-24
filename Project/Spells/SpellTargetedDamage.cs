using System.Text;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	internal class SpellTargetedDamage : Spell
	{
		private readonly int damage;

		protected SpellTargetedDamage(XElement data) : base(data)
		{
			damage = (int)data.Attribute("damage");

			StateChanged += SpellTargetedDamage_StateChanged;
		}

		private void SpellTargetedDamage_StateChanged(Spell sender, CastState oldState)
		{
			CombatSprite target;
			switch (State)
			{
				case CastState.Starting:
					target = Session.GetTarget(Owner);
					if (target == null) // if target is null, there are no valid targets.
						Cancel();
					break;

				case CastState.Finishing:
					target = Session.GetTarget(Owner);
					if (target != null)
						target.Health -= damage;
					break;
			}
		}


		[XmlData.XmlParserAttribute("TargetedDamage")]
		public static Spell Create(XElement data)
		{
			return new SpellTargetedDamage(data);
		}

		public override string GetTooltip()
		{
			var s = base.GetTooltip();
			var sb = new StringBuilder(s);

			sb.AppendLine("");
			sb.AppendLine("Targeted Damage");
			sb.Append("Amount: ");
			sb.AppendLine(damage.ToString());

			return sb.ToString();
		}
	}
}
