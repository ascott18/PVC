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
	class Game
	{
		public readonly MainWindow Window;

		public readonly Party Party;
		private readonly Dictionary<int, DungeonMap> maps = new Dictionary<int, DungeonMap>();

		private CombatSession currentSession;

		internal DungeonMap CurrentMap { get; private set; }

		private DungeonMap LoadDungeonMap(int mapID)
		{
			DungeonMap map;

			if (!maps.TryGetValue(mapID, out map))
			{
				map = maps[mapID] = new DungeonMap(mapID, this);
			}
			return map;
		}

		public void SetPartyLocation(int mapID, Point point)
		{
			DungeonMap map = LoadDungeonMap(mapID);

			Party.SetLocation(map.GetTile(point));
			CurrentMap = map;
		}

		public void EnterCombat(MonsterPack enemy)
		{
			if (InCombat)
				throw new InvalidOperationException("Can't enter combat while in combat");

			currentSession = new CombatSession(this, Party, enemy);
			Window.dungeonContainer.Hide();
			Window.combatArena.Show();
			Window.combatArena.CombatSession = currentSession;

			currentSession.StateChanged += Session_StateChanged;

			currentSession.StartCombat();
		}

		void Session_StateChanged(CombatSession sender)
		{
			Window.dungeonContainer.Show();
			Window.combatArena.Hide();

			foreach (var hero in Party.Members.Cast<Hero>())
			{
				if (hero.IsRetreated)
					hero.Health = hero.MaxHealth/10;
			}
		}

		public bool InCombat
		{
			get { return (currentSession != null && currentSession.State != CombatSession.CombatState.Ended); }
		}


		internal Game(MainWindow window)
		{
			Window = window;

			// TODO: Temp code
			Party = new Party(new Point(7, 7));
			Party.AddHero(new Hero(1));
			Party.AddHero(new Hero(2));

			SetPartyLocation(1, Party.InitialLocation);
		}

		public bool ProcessKey(Keys keyData)
		{
			if (InCombat)
				return false;

			switch (keyData)
			{
				case Keys.Up:
				case Keys.W:
					TryMoveParty(new Size(0, -1));
					return true;
				case Keys.Down:
				case Keys.S:
					TryMoveParty(new Size(0, 1));
					return true;
				case Keys.Left:
				case Keys.A:
					TryMoveParty(new Size(-1, 0));
					return true;
				case Keys.Right:
				case Keys.D:
					TryMoveParty(new Size(1, 0));
					return true;
			}

			return false;
		}

		private void TryMoveParty(Size offset)
		{
			Point location = Party.CurrentTile.Location + offset;
			Tile destination = CurrentMap.GetTile(location);

			if (destination == null)
			{
				// We are trying to go off the edge of the current map if 
				// GetTile returned null. Try to go to an adjacent map.
				string dir = null;
				if (location.X < 0)
				{
					dir = "W";
					location.X = MapData.DimX-1;
				}
				else if (location.X >= MapData.DimX)
				{
					dir = "E";
					location.X = 0;
				}
				else if (location.Y < 0)
				{
					dir = "N";
					location.Y = MapData.DimY-1;
				}
				else if (location.Y >= MapData.DimY)
				{
					dir = "S";
					location.Y = 0;
				}

				if (dir == null)
					throw new Exception("Didn't determine out-of-bound direction");

				var newMapID = CurrentMap.MapData.GetAdjacentMapID(dir);

				// If there is no adjacent map defined, don't do anything.
				if (newMapID == null) return;

				// Peek at the adjacent map and see if we can actually go to that spot.
				var newMap = LoadDungeonMap((int)newMapID);
				destination = newMap.GetTile(location);

				if (destination.CanBeOccupied())
					SetPartyLocation((int)newMapID, location);

				return;
			}

			if (destination.CanBeOccupied())
				Party.SetLocation(destination);
			else if (destination.TileObject != null)
				destination.TileObject.Interact(this);
		}

		public void Redraw()
		{
			Window.dungeonContainer.Invalidate();
		}
	}
}
