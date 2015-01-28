using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Project
{
	abstract class Spell
	{
		public double LastCastTime { get; protected set; }
		public double CooldownDuration { get; protected set; }

		public double CastStartTime { get; protected set; }
		public double CastDuration { get; protected set; }

		public CombatSession Session { get; protected set; }
		public CombatSprite Caster { get; protected set; }

		public bool IsCasting {
			get { return CastStartTime > 0; }
		}


		public static Spell GetSpell(int spellID)
		{
			var methods = XmlData.XmlParsable<Spell>.GetParsers();

			var itemsDoc = XmlData.GetDocument("Spells");
			var itemElement = itemsDoc.XPathSelectElement(String.Format("Spells/*[@id={0}]", spellID));

			var elementName = itemElement.Name.ToString();

			if (!methods.ContainsKey(elementName))
				throw new Exception("Missing parser for spell type " + elementName);

			var parserMethod = methods[elementName];

			return parserMethod(itemElement);
		}

		protected virtual void OnCastUpdate()
		{
			if (CastStartTime + CastDuration < Session.GetTime())
				OnCastFinish();
		}

		protected virtual void OnCastStart(CombatSession session, CombatSprite caster)
		{
			CastStartTime = session.GetTime();
			Session = session;
			Caster = caster;

			session.Ended += session_Ended;
			session.Update += session_Update;
		}

		private void session_Update(CombatSession sender)
		{
			if (IsCasting)
				OnCastUpdate();
		}

		protected virtual void OnCastFinish()
		{
			CastStartTime = 0;
		}

		public virtual bool CanCast
		{
			get
			{
				if (Session == null)
					return false;

				if (LastCastTime + CooldownDuration < Session.GetTime())
					return false;

				return true;
			}
		}

		private void session_Ended(CombatSession sender)
		{
			LastCastTime = Session.GetTime();
			CastStartTime = 0;
			Session = null;
		}

		public void Start(CombatSession arena, CombatSprite caster)
		{
			if (IsCasting)
				throw new Exception("Spell is already casting");

			if (caster.CurrentCast != null)
				throw new Exception("Caster is already casting");

			OnCastStart(arena, caster);
		}
	}
}
