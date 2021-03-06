﻿using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;

namespace Project.Spells
{
	internal class SpellSelfHealing : Spell
	{
		private readonly int healing;

		protected SpellSelfHealing(XElement data) : base(data)
		{
			healing = (int)data.Attribute("healing");

			StateChanged += SpellTargetedHealing_StateChanged;
		}

		private void SpellTargetedHealing_StateChanged(Spell sender, CastState oldState)
		{
			switch (State)
			{
				case CastState.Starting:
					break;

				case CastState.Finishing:
					DoComboAction(Heal, Owner, Owner, healing);
					ApplyAuras(Owner);

					break;
			}
		}


		[XmlData.XmlParserAttribute("SelfHealing")]
		public static Spell Create(XElement data)
		{
			return new SpellSelfHealing(data);
		}

		protected override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine("");
			sb.Append("Self Heal: ");
			sb.AppendLine(healing.ToString());
		}
	}
}
