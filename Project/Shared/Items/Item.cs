using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Project
{
	abstract class Item
	{
		public readonly int ItemID;

		public Item(int itemId)
		{
			ItemID = itemId;
		}




		/// <summary>
		///     The cached result of GetXmlParserMethods(). Don't read this field - always call GetXmlParserMethods().
		/// </summary>
		private static Dictionary<string, Func<XElement, Item>> parserMethods;


		/// <summary>
		///     Gets a dictionary of all methods that have been marked with [ItemXmlParserAttribute(elementName)],
		///     with the keys of the dictionary as elementName and the values as Func&lt;XElement, Item&gt;
		/// </summary>
		/// <returns>The dictionary of XML pasing methods</returns>
		private static Dictionary<string, Func<XElement, Item>> GetXmlParserMethods()
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
								  .Where(info => info.GetCustomAttributes<Item.ItemXmlParserAttribute>().Any())

									// Create a Dictionary from the methods that were marked with this attribute.
								  .ToDictionary
				<MethodInfo, string, Func<XElement, Item>>(
				// The key to the Dictionary should be the elementName defined by the attribute.
					info => info.GetCustomAttributes<Item.ItemXmlParserAttribute>().First().ElementName,

					// Create a Func<XElement, TileObject> as the value of the dictionary.
					info => element => info.Invoke(null, new object[] { element }) as Item
				);

			return parserMethods = methods;
		}

		public static Item GetItem(int itemID)
		{
			var methods = GetXmlParserMethods();

			var itemsDoc = XMLData.GetDataXmlDocument("Items");
			var itemElement = itemsDoc.XPathSelectElement(String.Format("Items/*[@id={0}]", itemID));

			var elementName = itemElement.Name.ToString();

			if (!methods.ContainsKey(elementName))
				throw new Exception("Missing parser for item type " + elementName);

			var parserMethod = methods[elementName];

			return parserMethod(itemElement); ;
		}


		/// <summary>
		/// An attribute to be used on methods that will parse an incoming
		/// XElement with parent element name elementName that is a grandchild of an Item element
		/// in Items.xml. The method must return a new Item based on the data
		/// in the XElement.
		/// </summary>
		public class ItemXmlParserAttribute : Attribute
		{
			public readonly string ElementName;
			/// <summary>
			/// Declare a method as one that will take an incoming XElement, parse it,
			/// and return a new TileObject instance.
			/// </summary>
			/// <param name="elementName">The XElement.Name that this method will parse.</param>
			public ItemXmlParserAttribute(string elementName)
			{
				ElementName = elementName;
			}
		}
	}
}
