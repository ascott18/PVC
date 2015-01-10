using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Project.Properties;

namespace Project
{
	class TileData : XMLData
	{
		private static TileData[] data = new TileData[128];

		public readonly int TileID;
		public readonly bool IsObstable;
		public readonly Image Texture;

		private TileData(int id, bool isObstable, string imageName)
		{
			TileID = id;
			IsObstable = isObstable;

			Texture = Resources.ResourceManager.GetObject(imageName) as Bitmap;
			if (Texture == null)
				throw new FileNotFoundException("Resource not found", imageName);
		}

		public static TileData GetTileData(int id)
		{
			// Check if a TileData object already has been created for this ID.
			if (id < data.Length && data[id] != null)
				return data[id];

			// Resize the holding array if needed.
			if (id >= data.Length)
				Array.Resize(ref data, id + 50);


			var xml = GetDataXmlDocument("Tiles");

			// Get the XML element that holds the data for the requested tileID.
			var tileElement = xml.XPathSelectElement(String.Format("Tiles/Tile[@id='{0}']", id));
			if (tileElement == null)
				throw new Exception(String.Format("No Tile with id {0} found", id));

			TileData tileData = new TileData(id, bool.Parse(tileElement.Attribute("obstacle").Value), tileElement.Attribute("texture").Value);

			return data[id] = tileData;
		}
	}
}
