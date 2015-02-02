using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	internal abstract class Spell
	{
		private readonly int spellID;

		public double LastCastTime { get; protected set; }
		public double CooldownDuration { get; protected set; }

		public double CastStartTime { get; protected set; }
		public double CastDuration { get; protected set; }

		public CombatSession Session { get; private set; }
		public CombatSprite Caster { get; private set; }

		public virtual bool CanCast
		{
			get
			{
				if (Session == null)
					return true; // hasn't been cast at all yet.

				if (LastCastTime > 0 && LastCastTime + CooldownDuration > Session.GetTime())
					return false;

				return true;
			}
		}

		/// <summary>
		/// Returns whether or not this spell in in progress.
		/// </summary>
		public bool IsCasting
		{
			get { return State == CastState.Started; }
		}

		public double RemainingCastTime
		{
			get
			{
				if (state != CastState.Started)
					return 0;

				return CastDuration - (Session.GetTime() - CastStartTime);
			}
		}

		public double RemainingCooldown
		{
			get
			{
				if (state == CastState.Unused)
					return 0;

				return CooldownDuration - (Session.GetTime() - LastCastTime);
			}
		}

		/// <summary>
		/// Represents the states that a spell can be in.
		/// </summary>
		public enum CastState
		{
			/// <summary>
			/// Spell is not attached to a CombatSession or Caster.
			/// </summary>
			Unused,

			/// <summary>
			/// Spell is attached to a CombatSession and Caster. Will immediately transition to Ready.
			/// </summary>
			Used,

			/// <summary>
			/// Spell is ready to start casting. Call Start().
			/// </summary>
			Ready,

			/// <summary>
			/// Spell has started casting, and is processing events before transitioning to Started (unless Cancel() is called).
			/// </summary>
			Starting,

			/// <summary>
			/// Spell is currently casting. Will continue until complete, or until Cancel() is called.
			/// </summary>
			Started,

			/// <summary>
			/// Spell finished casting, and is processing events before transitioning to Ready.
			/// </summary>
			Finishing,

			/// <summary>
			/// Cancel() was called. Spell is processing events before transitioning to Ready.
			/// </summary>
			Canceling,
		}

		private CastState state = CastState.Unused;

		/// <summary>
		/// Represents the current state of the spell.
		/// </summary>
		public CastState State
		{
			get { return state; }
			private set
			{
				if (state == value) return;

				state = value;

				if (StateChanging != null) StateChanging(this);

				if (StateChanged != null) StateChanged(this);
			}
		}

		public string Name { get; private set; }

		/// <summary>
		/// Constructs a spell, parsing base information that all spells have.
		/// </summary>
		/// <param name="data">The XElement to parse data from.</param>
		protected Spell(XElement data)
		{
			Name = (string)data.Attribute("name");
			spellID = (int)data.Attribute("id");
			CastDuration = (int)data.Attribute("castTime");
			CooldownDuration = (int)data.Attribute("cooldown");

			StateChanging += Spell_StateStateChanging;
			StateChanged += Spell_StateStateChanged;
		}

		private void Spell_StateStateChanging(Spell sender)
		{
			switch (State)
			{
				case CastState.Finishing:
					LastCastTime = Session.GetTime();
					break;

				case CastState.Ready:
					CastStartTime = 0;
					Session.Update -= Session_Update;
					break;

				case CastState.Started:
					CastStartTime = Session.GetTime();
					Session.Update += Session_Update;
					break;

				case CastState.Used:
					Session.StateChanged += Session_StateChanged;
					break;

				case CastState.Unused:
					LastCastTime = 0;
					Session.StateChanged -= Session_StateChanged;
					break;
			}
		}

		private void Spell_StateStateChanged(Spell sender)
		{
			switch (State)
			{
				case CastState.Unused:
					Session = null;
					Caster = null;
					break;
			}
		}

		/// <summary>
		/// Fires when the State of the spell changes. Fires before StateChanged.
		/// </summary>
		public event SpellEvent StateChanging;

		/// <summary>
		/// Fires when the State of the spell changes. Fires after StateChanging.
		/// </summary>
		public event SpellEvent StateChanged;


		/// <summary>
		/// Constructs an appropriate subclass of Spell based on the spellID recieved.
		/// </summary>
		/// <param name="spellID">The spellID of the spell to parse from Spells.xml and create.</param>
		/// <returns>A new instance of Spell.</returns>
		public static Spell GetSpell(int spellID)
		{
			var methods = XmlData.XmlParsable<Spell>.GetParsers();

			var itemsDoc = XmlData.GetDocument("Spells");
			var spellElement = itemsDoc.XPathSelectElement(String.Format("Spells/*[@id={0}]", spellID));

			if (spellElement == null)
				throw new InvalidDataException("No spell found with ID " + spellID);

			var elementName = spellElement.Name.ToString();

			if (!methods.ContainsKey(elementName))
				throw new Exception("Missing parser for spell type " + elementName);

			var parserMethod = methods[elementName];

			return parserMethod(spellElement);
		}

		private void Session_Update(CombatSession sender)
		{
			if (State == CastState.Started)
			{
				if (CastStartTime + CastDuration < Session.GetTime())
				{
					// The game timer has exceeded the ending time of the cast,
					// so trigger its completion.
					State = CastState.Finishing;

					if (State != CastState.Finishing)
						throw new Exception("Invalid state transition from Finishing to " + State);

					State = CastState.Ready;
				}
			}
		}

		private void Session_StateChanged(CombatSession sender)
		{
			if (sender.State != CombatSession.CombatState.Ended)
				return;

			if (State == CastState.Finishing)
				StateChanged += SetUnusedUponReady;
			else
			{
				Cancel();

				State = CastState.Unused;
			}
		}

		private void SetUnusedUponReady(Spell sender)
		{
			if (State == CastState.Ready)
			{
				StateChanged -= SetUnusedUponReady;
				State = CastState.Unused;
			}
			else
			{
				throw new Exception("Expected state change from finishing to ready");
			}


		}

		/// <summary>
		/// Cancels a spellcast that is starting or started.
		/// </summary>
		public void Cancel()
		{
			if (State != CastState.Starting && State != CastState.Started)
				return;

			State = CastState.Canceling;

			if (State != CastState.Canceling)
				throw new Exception("Invalid state transition from Canceling to " + State);
			
			State = CastState.Ready;
		}

		/// <summary>
		/// Starts the spell casting for the given CombatSession and casting CombatSprite.
		/// </summary>
		/// <param name="session">The CombatSession that the cast exists in.</param>
		/// <param name="caster">The caster of the spell.</param>
		/// <returns>True if the spell successfully started, otherwise false.</returns>
		public bool Start(CombatSession session, CombatSprite caster)
		{
			if (session.State == CombatSession.CombatState.Ended)
				return false;

			if (State == CastState.Unused)
			{
				Session = session;
				Caster = caster;
				State = CastState.Used;
				State = CastState.Ready;
			}

			if (State != CastState.Ready)
				return false;

			if (!CanCast)
				return false;

			if (caster.CurrentCast != null)
				return false;



			State = CastState.Starting;
			
			// Check the state again, to make sure it wasn't canceled.
			if (State == CastState.Starting)
			{
				State = CastState.Started;
				return true;
			}

			return false;
		}
	}

	internal delegate void SpellEvent(Spell sender);
}
