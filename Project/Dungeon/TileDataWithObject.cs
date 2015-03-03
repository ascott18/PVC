using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;

namespace Project.Dungeon
{
	class TileDataWithObject : TileData
	{
		public readonly string TileObjectName;

		protected TileDataWithObject(string objName, string texture) : base(true, texture)
		{
			TileObjectName = objName;
		}

		[XmlData.XmlParserAttribute("Object")]
		public new static TileData Parser(XElement tileElement)
		{
			var obj = tileElement.Attribute("object").Value;
			var texture = tileElement.Attribute("texture").Value;

			return new TileDataWithObject(obj, texture);
		}
	}
}
