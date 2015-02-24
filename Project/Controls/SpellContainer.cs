using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Spells;

namespace Project.Controls
{
	/// <summary>
	///     Displays information about the current cast time and cooldown time of a spell.
	///     Use the controls events to cast the associated spell.
	/// </summary>
	internal partial class SpellContainer : UserControl
	{
		private Spell spell;

		public SpellContainer()
		{
			InitializeComponent();

			SetStyle(ControlStyles.StandardClick, true);
			SetStyle(ControlStyles.StandardDoubleClick, true);

			Paint += SpellContainer_Paint;
			Hide();
		}

		/// <summary>
		///     Gets and sets the spell displayed by this SpellContainer.
		/// </summary>
		public Spell Spell
		{
			get { return spell; }
			set
			{
				if (spell != null)
				{
					spell.StateChanging -= Spell_StateChanging;
					spell.AutoCastChanged -= spell_AutoCastChanged;
					toolTip.SetToolTip(this, null);
				}
				spell = value;
				if (spell == null)
				{
					Hide();
				}
				else
				{
					spell.StateChanging += Spell_StateChanging;
					spell.AutoCastChanged += spell_AutoCastChanged;
					toolTip.SetToolTip(this, spell.GetTooltip());
					Show();
				}
			}
		}

		private void label_DoubleClick(object sender, MouseEventArgs e)
		{
			OnMouseDoubleClick(e);
		}

		private void label_Click(object sender, MouseEventArgs e)
		{
			OnMouseClick(e);
		}

		private void SpellContainer_Paint(object sender, PaintEventArgs e)
		{
			if (spell.RemainingCastTime > 0)
			{
				// Draw the castbar if the spell is being cast.
				e.Graphics.FillRectangle(Brushes.Orange,
				                         new Rectangle(
					                         0, 0,
					                         Width - (int)(Width * (spell.RemainingCastTime / spell.CastDuration)),
					                         Height)
					);
			}
			else if (spell.RemainingCooldown > 0)
			{
				// Draw the cooldown bar if the spell is on cooldown.
				e.Graphics.FillRectangle(Brushes.DarkGray, 
				                         new Rectangle(
					                         0, 0,
					                         (int)(Width * (spell.RemainingCooldown / spell.CooldownDuration)),
					                         Height)
					);
			}

			// Set the labels on the spell bar for its queue position/autocast state.
			var castControlLabel = "";
			if (spell.IsAutoCast)
			{
				castControlLabel = "*";
			}
			else if (spell.Session != null)
			{
				var queuedSpells = spell.Session.GetQueuedSpells(spell.Owner);
				if (queuedSpells.Contains(spell))
				{
					var queueIndex = queuedSpells.TakeWhile(x => x != spell).Count() + 1;

					castControlLabel = queueIndex.ToString();
				}
			}

			e.Graphics.DrawString(castControlLabel, Font, Brushes.Black, 0, 0);

			e.Graphics.DrawString(Spell.Name, Font, Brushes.Black, 12, 0);
		}

		private void spell_AutoCastChanged(Spell sender)
		{
			Refresh();
		}

		private void Spell_StateChanging(Spell sender, Spell.CastState newState)
		{
			// Listen to the combat session's Update event to
			// redraw the control while the spell is in use.
			switch (sender.State)
			{
				case Spell.CastState.Used:
					sender.Session.Update += Session_Update;
					break;
				case Spell.CastState.Unused:
					sender.Session.Update -= Session_Update;
					Refresh();
					break;
			}
		}

		private void Session_Update(CombatSession sender)
		{
			if (spell.State != Spell.CastState.Unused)
				Refresh();
		}
	}
}
