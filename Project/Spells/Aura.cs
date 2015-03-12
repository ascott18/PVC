using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	public abstract class Aura
	{
		protected Aura(XElement data)
		{
			Duration = (double)data.Attribute("duration");
			IsBlockable = (bool)data.Attribute("blockable");
		}

		/// <summary>
		///     The source of the aura
		/// </summary>
		public CombatSprite Source { get; protected set; }

		/// <summary>
		///     The target of the aura
		/// </summary>
		public CombatSprite Target { get; set; }

		/// <summary>
		///     The current combat session that the aura was used in.
		/// </summary>
		public CombatSession Session { get; private set; }

		/// <summary>
		///     The time at which this aura was applied,
		///     reltiave to the current combat session's timer.
		/// </summary>
		public double StartTime { get; private set; }

		/// <summary>
		///     The length of the aura's effect.
		/// </summary>
		public double Duration { get; protected set; }

		/// <summary>
		///     The time, in seconds, remaining on the aura.
		/// </summary>
		public double Remaining
		{
			get
			{
				if (StartTime == 0)
					return 0;

				return Math.Max(0, Duration - (Session.GetTime() - StartTime));
			}
		}

		/// <summary>
		///     Determines whether or not the application of the aura can be blocked.
		/// </summary>
		public bool IsBlockable { get; protected set; }

		/// <summary>
		///     Occurs when the aura is applied to a target.
		/// </summary>
		public event EventHandler Applied;

		/// <summary>
		///     Occurs when the aura is removed from a target.
		/// </summary>
		public event EventHandler Removed;

		/// <summary>
		///     Apply the aura to a target. This may only be called once per instance.
		/// </summary>
		/// <param name="target">The target of the aura.</param>
		public void Apply(CombatSprite target)
		{
			if (Session == null)
				throw new InvalidOperationException("Can't apply an aura that wasn't created for a CombatSession.");

			if (Target != null)
				throw new InvalidOperationException("Can't apply an aura more than once");

			if (target == null)
				throw new InvalidOperationException("No target specified");

			Target = target;
			StartTime = Session.GetTime();
			Session.Update += Session_Update;
			Session.StateChanged += Session_StateChanged;
			Target.Auras.Add(this);

			if (Applied != null) Applied(this, new EventArgs());
		}

		private void Session_Update(CombatSession sender)
		{
			if (Remaining <= 0)
				Remove();
			else
				OnUpdate();
		}

		/// <summary>
		///     Override in subclasses to provide behavior each time the CombatSession updates.
		/// </summary>
		protected virtual void OnUpdate()
		{
			// Override for implementations.
		}

		/// <summary>
		///     Remove the aura from its target.
		/// </summary>
		public void Remove()
		{
			Session.Update -= Session_Update;
			Target.Auras.Remove(this);

			if (Removed != null) Removed(this, new EventArgs());

			Target = null;
		}


		/// <summary>
		///     Constructs an aura from the given XElement.
		/// </summary>
		/// <param name="session">The CombatSession to create the aura for.</param>
		/// <param name="caster">The caster of the aura.</param>
		/// <param name="auraElement">The element to parse the aura from.</param>
		/// <returns>A new instance of Aura.</returns>
		public static Aura CreateAura(CombatSession session, CombatSprite caster, XElement auraElement)
		{
			var aura = XmlData.XmlParserParseElement<Aura>(auraElement);
			aura.Session = session;
			aura.Source = caster;
			return aura;
		}

		void Session_StateChanged(CombatSession sender)
		{
			if (sender.State == CombatSession.CombatState.Ended)
			{
				Remove();
			}
		}


		/// <summary>
		///     Constructs an aura from the given XElement.
		/// </summary>
		/// <param name="auraElement">The element to parse the aura from.</param>
		/// <returns>A new instance of Aura.</returns>
		public static Aura CreateAura(XElement auraElement)
		{
			var aura = XmlData.XmlParserParseElement<Aura>(auraElement);
			return aura;
		}

		public abstract void GetTooltip(StringBuilder sb);
	}
}
