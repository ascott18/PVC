using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class StatsScreen : Form
	{
		private Party party;
		private List<HeroStatsContainer> hsContainers = new List<HeroStatsContainer>();

		// Implements Singleton.
		private static StatsScreen instance;

		/// <summary>
		/// Gets the singleton instance of StatsScreen.
		/// </summary>
		public static StatsScreen Instance
		{
			get
			{
				if (instance != null)
					return instance;
				return instance = new StatsScreen();
			}
		}

		private StatsScreen()
		{
			instance = this;
			InitializeComponent();
			
			//TODO: magic number right here needs to be a constant in the Party class.
			for (int i = 0; i < 3; i++)
			{
				var heroStats = new HeroStatsContainer();
				heroStats.Location = new Point(5 + (i * heroStats.Width), 5);
				hsContainers.Add(heroStats);
				Controls.Add(heroStats);
			}
		}


		public Party Party
		{
			get { return party; }
			set
			{
				if (party != null)
				{
					foreach (var heroStatContainer in hsContainers)
						heroStatContainer.Hero = null;
				}

				party = value;
				if (party != null)
				{
					for (int i = 0; i < hsContainers.Count; i++)
					{
						var heroStatContainer = hsContainers[i];
						Hero hero = null;
						if (i < party.Members.Count)
							hero = party.Members[i] as Hero;

						heroStatContainer.Hero = hero;
					}
				}
			}
		}
	}
}
