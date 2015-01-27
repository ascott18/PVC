using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Project
{
	class MonsterPack : DungeonSprite
	{
		public readonly int UniqueID;

		public readonly List<Monster> Monsters = new List<Monster>();
		public const int MaxMonsters = 3;

		public MonsterPack(Point loc, int uniqueId) : base(loc)
		{
			UniqueID = uniqueId;
		}


		public override void Interact(Game game)
		{
			//TODO: Enter combat mode with the monster pack.
			CurrentTile.TileObject = null;
		}

		/// <summary>
		/// Parses an XML element and returns a MonsterPack object that represents it.	
		/// </summary>
		/// <param name="mpElement">The XElement to parse</param>
		/// <returns>The MonsterPack object parsed from the XML.</returns>
		[TileObjectXmlParser("MonsterPack")]
		public static TileObject XmlParser(XElement mpElement)
		{

			var loc = new Point(int.Parse(mpElement.Attribute("x").Value), int.Parse(mpElement.Attribute("y").Value));
			var uniqueID = int.Parse(mpElement.Attribute("id").Value);

			var mp = new MonsterPack(loc, uniqueID);

			foreach (var monsterElement in mpElement.XPathSelectElements("Monster"))
			{
				var monsterID = int.Parse(monsterElement.Attribute("id").Value);
				mp.Monsters.Add(new Monster(monsterID));
			}

			mp.Image = mp.Monsters.First().Image;

			return mp;
		}
	}
}
