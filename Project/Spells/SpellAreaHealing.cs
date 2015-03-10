using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	internal class SpellAreaHealing: Spell
	{
		private readonly int healing;

		protected SpellAreaHealing(XElement data) : base(data)
		{
			healing = (int)data.Attribute("healing");

			StateChanged += SpellAreaHealing_StateChanged;
		}

		private void SpellAreaHealing_StateChanged(Spell sender, CastState oldState)
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
                        HealMembers(Session.Party);
                    }
                    else
                    {
                        HealMembers(Session.MonsterPack);
                    }
                    break;
            }
		}

	    private void HealMembers(DungeonSprite sprite)
	    {
	        foreach (var member in sprite.Members)
	        {
	            DoComboAction(Heal, Owner, member, healing);
				ApplyAuras(member);
	        }
	    }


		[XmlData.XmlParserAttribute("AreaHealing")]
		public static Spell Create(XElement data)
		{
			return new SpellAreaHealing(data);
		}

		protected override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine("");
			sb.Append("Group Heal: ");
			sb.AppendLine(healing.ToString());
		}
	}
}
