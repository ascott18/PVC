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


		[XmlData.XmlParser("TargetedDamage")]
		public static Spell Create(XElement data)
		{
			return new SpellTargetedDamage()
			{
				damage = (int)data.Attribute("damage"),
				CastDuration = (int)data.Attribute("castTime")
			};
		}

		protected override void OnCastStart(CombatSession session, CombatSprite caster)
		{
			base.OnCastStart(session, caster);
			target = session.AutoAcquireTarget(caster);
		}

		protected override void OnCastFinish()
		{
			target.Health -= damage;
			target = null;

			base.OnCastFinish();
		}

	}
}
