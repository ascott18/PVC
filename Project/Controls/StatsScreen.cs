using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class StatsScreen : Form
	{
		private readonly List<HeroStatsContainer> hsContainers = new List<HeroStatsContainer>();
		private Party party;

		public StatsScreen()
		{
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
