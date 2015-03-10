using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	internal class SpellAreaDamage : Spell
	{
		private readonly int damage;

		protected SpellAreaDamage(XElement data) : base(data)
		{
			damage = (int)data.Attribute("damage");

			StateChanged += SpellAreaDamage_StateChanged;
		}

		private void SpellAreaDamage_StateChanged(Spell sender, CastState oldState)
		{
			switch (State)
			{
				case CastState.Starting:
					var target = Session.GetTarget(Owner);
					if (target == null) // if target is null, there are no valid targets.
						Cancel();
					break;

				case CastState.Finishing:
					if (Owner.Parent == Session.Party)
					{
						DamageMembers(Session.MonsterPack);
					}
					else
					{
						DamageMembers(Session.Party);
					}
					break;
			}
		}

		private void DamageMembers(DungeonSprite sprite)
		{
			foreach (var member in sprite.Members)
			{
				DoComboAction(DealBlockableDamage, Owner, member, damage);
				ApplyAuras(member);
			}
		}

		[XmlData.XmlParserAttribute("AreaDamage")]
		public static Spell Create(XElement data)
		{
			return new SpellAreaDamage(data);
		}

		protected override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine("");
			sb.Append("Area Damage: ");
			sb.AppendLine(damage.ToString());
		}
	}
}
