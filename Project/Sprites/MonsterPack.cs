using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;

namespace Project.Sprites
{
	public class MonsterPack : DungeonSprite
	{
		public const int MaxMonsters = 3;
		public readonly int UniqueID;

		private readonly List<Monster> monsters = new List<Monster>();

		public MonsterPack(Point loc, int uniqueId) : base(loc)
		{
			Members = monsters.AsReadOnly();

			UniqueID = uniqueId;
		}

		public override void Interact(Game game)
		{
			game.EnterCombat(this);
		}

		/// <summary>
		///     Parses an XML element and returns a MonsterPack object that represents it.
		/// </summary>
		/// <param name="mpElement">The XElement to parse</param>
		/// <returns>The MonsterPack object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("MonsterPack")]
		public static MonsterPack XmlParser(XElement mpElement)
		{
			var loc = new Point(int.Parse(mpElement.Attribute("x").Value), int.Parse(mpElement.Attribute("y").Value));
			var uniqueID = int.Parse(mpElement.Attribute("id").Value);

			var mp = new MonsterPack(loc, uniqueID);

			foreach (var monsterElement in mpElement.XPathSelectElements("Monster"))
			{
				if (mp.monsters.Count > MaxMonsters)
					throw new Exception("Too many monsters for this monster pack");

				var monsterID = int.Parse(monsterElement.Attribute("id").Value);
				var monster = new Monster(mp, monsterID);

				mp.monsters.Add(monster);
			}

			mp.Image = mp.Members.First().Image;

			return mp;
		}
	}
}
