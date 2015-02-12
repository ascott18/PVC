using System;
using System.Linq;
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
            damage = (int) data.Attribute("damage");

            StateChanged += SpellAreaDamage_StateChanged;
        }

        private void SpellAreaDamage_StateChanged(Spell sender, CastState oldState)
        {
            if (Session.Party.Members.Contains(Owner))
            {
                DamageMembers(Session.Party);
            }
            else if (Session.MonsterPack.Members.Contains(Owner))
            {
                DamageMembers(Session.MonsterPack);
            }
            else
            {
                throw new Exception("Sprite not found");
            }
        }

        private void DamageMembers(DungeonSprite sprite)
        {
            foreach (var member in sprite.Members)
            {
                member.Health -= damage;
            }
        }


        [XmlData.XmlParserAttribute("AreaDamage")]
        public static Spell Create(XElement data)
        {
            return new SpellAreaDamage(data);
        }
    }
}