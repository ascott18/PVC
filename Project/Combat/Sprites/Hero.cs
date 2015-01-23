using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Hero : CombatSprite
	{
		private readonly ItemEquippable[] equipment = new ItemEquippable[ItemEquippable.MAX_SLOTS];
	}
}
