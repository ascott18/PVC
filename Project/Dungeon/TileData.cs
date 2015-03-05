using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;
using Project.Properties;

namespace Project.Dungeon
{
	/// <summary>
	///     TileData is an immutable class that represents the static information about a tile
	///     that was parsed from XML. A single TileData instance will be used for all Tiles that
	///     use that tileID.
	/// </summary>
	public class TileData
	{
		private static readonly Dictionary<int, TileData> Data = new Dictionary<int, TileData>();

		/// <summary>
		///     The background image to be used by tiles that use this TileData. Can be thought of
		///     as a foreground image if IsObstacle is true.
		/// </summary>
		public readonly Image Image;

		/// <summary>
		///     Whether or not tiles that use this TileData can be occupied by a TileObject.
		/// </summary>
		public readonly bool IsObstacle;

		protected TileData(bool isObstacle, string imageName)
		{
			IsObstacle = isObstacle;

			Image = XmlData.LoadImage(imageName);
		}

		/// <summary>
		///     Gets the TileData for the requested tileID. Data will be parsed from Tiles.xml
		///     if it has not already been parsed. Otherwise, a cached instance will be returned.
		/// </summary>
		/// <param name="tileID">The ID of the type of tile.</param>
		/// <returns>The requested TileData instance for the tileID.</returns>
		public static TileData GetTileData(int tileID)
		{
			TileData ret;
			if (Data.TryGetValue(tileID, out ret))
				return ret;

			var tileData = XmlData.XmlParserParseByID<TileData>("Tiles", tileID);
			
			return Data[tileID] = tileData;
		}



		[XmlData.XmlParserAttribute("Tile")]
		public static TileData Parser(XElement tileElement)
		{
			var texture = tileElement.Attribute("texture").Value;
			var isObstacle = (bool)tileElement.Attribute("obstacle");

			var tileData = new TileData(isObstacle, texture);

			return tileData;
		}

	}
}
