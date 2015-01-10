using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Project
{
	internal class MapData : XMLData
	{
		public const int DIM_X = 14;
		public const int DIM_Y = 14;
		private static MapData[] data = new MapData[128];


		public readonly int MapID;
		public ReadOnlyCollection<DoorData> Doors { get; private set; }


		private readonly TileData[,] tiles = new TileData[DIM_X, DIM_Y];


		private MapData(int id)
		{
			MapID = id;
		}

		/// <summary>
		///     Returns the TileData object for the requested map coordinates.
		/// </summary>
		/// <param name="x">The x coordinate to request.</param>
		/// <param name="y">The y coordinate to request.</param>
		/// <returns>The requested TileData object.</returns>
		public TileData GetTileData(int x, int y)
		{
			return tiles[x, y];
		}


		/// <summary>
		///     Gets a MapData object for the requested mapID. MapData objects are cached.
		/// </summary>
		/// <param name="mapID">The mapID to get the MapData for.</param>
		/// <returns>The requested MapData.</returns>
		public static MapData GetMapData(int mapID)
		{
			// Check to see if there already is a data object created.
			if (mapID < data.Length && data[mapID] != null)
				return data[mapID];

			// Resize the holding array if needed.
			if (mapID >= data.Length)
				Array.Resize(ref data, mapID + 50);


			return ParseMapData(mapID);
		}

		/// <summary>
		/// Parses the XML document and creates a MapData object.
		/// </summary>
		/// <param name="mapID">The MapID to parse from the XML document.</param>
		/// <returns>The parsed MapData</returns>
		private static MapData ParseMapData(int mapID)
		{
			XDocument xml = GetDataXmlDocument("Maps");

			// Get the XML element for the request mapID.
			XElement mapElement = xml.XPathSelectElement(String.Format("Maps/Map[@id='{0}']", mapID));
			if (mapElement == null)
				throw new Exception(String.Format("No Map with id {0} found", mapID));

			// Create the new MapData if one didn't already exist.
			var mapData = new MapData(mapID);





			// Populate mapData.tiles with the TileData objects for this map.
			string tileDataRaw = mapElement.Element("Tiles").Value;
			int index = 0;
			foreach (Match match in Regex.Matches(tileDataRaw, @"[0-9]+"))
			{
				if (index >= DIM_X * DIM_Y)
					throw new IndexOutOfRangeException(String.Format("Too many tiles defined for map ID {0}", mapID));

				mapData.tiles[index % DIM_X, index / DIM_X] = TileData.GetTileData(int.Parse(match.Value));

				index++;
			}

			// Make sure there were as many tiles as there should have been.
			if (index != DIM_X * DIM_Y)
				throw new IndexOutOfRangeException(String.Format("Not enough tiles defined for map ID {0}", mapID));






			// Create data representations of the map's doors
			var doors = new List<DoorData>();
			foreach (var doorElement in mapElement.Element("Doors").Elements())
			{
				var loc = new Point(int.Parse(doorElement.Attribute("x").Value), int.Parse(doorElement.Attribute("y").Value));
				var dest = new Point(int.Parse(doorElement.Attribute("toX").Value), int.Parse(doorElement.Attribute("toY").Value));
				var destMapID = int.Parse(doorElement.Attribute("toMap").Value);

				doors.Add(new DoorData(loc, dest, destMapID));
			}
			// Store it as readonly so that nothing else can modify it.
			mapData.Doors = doors.AsReadOnly();






			return data[mapID] = mapData;
		}
	}

	class DoorData
	{
		public readonly Point Location;
		public readonly Point DestinationPoint;
		public readonly int DestinationMapID;

		public DoorData(Point loc, Point dest, int mapID)
		{
			Location = loc;
			DestinationPoint = dest;
			DestinationMapID = mapID;
		}
	}
}
