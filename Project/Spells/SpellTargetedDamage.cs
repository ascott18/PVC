using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	internal class SpellTargetedDamage : Spell
	{
		private CombatSprite target;
		private readonly int damage;

		protected SpellTargetedDamage(XElement data) : base(data)
		{
			damage = (int)data.Attribute("damage");

			StateChanged += SpellTargetedDamage_StateChanged;
		}

		void SpellTargetedDamage_StateChanged(Spell sender)
		{
			switch (State)
			{
				case CastState.Starting:
					target = Session.AutoAcquireTarget(Caster);
					if (target == null)
						Cancel();
					break;

				case CastState.Finishing:
					target.Health -= damage;
					target = null;
					break;
			}
		}


		[XmlData.XmlParser("TargetedDamage")]
		public static Spell Create(XElement data)
		{
			return new SpellTargetedDamage(data);
		}
	}
}
