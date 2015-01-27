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
	/// <summary>
	/// TileData is an immutable class that represents the static information about a tile
	/// that was parsed from XML. A single TileData instance will be used for all Tiles that
	/// use that tileID.
	/// </summary>
	class TileData
	{
		private static TileData[] data = new TileData[128];

		/// <summary>
		/// The tileID of this TileData, as defined by the "id" attribute in Tiles.xml
		/// </summary>
		public readonly int TileID;

		/// <summary>
		/// Whether or not tiles that use this TileData can be occupied by a TileObject.
		/// </summary>
		public readonly bool IsObstable;

		/// <summary>
		/// The background image to be used by tiles that use this TileData. Can be thought of 
		/// as a foreground image if IsObstacle is true.
		/// </summary>
		public readonly Image Image;

		private TileData(int id, bool isObstable, string imageName)
		{
			TileID = id;
			IsObstable = isObstable;

			if (imageName == "")
			{
				// A blank image if no image is defined.
				Image = new Bitmap(Tile.DimPixels, Tile.DimPixels);
			}
			else
			{
				// Try and get the embedded resource represented by the provided name.
				// Embedded resources are defined in Resources.resx
				Image = Resources.ResourceManager.GetObject(imageName) as Bitmap;
				if (Image == null)
					throw new FileNotFoundException("Resource not found", imageName);
			}
		}

		/// <summary>
		/// Gets the TileData for the requested tileID. Data will be parsed from Tiles.xml
		/// if it has not already been parsed. Otherwise, a cached instance will be returned.
		/// </summary>
		/// <param name="tileID">The ID of the type of tile.</param>
		/// <returns>The requested TileData instance for the tileID.</returns>
		public static TileData GetTileData(int tileID)
		{
			// Check if a TileData object already has been created for this ID.
			if (tileID < data.Length && data[tileID] != null)
				return data[tileID];

			// Resize the holding array if needed.
			if (tileID >= data.Length)
				Array.Resize(ref data, tileID + 50);


			var xml = XmlData.GetDocument("Tiles");

			// Get the XML element that holds the data for the requested tileID.
			var tileElement = xml.XPathSelectElement(String.Format("Tiles/Tile[@id='{0}']", tileID));
			if (tileElement == null)
				throw new Exception(String.Format("No Tile with id {0} found", tileID));

			TileData tileData = new TileData(tileID, bool.Parse(tileElement.Attribute("obstacle").Value), tileElement.Attribute("texture").Value);

			return data[tileID] = tileData;
		}
	}
}
