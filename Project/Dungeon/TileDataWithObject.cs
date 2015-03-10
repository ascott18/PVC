using System;
using System.Linq;
using System.Xml.Linq;
using Project.Data;

namespace Project.Dungeon
{
	internal class TileDataWithObject : TileData
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
