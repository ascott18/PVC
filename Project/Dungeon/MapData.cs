using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;

namespace Project.Dungeon
{
	/// <summary>
	///     MapData represents a complete Map element parsed from Maps.xml.
	///     It is immutable, but the TileObjects that it contains may be modified as needed
	///     as gameplay progresses.
	/// </summary>
	public class MapData
	{
		/// <summary>
		///     The number of tiles in a map in the horizonal direction.
		/// </summary>
		public const int DimX = 13;

		/// <summary>
		///     The number of tiles in a map in the vertical direction.
		/// </summary>
		public const int DimY = 13;

		/// <summary>
		///     Holds cached MapData instances, keyed by their mapID.
		///     This array is dynamically resized as needed.
		/// </summary>
		private static MapData[] data = new MapData[128];


		/// <summary>
		///     The mapID that uniquely identifies this MapData from all other maps.
		/// </summary>
		public readonly int MapID;

		private readonly Dictionary<string, int> adjacencies = new Dictionary<string, int>();

		/// <summary>
		///     The TileData objects that represent the base data of each tile of this map,
		///     pased from the Tiles element of this MapData's corresponding Map element.
		/// </summary>
		private readonly TileData[,] tiles = new TileData[DimX, DimY];

		private XElement mapElement;

		private MapData(int id)
		{
			MapID = id;
		}

		/// <summary>
		///     Returns the TileData object for the requested map coordinates.
		/// </summary>
		/// <param name="loc">The coordinates of the tile to request.</param>
		/// <returns>The requested TileData object.</returns>
		public TileData GetTileData(Point loc)
		{
			return tiles[loc.X, loc.Y];
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
		///     Parses the XML data for the map and creates a new set of TileObjects
		///     as defined by the children of the Objects XMl element.
		/// </summary>
		/// <returns>A list of the created TileObjects. They are not cached.</returns>
		public List<TileObject> GetTileObjects()
		{
			var parserMethods = XmlData.XmlParsable<TileObject>.GetParsers();

			var objects = new List<TileObject>();

			// Create TileObjects for all the map's objects
			var objectsElement = mapElement.Element("Objects");
			if (objectsElement != null)
				foreach (XElement objectElement in objectsElement.Elements())
				{
					string elementName = objectElement.Name.ToString();

					// Check to see if there is a parser method defined for this element name.
					if (!parserMethods.ContainsKey(elementName))
						throw new Exception(String.Format("No parser for element {0}", elementName));

					// Attempt to parse it, delegating out to the method declared for this
					// element name using the [XmlParserAttribute(elementName)] attribute.
					TileObject parsedTileObject = parserMethods[elementName](objectElement);

					// If null was returned by the parser, something must have gone wrong.
					if (parsedTileObject == null)
						throw new Exception(String.Format("Could not parse element {0}", elementName));


					objects.Add(parsedTileObject);
				}

			// Create TileObjects for the TileData's static objects.
			for (int x = 0; x < tiles.GetLength(0); x++)
				for (int y = 0; y < tiles.GetLength(1); y++)
				{
					var tileData = tiles[x, y];

					var objTileData = tileData as TileDataWithObject;
					if (objTileData == null) continue;

					try
					{
						var type = Assembly
							.GetExecutingAssembly()
							.GetTypes()
							.First(t => t.Name == objTileData.TileObjectName && typeof(TileObject).IsAssignableFrom(t));

						var ctor = type.GetConstructor(new[] {typeof(Point)});
						var obj = (TileObject)ctor.Invoke(new object[] {new Point(x, y)});
						objects.Add(obj);
					}
					catch (Exception)
					{
						throw new ApplicationException("TileObject " + objTileData.TileObjectName + " couldn't be created.");
					}
				}

			return objects;
		}

		/// <summary>
		///     Parses Maps.xml and creates a MapData object.
		/// </summary>
		/// <param name="mapID">The MapID to parse from the XML document.</param>
		/// <returns>The parsed MapData</returns>
		private static MapData ParseMapData(int mapID)
		{
			// Get the XML element for the requested mapID.
			var mapElement = XmlData.GetXElementByID("Maps", mapID);
			if (mapElement == null)
				throw new Exception(String.Format("No Map with id {0} found", mapID));

			return data[mapID] = ParseMapData(mapID, mapElement);
		}

		/// <summary>
		///     Parses the given mapElement and creates a MapData object.
		/// </summary>
		/// <param name="mapID">The MapID to parse.</param>
		/// <returns>The parsed MapData</returns>
		public static MapData ParseMapData(int mapID, XElement mapElement)
		{
			// Create the new MapData if one didn't already exist.
			var mapData = new MapData(mapID)
			{
				mapElement = mapElement
			};

			// Populate mapData.tiles with the TileData objects for this map.
			var tilesElement = mapElement.Element("Tiles");
			if (tilesElement == null)
				throw new ApplicationException("Map element missing Tiles element.");

			string tileDataRaw = tilesElement.Value;
			int index = 0;
			foreach (Match match in Regex.Matches(tileDataRaw, @"[0-9]+"))
			{
				if (index >= DimX * DimY)
					throw new IndexOutOfRangeException(String.Format("Too many tiles defined for map ID {0}", mapID));

				mapData.tiles[index % DimX, index / DimX] = TileData.GetTileData(int.Parse(match.Value));

				index++;
			}

			if (index != DimX * DimY)
				throw new IndexOutOfRangeException(String.Format("Not enough tiles defined for map ID {0}", mapID));


			// Populate mapData.adjacencies with the defined AdjacentMaps.
			foreach (var xElement in mapElement.XPathSelectElements("AdjacentMaps/AdjacentMap"))
			{
				mapData.adjacencies[xElement.Attribute("dir").Value] = int.Parse(xElement.Attribute("id").Value);
			}

			return mapData;
		}


		/// <summary>
		///     Returns the id of the adjacent map to this map in the indicated direction.
		/// </summary>
		/// <param name="direction">The direction, either "N", "S", "E", or "W".</param>
		/// <returns>The mapID of the adjacent map.</returns>
		public int? GetAdjacentMapID(string direction)
		{
			if (adjacencies.ContainsKey(direction))
				return adjacencies[direction];

			return null;
		}
	}
}
