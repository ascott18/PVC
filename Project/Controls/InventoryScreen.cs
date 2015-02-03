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
	internal partial class InventoryScreen : UserControl
	{
		private Party party;
		private readonly List<HeroInventoryContainer> hiContainers = new List<HeroInventoryContainer>();

		public InventoryScreen()
		{
			InitializeComponent();

			//TODO: magic number right here needs to be a constant in the Party class.
			for (int i = 0; i < 3; i++)
			{
				var heroInventory = new HeroInventoryContainer();
				heroInventory.Location = new Point(10, 10 + (i * heroInventory.Height));
				hiContainers.Add(heroInventory);
				Controls.Add(heroInventory);
			}
		}


		public Party Party
		{
			get { return party; }
			set
			{
				if (party != null)
				{
					foreach (var heroInventoryContainer in hiContainers)
						heroInventoryContainer.Hero = null;
				}

				party = value;
				if (party != null)
				{
					for (int i = 0; i < hiContainers.Count; i++)
					{
						var heroInventoryContainer = hiContainers[i];
						Hero hero = null;
						if (i < party.Members.Count)
							hero = party.Members[i] as Hero;

						heroInventoryContainer.Hero = hero;
					}
				}
				
			}
		}
	}
}
