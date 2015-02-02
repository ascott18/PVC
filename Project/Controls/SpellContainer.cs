using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Spells;

namespace Project.Controls
{
	internal partial class SpellContainer : UserControl
	{
		private Spell spell;

		public SpellContainer()
		{
			InitializeComponent();

			SetStyle(ControlStyles.StandardClick, true);
			SetStyle(ControlStyles.StandardDoubleClick, true);

			spellName.MouseClick += label_Click;
			spellName.MouseDoubleClick += label_DoubleClick;
			Paint += SpellContainer_Paint;
			Hide();
		}

		void label_DoubleClick(object sender, MouseEventArgs e)
		{
			OnMouseDoubleClick(e);
		}

		void label_Click(object sender, MouseEventArgs e)
		{
			OnMouseClick(e);
		}

		void SpellContainer_Paint(object sender, PaintEventArgs e)
		{
			if (spell.RemainingCastTime > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.Orange), 
				                         new Rectangle(
					                         0, 0, 
					                         Width - (int)(Width * (spell.RemainingCastTime / spell.CastDuration)), 
					                         Height)
				);
			}
			else if (spell.RemainingCooldown > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray),
				                         new Rectangle(
					                         0, 0,
					                         (int)(Width * (spell.RemainingCooldown / spell.CooldownDuration)),
					                         Height)
				);
			}

		}

		public Spell Spell
		{
			get { return spell; }
			set
			{
				if (spell != null)
				{
					spell.StateChanging -= Spell_StateChanging;
					spell.AutoCastChanged -= spell_AutoCastChanged;
					castControlLabel.Text = "";
				}
				spell = value;
				if (spell == null)
				{
					Hide();
				}
				else
				{
					spellName.Text = spell.Name;
					spell.StateChanging += Spell_StateChanging;
					spell.AutoCastChanged += spell_AutoCastChanged;
					Show();
				}
			}
		}

		void spell_AutoCastChanged(Spell sender)
		{
			Invalidate();
		}

		void Spell_StateChanging(Spell sender)
		{
			switch (sender.State)
			{
				case Spell.CastState.Used:
					sender.Session.Update += Session_Update;
					break;
				case Spell.CastState.Unused:
					sender.Session.Update -= Session_Update;
					Invalidate();
					break;
			}
		}

		void Session_Update(CombatSession sender)
		{
			castControlLabel.Text = "";

			if (spell.IsAutoCast)
			{
				castControlLabel.Text = "*";
			}
			else if (spell.Session != null)
			{
				var queuedSpells = spell.Session.GetQueuedSpells(spell.Owner);
				if (queuedSpells.Contains(spell))
				{
					var queueIndex = queuedSpells.TakeWhile(x => x != spell).Count() + 1;

					castControlLabel.Text = queueIndex.ToString();
				}
			}

			if (spell.State != Spell.CastState.Unused)
				Invalidate();
		}
	}
}
