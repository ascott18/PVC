﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Items;

namespace Project
{
	internal partial class CombatCompleteDialog : Form
	{
		public CombatCompleteDialog(CombatSession session)
		{
			InitializeComponent();

			partyPortrait.Image = session.Party.Image;
			monsterPortrait.Image = session.MonsterPack.Image;
			StartPosition = FormStartPosition.CenterParent;
		}

		public void SetItems(IEnumerable<Item> items)
		{
			itemFlowPanel1.LoadItems(items);
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}