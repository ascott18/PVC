using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Properties;

namespace Project
{
	/// <summary>
	/// XMLData is an abstract class that is the foundation for immutable data objects parsed from XML.
	/// It contains functionality for loading XML files from the project's resx file and storing these as
	/// XDocuments for repeated use.
	/// </summary>
	abstract class XMLData
	{
		private static readonly Dictionary<string, XDocument> Documents = new Dictionary<string, XDocument>();

		/// <summary>
		/// Gets the XML Document for the specified embedded resource.
		/// Loads it if it isn't already loaded.
		/// </summary>
		/// <returns>The XDocument representing the requested resources.</returns>
		protected static XDocument GetDataXmlDocument(string resourceName)
		{
			XDocument doc;

			// See if we have already loaded this resource into an XDocument
			if (Documents.TryGetValue(resourceName, out doc))
				return doc;

			// convert string to stream
			var res = Resources.ResourceManager.GetString(resourceName);
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(res));

			// Load up the document
			doc = XDocument.Load(stream);

			// Save it, and return it.
			return Documents[resourceName] = doc;
		}

		internal static Image LoadImage(string imageName)
		{
			if (imageName == "")
			{
				return null;
			}

			// Try and get the embedded resource represented by the provided name.
			// Embedded resources are defined in Resources.resx
			var Image = Resources.ResourceManager.GetObject(imageName) as Bitmap;
			if (Image == null)
				throw new FileNotFoundException("Resource not found", imageName);

			return Image;
		}
	}
}
