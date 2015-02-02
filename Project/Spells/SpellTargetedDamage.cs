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

		void SpellTargetedDamage_StateChanged(Spell sender)
		{
			CombatSprite target;
			switch (State)
			{
				case CastState.Starting:
					target = Session.AutoAcquireTarget(Owner);
					if (target == null) // if target is null, there are no valid targets.
						Cancel();
					break;

				case CastState.Finishing:
					target = Session.AutoAcquireTarget(Owner);
					if (target != null)
						target.Health -= damage;
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
