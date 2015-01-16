using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Project
{
	/// <summary>
	/// MapData represents a complete Map element parsed from Maps.xml.
	/// It is immutable, but the TileObjects that it contains may be modified as needed
	/// as gameplay progresses.
	/// </summary>
	internal class MapData : XMLData
	{
		/// <summary>
		/// The number of tiles in a map in the horizonal direction.
		/// </summary>
		public const int DIM_X = 14;

		/// <summary>
		/// The number of tiles in a map in the vertical direction.
		/// </summary>
		public const int DIM_Y = 14;

		/// <summary>
		/// Holds cached MapData instances, keyed by their mapID.
		/// This array is dynamically resized as needed.
		/// </summary>
		private static MapData[] data = new MapData[128];


		/// <summary>
		/// The mapID that uniquely identifies this MapData from all other maps.
		/// </summary>
		public readonly int MapID;


		/// <summary>
		/// The TileObjects that were constructed as this MapData was parsed from XML.
		/// These should be set on the appropriate Tiles of the DungeonMap that this MapData corresponds to,
		/// and may be modified as needed as gameplay progresses.
		/// </summary>
		private readonly TileObject[,] objects = new TileObject[DIM_X, DIM_Y];

		/// <summary>
		/// The TileData objects that represent the base data of each tile of this map,
		/// pased from the Tiles element of this MapData's corresponding Map element.
		/// </summary>
		private readonly TileData[,] tiles = new TileData[DIM_X, DIM_Y];


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
		///     Parses the XML document and creates a MapData object.
		/// </summary>
		/// <param name="mapID">The MapID to parse from the XML document.</param>
		/// <returns>The parsed MapData</returns>
		private static MapData ParseMapData(int mapID)
		{
			XDocument xml = GetDataXmlDocument("Maps");

			// Get the XML element for the requested mapID.
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



			var parserMethods = GetXmlParserMethods();



			// Create TileObjects for all the map's objects
			foreach (XElement objectElement in mapElement.Element("Objects").Elements())
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


				// Store the parsed TileObject in the array at the correct location.
				Point loc = parsedTileObject.Location;
				mapData.objects[loc.X, loc.Y] = parsedTileObject;
			}

			return data[mapID] = mapData;
		}



		/// <summary>
		///     The cached result of GetXmlParserMethods(). Don't read this field - always call GetXmlParserMethods().
		/// </summary>
		private static Dictionary<string, Func<XElement, TileObject>> parserMethods;

		/// <summary>
		///     Gets a dictionary of all methods that have been marked with [XmlParserAttribute(elementName)],
		///     with the keys of the dictionary as elementName and the values as Func&lt;XElement, TileObject&gt;
		/// </summary>
		/// <returns>The dictionary of XML pasing methods</returns>
		private static Dictionary<string, Func<XElement, TileObject>> GetXmlParserMethods()
		{
			// The dictionary is cached since new parsers won't be added after it is first created,
			// and creating it is a pretty involved process that we don't want to do repeatedly.
			if (parserMethods != null)
				return parserMethods;

			// Inspired by http://stackoverflow.com/questions/3467765/get-method-details-using-reflection-and-decorated-attribute
			var methods = Assembly.GetExecutingAssembly()
			                      .GetTypes()
			                      .SelectMany(type => type.GetMethods())

								  // Filter out only methods that are marked with [XmlParserAttribute]
			                      .Where(info => info.GetCustomAttributes<TileObject.TileObjectXmlParserAttribute>().Any())

									// Create a Dictionary from the methods that were marked with this attribute.
			                      .ToDictionary
				<MethodInfo, string, Func<XElement, TileObject>>(
					// The key to the Dictionary should be the elementName defined by the attribute.
					info => info.GetCustomAttributes<TileObject.TileObjectXmlParserAttribute>().First().ElementName,

					// Create a Func<XElement, TileObject> as the value of the dictionary.
					info => element => info.Invoke(null, new object[] { element }) as TileObject
				);

			return parserMethods = methods;
		}
	}
}
