using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Project.Data;
using Project.Sprites;

namespace Project.Spells
{
	/// <summary>
	///     Spell is an abstract class that acts as a base for all spells in the game.
	///     Spell instances are owned by a single CombatSprite, and are shared between
	///     all CombatSessions that the owner participates in. Spells are represented as
	///     finite state machines with well-defined state transitions. A diagram
	///     of possible state transitions is located in UML/CastStates.activitydiagram.
	/// </summary>
	public abstract class Spell
	{
		/// <summary>
		///     Represents the states that a spell can be in.
		/// </summary>
		public enum CastState
		{
			/// <summary>
			///     Spell is not attached to a CombatSession.
			/// </summary>
			Unused,

			/// <summary>
			///     Spell is attached to a CombatSession. Will immediately transition to Ready.
			/// </summary>
			Used,

			/// <summary>
			///     Spell is ready to start casting. Call Start().
			/// </summary>
			Ready,

			/// <summary>
			///     Spell has started casting, and is processing events before transitioning to Started (unless Cancel() is called).
			/// </summary>
			Starting,

			/// <summary>
			///     Spell is currently casting. Will continue until complete, or until Cancel() is called.
			/// </summary>
			Started,

			/// <summary>
			///     Spell finished casting, and is processing events before transitioning to Ready.
			/// </summary>
			Finishing,

			/// <summary>
			///     Cancel() was called. Spell is processing events before transitioning to Ready.
			/// </summary>
			Canceling,
		}

		private static readonly Random rand = new Random();
		public readonly IReadOnlyList<XElement> AuraElements;

		private readonly int spellID;
		protected string TooltipCache;
		private bool isAutoCast;
		private CastState state = CastState.Unused;

		/// <summary>
		///     Constructs a spell, parsing base information that all spells have.
		/// </summary>
		/// <param name="data">The XElement to parse data from.</param>
		protected Spell(XElement data)
		{
			Name = (string)data.Attribute("name");
			spellID = (int)data.Attribute("id");
			CastDuration = (double)data.Attribute("castTime");
			CooldownDuration = (double)data.Attribute("cooldown");
			AuraElements = data.XPathSelectElements("Auras/*").ToList().AsReadOnly();

			StateChanging += Spell_StateStateChanging;
			StateChanged += Spell_StateStateChanged;
		}

		/// <summary>
		///     The last time that this spell was cast,
		///     relative to the current combat session's timer.
		/// </summary>
		public double LastCastTime { get; protected set; }

		/// <summary>
		///     The length of the spell's cooldown, in seconds,
		///     from the time that the spell is last cast.
		///     This is not the remaining time on the cooldown.
		/// </summary>
		public double CooldownDuration { get; protected set; }

		/// <summary>
		///     The time at which this spell began casting,
		///     reltiave to the current combat session's timer.
		/// </summary>
		public double CastStartTime { get; protected set; }

		/// <summary>
		///     The length of the spell's cast, in seconds.
		///     This is not the remaining time left on the current cast.
		/// </summary>
		public double CastDuration { get; protected set; }

		/// <summary>
		///     The current combat session that the spell is used in.
		///     This will be null if the spell has not yet been cast,
		///     or if the spell has not been cast since the last combat session
		///     it was associated with ended.
		/// </summary>
		public CombatSession Session { get; private set; }

		/// <summary>
		///     The CombatSprite that owns this spell, and who will be casting it.
		/// </summary>
		public CombatSprite Owner { get; private set; }

		/// <summary>
		///     Whether or not the spell is ready to be cast,
		///     based on its current cooldown.
		/// </summary>
		public virtual bool CanCast
		{
			get { return RemainingCooldown == 0; }
		}

		/// <summary>
		///     Returns whether or not this spell is currently casting.
		/// </summary>
		public bool IsCasting
		{
			get { return State == CastState.Started; }
		}

		/// <summary>
		///     The time, in seconds, remaining on the cast of this spell.
		///     This value is 0 if the spell is not currently casting.
		/// </summary>
		public double RemainingCastTime
		{
			get
			{
				if (state != CastState.Started)
					return 0;

				return Math.Max(0, CastDuration - (Session.GetTime() - CastStartTime));
			}
		}

		/// <summary>
		///     Returns the time remaining until this spell is able to be cast again.
		///     This value is 0 if it can be cast immediately.
		/// </summary>
		public double RemainingCooldown
		{
			get
			{
				if (state == CastState.Unused || LastCastTime == 0)
					return 0;

				return Math.Max(0, CooldownDuration - (Session.GetTime() - LastCastTime));
			}
		}

		/// <summary>
		///     Represents the current state of the spell.
		/// </summary>
		public CastState State
		{
			get { return state; }
			private set
			{
				if (state == value) return;

				if (StateChanging != null) StateChanging(this, value);

				var oldState = state;
				state = value;

				if (StateChanged != null) StateChanged(this, oldState);
			}
		}

		/// <summary>
		///     The name of this spell.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		///     Whether or not the owner of this spell should attempt to auto-cast this spell.
		/// </summary>
		public bool IsAutoCast
		{
			get { return isAutoCast; }
			set
			{
				isAutoCast = value;
				if (AutoCastChanged != null) AutoCastChanged(this);
			}
		}

		/// <summary>
		///     Fires when the autocast property of this spell is set.
		/// </summary>
		public event SpellEvent AutoCastChanged;


		private void Spell_StateStateChanging(Spell sender, CastState newState)
		{
			// Handles baseline state changes for all spell types,
			// like recording the last cast time, and registering events
			// to watch for cast completions.
			switch (newState)
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

		private void Spell_StateStateChanged(Spell sender, CastState oldState)
		{
			// This is handled in the StateChanged event instead of
			// the StateChanging event because we need to allow other interested
			// parties to have accesss to the current combat session before it
			// gets set null (they may need to unregister events from it, for e.g.).
			switch (State)
			{
				case CastState.Unused:
					Session = null;
					break;
			}
		}

		/// <summary>
		///     Fires when the State of the spell is about to change. Fires before StateChanged.
		///     The second parameter is the state that it is changing to.
		/// </summary>
		public event SpellStateEvent StateChanging;

		/// <summary>
		///     Fires when the State of the spell changes. Fires after StateChanging.
		///     The second parameter is the state that it changed from.
		/// </summary>
		public event SpellStateEvent StateChanged;


		/// <summary>
		///     Constructs an appropriate subclass of Spell based on the spellID recieved.
		/// </summary>
		/// <param name="owner">The CombatSprite that owns the spell.</param>
		/// <param name="spellID">The spellID of the spell to parse from Spells.xml and create.</param>
		/// <returns>A new instance of Spell.</returns>
		public static Spell GetSpell(CombatSprite owner, int spellID)
		{
			var spell = XmlData.XmlParserParseByID<Spell>("Spells", spellID);
			spell.Owner = owner;

			return spell;
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
			{
				// The combat session has ended, but the spell is currently finishing.
				// So, we need to wait to set the spell unused until the spell is ready,
				// since Finishing -> Unused is not a valid transition.

				// This happens when a spell finishing triggers the end of a combat session
				// by dealing fatal damage to one of the combatants. The state will end while the spell is 
				// still processing its Finishing state change.

				// Unregister first to make sure its only registered once.
				StateChanged -= SetUnusedUponReady;
				StateChanged += SetUnusedUponReady;
			}
			else
			{
				// If we aren't currently finishing a cast, attempt to cancel,
				// and then set unused. All possible state transitions here are valid.
				Cancel();

				State = CastState.Unused;
			}
		}

		private void SetUnusedUponReady(Spell sender, CastState oldState)
		{
			// See the Session_StateChanged method for an explanation of why this exists.

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
		///     Cancels a spellcast that is starting or started.
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
		///     Starts the spell casting for the given CombatSession and casting CombatSprite.
		/// </summary>
		/// <param name="session">The CombatSession that the cast exists in.</param>
		/// <returns>True if the spell successfully started, otherwise false.</returns>
		public bool Start(CombatSession session)
		{
			if (session.State == CombatSession.CombatState.Ended)
				return false;

			if (State == CastState.Unused)
			{
				Session = session;
				State = CastState.Used;
				State = CastState.Ready;
			}

			if (State != CastState.Ready)
				return false;

			if (!CanCast)
				return false;

			if (Owner.CurrentCast != null)
				return false;

			if (!Owner.IsActive)
				return false;

			if (Owner.Auras.OfType<AuraStun>().Any(aura => aura.Remaining > 0))
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


		public string GetTooltip()
		{
			if (TooltipCache != null)
				return TooltipCache;

			var sb = new StringBuilder();
			sb.AppendLine(Name);
			sb.AppendLine(String.Format("{0:F1} sec cast", CastDuration));
			sb.AppendLine(String.Format("{0:F1} sec cooldown", CooldownDuration));

			GetTooltip(sb);

			foreach (var aura in AuraElements.Select(Aura.CreateAura))
			{
				aura.GetTooltip(sb);
			}

			return TooltipCache = sb.ToString();
		}

		/// <summary>
		///     Populates the tooltip with spell-specific information.
		/// </summary>
		/// <param name="sb"></param>
		protected abstract void GetTooltip(StringBuilder sb);

		internal static void DoComboAction(Action<CombatSprite, int> method, CombatSprite caster, CombatSprite target, int hp)
		{
			int combo = caster.Attributes.Combo;

			method(target, hp);
			while (combo > 0)
			{
				int chance = rand.Next(0, 101);
				if (chance < combo)
				{
					method(target, hp);
				}
				combo -= 100;
			}
		}

		internal static void DoBlockableAction(Action<CombatSprite> method, CombatSprite target)
		{
			int block = target.Attributes.Block;
			int chance = rand.Next(0, 101);
			if (chance < block) // attack was blocked
				return;

			method(target);
		}

		internal static void Heal(CombatSprite target, int health)
		{
			if (target.IsActive)
				target.Health += health;
		}

		internal static void DealBlockableDamage(CombatSprite target, int damage)
		{
			int block = target.Attributes.Block;
			int chance = rand.Next(0, 101);
			if (chance < block) // attack was blocked
				return;

			DealUnblockableDamage(target, damage);
		}

		internal static void DealUnblockableDamage(CombatSprite target, int damage)
		{
			if (target.IsActive)
				target.Health -= damage;
		}

		/// <summary>
		///     Apply all of the auras associated with the spell to a target.
		/// </summary>
		/// <param name="target">The target to apply the auras to.</param>
		protected void ApplyAuras(CombatSprite target)
		{
			var auras = AuraElements.Select(auraElement => Aura.CreateAura(Session, Owner, auraElement));
			foreach (var aura in auras)
			{
				if (aura.IsBlockable)
					DoBlockableAction(aura.Apply, target);
				else
					aura.Apply(target);
			}
		}
	}

	/// <summary>
	///     A generic spell event.
	/// </summary>
	/// <param name="sender">The spell for which the event is firing.</param>
	public delegate void SpellEvent(Spell sender);

	/// <summary>
	///     A state change event for a spell. The state parameter varies per event
	///     implementation - refer to the docs for each event that uses this delegate
	///     for details.
	/// </summary>
	/// <param name="sender">The spell for which the state is changing.</param>
	/// <param name="state">
	///     Associated extra state information.
	///     See individual event implementations for detail.
	/// </param>
	public delegate void SpellStateEvent(Spell sender, Spell.CastState state);
}
