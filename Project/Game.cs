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
	///     Game represents a single session of the game. It holds all of the DungeonMaps
	///     that are part of the game (DungeonMaps hold state information about the dungeon itself),
	///     as well as the Party that the user is playing as.
	///     It acts as the controller for party movement, and for initiating combat.
	/// </summary>
	internal class Game
	{
		/// <summary>
		///     The party that is playing through this Game.
		/// </summary>
		public readonly Party Party;

		private readonly Dictionary<int, DungeonMap> maps = new Dictionary<int, DungeonMap>();
		private CombatSession currentSession;

		internal Game()
		{
			// TODO: Temp code
			Party = new Party(new Point(7, 7));

			var heroIDs = HeroSelectScreen.BuildParty();

			foreach (var heroID in heroIDs)
			{
				Party.AddHero(new Hero(Party, heroID));
			}

			SetPartyLocation(1, Party.InitialLocation);
		}

		/// <summary>
		///     The DungeonMap that the Party is currently located in,
		///     and thus is being drawn to MainWindow's DungeonContainer.
		/// </summary>
		internal DungeonMap CurrentMap { get; private set; }

		/// <summary>
		///     True if the game has a CombatSession that is currently in progress.
		/// </summary>
		public bool InCombat
		{
			get { return (currentSession != null && currentSession.State != CombatSession.CombatState.Ended); }
		}

		/// <summary>
		///     Gets a DungeonMap by ID. Loads it from XML if it has not
		///     already bene created for this Game.
		/// </summary>
		/// <param name="mapID">The ID of the map to get.</param>
		/// <returns>The requested DungeonMap.</returns>
		private DungeonMap LoadDungeonMap(int mapID)
		{
			DungeonMap map;

			if (!maps.TryGetValue(mapID, out map))
			{
				map = maps[mapID] = new DungeonMap(mapID, this);
			}
			return map;
		}

		/// <summary>
		///     Set the location of the party to the given mapID and Point within that map.
		/// </summary>
		/// <param name="mapID">The mapID to place the party in.</param>
		/// <param name="point">The location within the specified map to place the party at.</param>
		public void SetPartyLocation(int mapID, Point point)
		{
			DungeonMap map = LoadDungeonMap(mapID);

			Party.SetLocation(map.GetTile(point));
			CurrentMap = map;
		}

		/// <summary>
		///     Initiates combat with the given MonsterPack. This will create a new CombatSession,
		///     and cause the MainWindow to start displaying that CombatSession.
		/// </summary>
		/// <param name="enemy">The MonsterPack to initiate combat with.</param>
		public void EnterCombat(MonsterPack enemy)
		{
			if (InCombat)
				throw new InvalidOperationException("Can't enter combat while in combat");

			currentSession = new CombatSession(Party, enemy);
		//	MainWindow.Window.dungeonContainer.Hide();
			MainWindow.Window.combatArena.Show();
			MainWindow.Window.combatArena.CombatSession = currentSession;

			currentSession.StateChanged += Session_StateChanged;

			currentSession.StartCombat();
		}

		private void Session_StateChanged(CombatSession sender)
		{
			if (sender.State == CombatSession.CombatState.Ended)
			{
				if (sender.Winner == Party)
					sender.MonsterPack.CurrentTile.TileObject = null;


				// Combat has ended. Restore the dungeon.
				//MainWindow.Window.dungeonContainer.Show();
				MainWindow.Window.combatArena.Hide();

				// Restore 10% health to each hero.
				foreach (var hero in Party.Members.Cast<Hero>())
					hero.Health += hero.MaxHealth / 10;
			}
		}

		/// <summary>
		///     Process a keypress and perform appropriate actions to state of the game.
		/// </summary>
		/// <param name="keyData">The key that was pressed.</param>
		/// <returns>True if a key was handled, otherwise false.</returns>
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

		/// <summary>
		///     Attempt to move the party in the requested direction.
		/// </summary>
		/// <param name="offset">The direction and magnitude of the attempted movement.</param>
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
					location.X = MapData.DimX - 1;
				}
				else if (location.X >= MapData.DimX)
				{
					dir = "E";
					location.X = 0;
				}
				else if (location.Y < 0)
				{
					dir = "N";
					location.Y = MapData.DimY - 1;
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

		/// <summary>
		///     Request a redraw of the current dungeon.
		/// </summary>
		public void RedrawDungeon()
		{
			MainWindow.Window.dungeonContainer.Invalidate();
		}
	}
}
