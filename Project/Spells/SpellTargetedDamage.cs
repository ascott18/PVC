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
					{
						DoComboAction(DealBlockableDamage, Owner, target, damage);
						ApplyAuras(target);
					}
					break;
			}
		}


		[XmlData.XmlParserAttribute("TargetedDamage")]
		public static Spell Create(XElement data)
		{
			return new SpellTargetedDamage(data);
		}

		protected override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine("");
			sb.Append("Damage: ");
			sb.AppendLine(damage.ToString());
		}
	}
}
