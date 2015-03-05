using System;
using System.Linq;
using System.Xml.Linq;
using Project;
using Project.Dungeon;

namespace MapEdit
{
	class Controller : DungeonController
	{
		public void SetMap(int mapID)
		{
			CurrentMap = new DungeonMap(mapID, this);
		}
		public void SetMap(int mapID, XElement xelement)
		{
			CurrentMap = new DungeonMap(mapID, MapData.ParseMapData(mapID, xelement), this);
		}
	}
}
