using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;
using Project.Items;
using Project.Sprites;

namespace Project.Dungeon
{
    class Watercooler : TileObject
    {
		protected Watercooler(Point loc)
			: base(loc)
        {
        }


        public override void Interact(Game game)
        {
	        foreach (var sprite in game.Party.Members)
	        {
		        var hero = sprite as Hero;
		        hero.Health += hero.MaxHealth / 20;
	        }
        }


		/// <summary>
		///     Parses an XML element and returns a Watercooler object that represents it.
		/// </summary>
		/// <param name="coolerElement">The XElement to parse</param>
		/// <returns>The Watercooler object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("Watercooler")]
		public static TileObject XmlParser(XElement coolerElement)
		{
			var loc = new Point(int.Parse(coolerElement.Attribute("x").Value), int.Parse(coolerElement.Attribute("y").Value));

			var watercooler = new Watercooler(loc)
            {
				Image = XmlData.LoadImage(coolerElement.Attribute("texture").Value)
            };

		    return watercooler;
		}
    }
}
