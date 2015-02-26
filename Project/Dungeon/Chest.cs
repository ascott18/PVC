using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;
using Project.Items;

namespace Project.Dungeon
{
    class Chest : TileObject
    {
        private List<LootPool> lootPools = new List<LootPool>(); 

        protected Chest(Point loc) : base(loc)
        {
        }

        public override void Interact(Game game)
        {
            var loot = new List<Item>();
            foreach (var lootPool in lootPools)
            {
                loot.AddRange(lootPool.GenerateLoot());
            }

            var dialog = new ChestLootDialog();
            dialog.SetItems(loot);
            dialog.ShowDialog();

            game.Party.AddInventoryItemRange(loot);
            CurrentTile.TileObject = null;
        }


		/// <summary>
		///     Parses an XML element and returns a Chest object that represents it.
		/// </summary>
		/// <param name="chestElement">The XElement to parse</param>
		/// <returns>The Chest object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("Chest")]
		public static TileObject DoorXmlParser(XElement chestElement)
		{
			var loc = new Point(int.Parse(chestElement.Attribute("x").Value), int.Parse(chestElement.Attribute("y").Value));

            var chest = new Chest(loc)
            {
                Image = XmlData.LoadImage(chestElement.Attribute("texture").Value)
            };

            var lootPoolElements = chestElement.Elements("LootPool");
            foreach (var lootPoolElement in lootPoolElements)
            {
                var poolID = (int)lootPoolElement.Attribute("id");
                var pool = LootPool.GetLootPool(poolID);
                chest.lootPools.Add(pool);
            }

		    return chest;
		}
    }
}
