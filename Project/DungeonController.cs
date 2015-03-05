using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Controls;
using Project.Dungeon;
using Project.Sprites;

namespace Project
{
	/// <summary>
	///     DungeonController is a base class for classes that will act as the controller for a dungeon.
	///		This can include the controller for the actual game, and the map editor controller.
	/// </summary>
	public abstract class DungeonController
	{
		protected readonly Dictionary<int, DungeonMap> maps = new Dictionary<int, DungeonMap>();
		private DungeonMap currentMap;

		public DungeonController()
		{

		}

		/// <summary>
		///     The current DungeonMap being controlled.
		/// </summary>>
		public virtual DungeonMap CurrentMap
		{
			get { return currentMap; }
			protected set
			{
				currentMap = value;
			}
		}

		/// <summary>
		///     Gets a DungeonMap by ID. Loads it from XML if it has not
		///     already bene created for this Game.
		/// </summary>
		/// <param name="mapID">The ID of the map to get.</param>
		/// <returns>The requested DungeonMap.</returns>
		protected DungeonMap LoadDungeonMap(int mapID)
		{
			DungeonMap map;

			if (!maps.TryGetValue(mapID, out map))
			{
				map = maps[mapID] = new DungeonMap(mapID, this);
			}
			return map;
		}
	}
}