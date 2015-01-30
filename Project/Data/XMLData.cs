using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Project.Properties;

namespace Project.Data
{
	/// <summary>
	/// XmlData is a utility class for parsing data from XML.
	/// It contains functionality for loading XML files from the project's resx
	/// file and storing these as XDocuments for repeated use.
	/// </summary>
	static class XmlData
	{
		private static readonly Dictionary<string, XDocument> Documents = new Dictionary<string, XDocument>();

		/// <summary>
		/// Gets the XML Document for the specified embedded resource.
		/// Loads it if it isn't already loaded.
		/// </summary>
		/// <returns>The XDocument representing the requested resources.</returns>
		internal static XDocument GetDocument(string resourceName)
		{
			XDocument doc;

			// See if we have already loaded this resource into an XDocument
			if (Documents.TryGetValue(resourceName, out doc))
				return doc;

			// convert string to stream
			var res = Resources.ResourceManager.GetString(resourceName);
			if (res == null)
				throw new FileNotFoundException("Could not find resource", resourceName);

			var stream = new MemoryStream(Encoding.UTF8.GetBytes(res));

			// Load up the document
			doc = XDocument.Load(stream);

			// Save it, and return it.
			return Documents[resourceName] = doc;
		}

		/// <summary>
		/// Try and get the embedded image represented by the provided name.
		/// Embedded resources are defined in Resources.resx
		/// </summary>
		/// <param name="imageName">The name of the embedded image to retrive</param>
		/// <returns>The image, or null if the input parameter was the empty string.</returns>
		internal static Image LoadImage(string imageName)
		{
			if (imageName == "")
			{
				return null;
			}

			var image = Resources.ResourceManager.GetObject(imageName) as Bitmap;
			if (image == null)
				throw new FileNotFoundException("Resource not found", imageName);

			return image;
		}

		[AttributeUsage(AttributeTargets.Method)]
		internal class XmlParserAttribute : Attribute
		{
			public readonly string ElementName;

			/// <summary>
			/// Declare a method as one that will take an incoming XElement, parse it,
			/// and return a new instance of an appropriate type.
			/// </summary>
			/// <param name="elementName">The XElement.Name that this method will parse.</param>
			public XmlParserAttribute(string elementName)
			{
				ElementName = elementName;
			}
		}

		internal static class XmlParsable<T>
		{ 
			private static Dictionary<string, Func<XElement, T>> parsers;
			/// <summary>
			///     Gets a dictionary of all methods that have been marked with
			///		[XmlParserAttribute(elementName)] and have return type T,
			///     with the keys of the dictionary as elementName and the values as Func&lt;XElement, T&gt;
			/// </summary>
			/// <returns>The dictionary of XML pasing methods</returns>
			public static Dictionary<string, Func<XElement, T>> GetParsers()
			{
				// The dictionary is cached since new parsers won't be added after it is first created,
				// and creating it is a pretty involved process that we don't want to do repeatedly.
				if (parsers != null)
					return parsers;

				// Inspired by http://stackoverflow.com/questions/3467765/get-method-details-using-reflection-and-decorated-attribute
				var methods = Assembly.GetExecutingAssembly()
									  .GetTypes()
									  .SelectMany(type => type.GetMethods())

									  // Filter out only methods that are marked with [XmlParserAttribute]
									  // that return the requested type.
									  .Where(info => info.GetCustomAttributes<XmlParserAttribute>().Count() == 1
										&& typeof(T).IsAssignableFrom(info.ReturnType))

									  // Create a Dictionary from the methods that were marked with this attribute.
									  .ToDictionary
					<MethodInfo, string, Func<XElement, T>>(
						// The key to the Dictionary should be the elementName defined by the attribute.
						info => info.GetCustomAttributes<XmlParserAttribute>().First().ElementName,

						// Create a Func<XElement, TileObject> as the value of the dictionary.
						info => element => (T)info.Invoke(null, new object[] { element })
					);

				return parsers = methods;
			}
		}
	}
}
