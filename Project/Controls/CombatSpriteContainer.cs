using System;
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
		private CombatSprite target;

		public CombatSpriteContainer()
		{
			InitializeComponent();
			CheckTargetImageEnabled();
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
					sprite_HealthChanged(sprite);

					for (int i = 0; i < Math.Min(SpellContainers.Count, sprite.Spells.Count); i++)
					{
						var spellContainer = SpellContainers[i];

						spellContainer.Spell = sprite.Spells[i];
					}
				}
			}
		}

		public CombatSprite Target
		{
			get { return target; }
			set
			{
				target = value;

				targetImage.Image = target != null ? target.Image : null;
				CheckTargetImageEnabled();
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

		private void CheckTargetImageEnabled()
		{
			targetImage.Visible = target != null && Enabled;

		}

		private void targetImage_EnabledChanged(object sender, EventArgs e)
		{
			CheckTargetImageEnabled();
		}
	}
}
