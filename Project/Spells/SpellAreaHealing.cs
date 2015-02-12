using System;
using System.Linq;
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
		    if (Session.Party.Members.Contains(Owner))
		    {
                HealMembers(Session.Party);
		    }
            else if (Session.MonsterPack.Members.Contains(Owner))
            {
                HealMembers(Session.MonsterPack);
            }
            else
            {
                throw new Exception("Sprite not found");
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
	}
}
