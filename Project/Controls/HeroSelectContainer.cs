using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class HeroSelectContainer : UserControl
	{
		public readonly Hero Hero;

		public HeroSelectContainer(Hero hero)
		{
			InitializeComponent();
			
			Hero = hero;

			RecursivelyRegisterClick(this, HeroSelectContainer_Click);

			heroContainer.Sprite = hero;
			desc.Text = hero.Description;
		}

		public bool Chosen { get; private set; }

		private void HeroSelectContainer_Click(object sender, EventArgs e)
		{
			ToggleChosen();
		}

		private void HeroSelectContainer_Paint(object sender, PaintEventArgs e)
		{
			if (Chosen)
				e.Graphics.DrawRectangle(new Pen(Brushes.OrangeRed, 3), 1, 1, ClientSize.Width - 3, ClientSize.Height - 3);
			else
				e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
		}

		private void ToggleChosen()
		{
			Chosen = !Chosen;
			if (ChosenChanged != null) ChosenChanged(this);
			Invalidate();
		}

		public event Action<HeroSelectContainer> ChosenChanged;

		private static void RecursivelyRegisterClick(Control parentControl, EventHandler handler)
		{
			parentControl.Click += handler;
			foreach (var control in parentControl.Controls)
			{
				if (control is Control)
					RecursivelyRegisterClick(control as Control, handler);
			}
		}
	}
}
