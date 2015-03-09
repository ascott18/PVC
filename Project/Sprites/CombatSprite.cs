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
	public abstract class CombatSprite
	{
		public readonly List<Spell> Spells = new List<Spell>();
		private Attributes attributes;

		private int health = 1;
		private int maxHealth = 1;
		public readonly DungeonSprite Parent;
		public List<Aura> Auras = new List<Aura>();

		protected CombatSprite(DungeonSprite parent)
		{
			HealthChanged += CombatSprite_HealthChanged;
		    Parent = parent;
		}

		public Image Image { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }

		public Spell CurrentCast
		{
			get { return Spells.FirstOrDefault(spell => spell.IsCasting); }
		}

		public virtual int MinHealth
		{
			get { return 0; }
		}

		public int Health
		{
			get { return health; }
			set
			{
				var oldHealth = health;
				health = Math.Max(value, MinHealth); // Health can't be < MinHealth
			    health = Math.Min(health, MaxHealth);
				
				if (HealthChanged != null && oldHealth != health) 
					HealthChanged(this);
			}
		}

		public bool IsDead
		{
			get { return Health == 0; }
		}

		public virtual bool IsActive
		{
			get { return !IsDead; }
		}

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
				var newMaxHealth = attributes.Stamina * healthPerStamina;
				var healthScaleFactor = (double)newMaxHealth / MaxHealth;

				MaxHealth = newMaxHealth;
				Health = (int)(Health * healthScaleFactor);

				OnAttributesChanged();
			}
		}

		private void CombatSprite_HealthChanged(CombatSprite sender)
		{
			if (!IsActive && CurrentCast != null)
				CurrentCast.Cancel();
		}

		public event SpriteEvent HealthChanged;

		/// <summary>
		///     Recalculates the attributes of the sprite.
		/// </summary>
		public void RecalculateAttributes()
		{
			var args = new SpriteAttributesRecalcEventArgs
			{
				Attributes = BaseAttributes
			};

			if (RecalculatingAttributes != null)
				RecalculatingAttributes(this, args);

			Attributes = args.Attributes * args.AttributesMultiplier;
		}

		/// <summary>
		/// Fires when the Sprite is recalculating its attibutes.
		/// Modify the event args to modify the sprite's stats.
		/// </summary>
		public event SpriteAttributesRecalcEvent RecalculatingAttributes;

		protected void OnAttributesChanged()
		{
			if (AttributesChanged != null) AttributesChanged(this);
		}
		public event SpriteEvent AttributesChanged;

		/// <summary>
		///     Parses data from an XElement that are shared between all CombatSprite subclasses.
		/// </summary>
		/// <param name="element">The XElement to parse the shared data from.</param>
		protected void ParseCommonAttributes(XElement element)
		{
			Image = XmlData.LoadImage(element.Attribute("texture").Value);
			Name = element.Attribute("name").Value;

			var description = element.Attribute("desc");
			if (description != null) Description = description.Value;

			BaseAttributes = Attributes.Parse(element.Element("Attributes"));

			foreach (var spellElement in element.XPathSelectElements("Spell"))
			{
				Spells.Add(Spell.GetSpell(this, (int)spellElement.Attribute("id")));
			}
		}
	}

	public delegate void SpriteAttributesRecalcEvent(CombatSprite sender, SpriteAttributesRecalcEventArgs args);

	public class SpriteAttributesRecalcEventArgs
	{
		public Attributes Attributes = new Attributes();
		public AttributesMultiplier AttributesMultiplier = new AttributesMultiplier(1, 1, 1);
	}

	public delegate void SpriteEvent(CombatSprite sender);
}
