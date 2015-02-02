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

			label.Click += label_Click;
			Paint += SpellContainer_Paint;
			Hide();
		}

		void label_Click(object sender, EventArgs e)
		{
			OnClick(e);
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
					
				}
				spell = value;
				if (spell == null)
				{
					Hide();
				}
				else
				{
					label.Text = spell.Name;
					spell.StateChanging += Spell_StateChanging;
					Show();
				}
			}
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
			if (spell.State != Spell.CastState.Unused)
				Invalidate();
		}
	}
}
