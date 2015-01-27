using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project
{
	class CombatSprite
	{
		public Image Image { get; protected set; }
		public string Name { get; protected set; }

		/// <summary>
		/// Parses data from an XElement that are shared between all CombatSprite subclasses.
		/// </summary>
		/// <param name="element">The XElement to parse the shared data from.</param>
		protected void ParseCommonAttributes(XElement element)
		{
			Image = XMLData.LoadImage(element.Attribute("texture").Value);

			Name = element.Attribute("name").Value;
		}
	}
}
