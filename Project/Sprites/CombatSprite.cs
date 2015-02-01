using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;
using Project.Spells;

namespace Project.Sprites
{
	class CombatSprite
	{
		public Image Image { get; protected set; }
		public string Name { get; protected set; }

		public readonly List<Spell> Spells = new List<Spell>();

		
		public Spell CurrentCast
		{
			get { return Spells.FirstOrDefault(spell => spell.IsCasting); }
		}

		private int health = 1;
		private int maxHealth = 1;
		private Attributes attributes;

		public virtual int MinHealth
		{
			get { return 0; }
		}

		public int Health
		{
			get { return health; }
			set
			{
				health = Math.Max(value, MinHealth); // Health can't be < MinHealth
				if (HealthChanged != null) HealthChanged(this);
			}
		}
		public event SpriteEvent HealthChanged;

		public bool IsDead { get { return Health == 0; } }

		public virtual bool IsActive { get { return !IsDead; } }

		public int MaxHealth
		{
			get { return maxHealth; }
			private set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException("value", value, "MaxHealth cannot be less than 1");

				maxHealth = value;
				if (HealthChanged != null) HealthChanged(this);
			}
		}

		protected Attributes BaseAttributes { get; set; }

		public Attributes Attributes
		{
			get { return attributes; }
			protected set
			{
				attributes = value;
				const int healthPerStamina = 10;

				// Scale up the current health so that the percent health remains the same
				// before and after the adjustment to it.
				var newMaxHealth = attributes.Stamina*healthPerStamina;
				var healthScaleFactor = (double)newMaxHealth/MaxHealth;
				Health = (int)(Health * healthScaleFactor);

				MaxHealth = newMaxHealth;
			}
		}

		/// <summary>
		/// Recalculates the attributes of the sprite. Should be overridden by subclasses.
		/// </summary>
		public virtual void RecalculateAttributes()
		{
			Attributes = BaseAttributes;
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

			foreach (var spellElement in element.XPathSelectElements("Spell"))
			{
				Spells.Add(Spell.GetSpell((int)spellElement.Attribute("id")));
			}
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
			return new Attributes
			{
				Stamina = a1.Stamina + a2.Stamina,
				Strength = a1.Strength + a2.Strength,
				Intellect = a1.Intellect + a2.Intellect,
				Dexterity = a1.Dexterity + a2.Dexterity
			};
		}

		public static Attributes ParseAttributes(XElement element)
		{
			if (element == null) throw new ArgumentNullException("element");

			return new Attributes
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
