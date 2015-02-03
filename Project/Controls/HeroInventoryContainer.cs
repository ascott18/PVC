using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Items;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class HeroInventoryContainer : UserControl
	{
		private readonly List<ItemContainer> itemContainers = new List<ItemContainer>();
		private Hero hero;

		public HeroInventoryContainer()
		{
			InitializeComponent();

			for (int i = 0; i < ItemEquippable.NumSlots; i++)
			{
				var itemContainer = new ItemContainer
				{
					Width = equipmentFlow.Width - 2,
					Anchor = AnchorStyles.Left | AnchorStyles.Top
				};
				equipmentFlow.Controls.Add(itemContainer);
				itemContainers.Add(itemContainer);
			}
		}

		public Hero Hero
		{
			get { return hero; }
			set
			{
				if (hero != null)
				{
					hero.EquipmentChanged -= hero_EquipmentChanged;
					foreach (var itemContainer in itemContainers)
						itemContainer.Item = null;
				}
				hero = value;
				if (hero == null)
				{
					Hide();
				}
				else
				{
					Show();
					hero.EquipmentChanged += hero_EquipmentChanged;
					hero_EquipmentChanged(hero);
					name.Text = hero.Name;
					image.Image = hero.Image;
				}
			}
		}

		private void hero_EquipmentChanged(CombatSprite sender)
		{
			for (int i = 0; i < itemContainers.Count; i++)
			{
				var itemContainer = itemContainers[i];
				var item = Hero.GetEquippedItem((ItemEquippable.SlotID)i);
				itemContainer.Item = item;
			}
		}
	}
}
