using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	abstract class Item
	{
		public readonly int ItemID;

		public Item(int itemId)
		{
			ItemID = itemId;
		}
	}
}
