using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Properties;

namespace Project
{
	abstract class XMLData
	{
		private static readonly Dictionary<string, XDocument> Documents = new Dictionary<string, XDocument>();

		/// <summary>
		/// Gets the XML Document for the specified embedded resource.
		/// Loads it if it isn't already loaded.
		/// </summary>
		/// <returns>The XDocument representing the game's map data.</returns>
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
	}
}
