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
	            member.Health += healing;
	        }
	    }


		[XmlData.XmlParserAttribute("AreaHealing")]
		public static Spell Create(XElement data)
		{
			return new SpellAreaHealing(data);
		}

		public override string GetTooltip()
		{
			var s = base.GetTooltip();
			var sb = new StringBuilder(s);

			sb.AppendLine("");
			sb.AppendLine("Group Heal");
			sb.Append("Amount: ");
			sb.AppendLine(healing.ToString());

			return sb.ToString();
		}
	}
}
