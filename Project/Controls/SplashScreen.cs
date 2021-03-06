﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	public partial class SplashScreen : Form
	{
		private static readonly string[] jokeStrings = 
		{
			"(whatever that means)",
			"(no relation to polyvinyl chloride)",
			"(perhaps some sort of versus)",
			"(now with 20% more plasticity)",
			"(penniless vigilante collective)",
			"(passive venerable computers)",
		};

		public SplashScreen()
		{
			InitializeComponent();

			jokes.Text = jokeStrings[new Random().Next(jokeStrings.Length)];
		}

		private void newGameButton_Click(object sender, EventArgs e)
		{
			Hide();
			MainWindow.Window.CreateGame();

			MainWindow.Window.Show();
		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void debugStart_Click(object sender, EventArgs e)
		{
			MainWindow.Window.IsDebug = true;
			Hide();

			Party p = new Party(new Point(7, 7));
			int mapID = Convert.ToInt32(debugStartMapID.Text);
			p.AddHero(new Hero(p, 7));
			p.AddHero(new Hero(p, 5));
			p.AddHero(new Hero(p, 2));
			MainWindow.Window.CreateGame(p, mapID);


			MainWindow.Window.Show();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.F10)
			{
				debugStart.Visible = true;
				debugStartMapID.Visible = true;
				debugStartMapID.Focus();
				AcceptButton = debugStart;
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
