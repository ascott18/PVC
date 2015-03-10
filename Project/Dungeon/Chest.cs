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
        private List<LootPool> lootPools;
		public bool ShouldDespawn { get; private set; }
		public bool Looted { get; private set; }


        protected Chest(Point loc) : base(loc)
        {
        }

        public override void Interact(Game game)
        {
	        if (Looted) return;
	        Looted = true;

            var loot = new List<Item>();
            foreach (var lootPool in lootPools)
            {
                loot.AddRange(lootPool.GenerateLoot());
            }

            var dialog = new ChestLootDialog();
            dialog.SetItems(loot);
            dialog.ShowDialog();

            game.Party.AddInventoryItemRange(loot);

			if (ShouldDespawn)
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

			var despawnElement = chestElement.Attribute("despawn");

            var chest = new Chest(loc)
            {
	            Image = XmlData.LoadImage(chestElement.Attribute("texture").Value),
	            lootPools = LootPool.ParseLootPools(chestElement),
				ShouldDespawn = despawnElement == null || (bool)despawnElement
            };

			return chest;
		}
    }
}
