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


		private int _health;
		public int Health
		{
			get { return _health; }
			set
			{
				_health = value;
				if (HealthChanged != null) HealthChanged(this);
			}
		}
		public event SpriteEvent HealthChanged;

		public int MaxHealth { get; private set; }

		public Attributes BaseAttributes { get; private set; }

		public virtual Attributes Attributes {
			get { return BaseAttributes; }
		}

		/// <summary>
		/// Parses data from an XElement that are shared between all CombatSprite subclasses.
		/// </summary>
		/// <param name="element">The XElement to parse the shared data from.</param>
		protected void ParseCommonAttributes(XElement element)
		{
			Image = XmlData.LoadImage(element.Attribute("texture").Value);

			Name = element.Attribute("name").Value;


			BaseAttributes = Attributes.ParseAttributes(element.Element("Attributes"));
		}
	}

	public struct Attributes
	{
		public int Stamina;
		public int Strength;
		public int Intellect;
		public int Dexterity;

		public static Attributes operator +(Attributes a1, Attributes a2)
		{
			return new Attributes()
			{
				Stamina = a1.Stamina + a2.Stamina,
				Strength = a1.Strength + a2.Strength,
				Intellect = a1.Intellect + a2.Intellect,
				Dexterity = a1.Dexterity + a2.Dexterity
			};
		}

		public static Attributes ParseAttributes(XElement element)
		{
			return new Attributes()
			{
				Stamina = int.Parse(element.Attribute("stamina").Value),
				Strength = int.Parse(element.Attribute("strength").Value),
				Intellect = int.Parse(element.Attribute("intellect").Value),
				Dexterity = int.Parse(element.Attribute("dexterity").Value)
			};
		}
	}

	internal delegate void SpriteEvent(CombatSprite sender);
}
