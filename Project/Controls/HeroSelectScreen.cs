using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.XPath;
using Project.Data;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class HeroSelectScreen : Form
	{
		private readonly List<HeroSelectContainer> containers = new List<HeroSelectContainer>();

		private HeroSelectScreen()
		{
			InitializeComponent();

			finishedButton.Enabled = false;

			var xDoc = XmlData.GetDocument("Heroes");

			foreach (var element in xDoc.XPathSelectElements("Heroes/Hero"))
			{
				var hero = new Hero(null, (int)element.Attribute("id"));
				var container = new HeroSelectContainer(hero);
				container.ChosenChanged += container_ChosenChanged;
				containers.Add(container);
				flowLayoutPanel1.Controls.Add(container);
			}
		}

		private void container_ChosenChanged(HeroSelectContainer obj)
		{
			var chosen = GetSelectedHeroIDs();

			int index = 0;
			foreach (var heroSelectContainer in containers.OrderBy(c => c.ChosenTime))
			{
				if (heroSelectContainer.Chosen)
					heroSelectContainer.ChosenIndex = ++index;
			}

			finishedButton.Enabled = chosen.Any() && chosen.Count() <= 3;
		}

		public static IEnumerable<int> BuildParty()
		{
			var dialog = new HeroSelectScreen();
			dialog.ShowDialog();

			return dialog.GetSelectedHeroIDs();
		}

		private IEnumerable<int> GetSelectedHeroIDs()
		{
			return (from heroSelectContainer in containers
			        where heroSelectContainer.Chosen
			        orderby heroSelectContainer.ChosenIndex
			        select heroSelectContainer.Hero.HeroID);
		}

		private void finishedButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
