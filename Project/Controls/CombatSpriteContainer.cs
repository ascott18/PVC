﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Project.Sprites;

namespace Project.Controls
{
	internal partial class CombatSpriteContainer : UserControl
	{
		private readonly List<SpellContainer> spellContainers = new List<SpellContainer>();

		private CombatSprite sprite;

		public CombatSpriteContainer()
		{
			InitializeComponent();
		}

		public IReadOnlyList<SpellContainer> SpellContainers { get; private set; }

		internal CombatSprite Sprite
		{
			get { return sprite; }
			set
			{
				if (sprite != null)
				{
					sprite.HealthChanged -= sprite_HealthChanged;
					foreach (var spellContainer in SpellContainers)
						spellContainer.Spell = null;
				}

				sprite = value;
				if (sprite == null)
				{
					Hide();
				}
				else
				{
					Show();
					AttributesContainer.Sprite = sprite;
					sprite.HealthChanged += sprite_HealthChanged;

					for (int i = 0; i < Math.Min(SpellContainers.Count, sprite.Spells.Count); i++)
					{
						var spellContainer = SpellContainers[i];

						spellContainer.Spell = sprite.Spells[i];
					}
				}
			}
		}

		private void sprite_HealthChanged(CombatSprite sender)
		{
			Enabled = Sprite.IsActive;
		}


		protected void InitializeSpellContainers(int x)
		{
			for (int i = 0; i < 3; i++)
			{
				var container = new SpellContainer
				{
					Location = new Point(x, 13 + (i * 17)),
					Name = "spellContainer" + i,
				};

				Controls.Add(container);
				spellContainers.Add(container);
			}
			SpellContainers = spellContainers.AsReadOnly();
			AttributesContainer.SendToBack();
		}
	}
}
