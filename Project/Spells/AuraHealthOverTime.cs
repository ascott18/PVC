using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Sprites;

namespace Project.Spells
{
	abstract class AuraHealthOverTime : Aura
	{
		public int AmountTotal { get; protected set; }
		public int AmountRemaining { get; protected set; }

		public Action<CombatSprite, int> Action { get; protected set; }

		protected AuraHealthOverTime(XElement data) : base(data)
		{
			Removed += AuraHealthOverTime_Removed;
		}

		void AuraHealthOverTime_Removed(object sender, EventArgs e)
		{
			Spell.DoComboAction(Action, Source, Target, AmountRemaining);
		}

		protected override void OnUpdate()
		{
			var percentTimeRemaining = Remaining / Duration;
			var percentAmountDone = (double)AmountRemaining / AmountTotal;

			var percentToDo = percentAmountDone - percentTimeRemaining;

			var amountToDo = percentToDo * AmountTotal;
			amountToDo = Math.Min(amountToDo, AmountRemaining);

			var actualAmountToDo = (int)amountToDo;

			AmountRemaining -= actualAmountToDo;

			Spell.DoComboAction(Action, Source, Target, actualAmountToDo);
		}
	}
}
