using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Spells
{
	internal class SpellTargetedDamage : Spell
	{
		private CombatSprite target;
		private int damage;

		protected SpellTargetedDamage(XElement data) : base(data)
		{
			damage = (int)data.Attribute("damage");

			StateChanged += SpellTargetedDamage_StateChanged;
		}

		void SpellTargetedDamage_StateChanged(Spell sender)
		{
			if (State == CastState.Starting)
			{
				target = Session.AutoAcquireTarget(Caster);
				if (target == null)
					Cancel();
			}

			else if (State == CastState.Finishing)
			{
				target.Health -= damage;
				target = null;
			}
		}


		[XmlData.XmlParser("TargetedDamage")]
		public static Spell Create(XElement data)
		{
			return new SpellTargetedDamage(data);
		}
	}
}
