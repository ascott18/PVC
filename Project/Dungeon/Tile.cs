using System;
using System.Drawing;

namespace Project.Dungeon
{
	/// <summary>
	///     Tile represents an actual tile in a DungeonMap.
	///     It contains information about what occupies that spot on the map,
	///     and it draws anything that occupies that tile.
	/// </summary>
	internal class Tile
	{
		public const int DimPixels = 50;

		/// <summary>
		///     The DungeonMap that this Tile belongs to.
		/// </summary>
		public readonly DungeonMap DungeonMap;

		/// <summary>
		///     The coordinates of this tile within its parent DungeonMap.
		/// </summary>
		public readonly Point Location;

		/// <summary>
		/// The rectangle that represents the draw location of the tile
		/// within the DungeonMap.
		/// </summary>
		public readonly Rectangle Rectangle;

		private TileObject tileObject;

		/// <summary>
		///     Create a Tile for the specified DungeonMap at the specified coordinates within that map.
		/// </summary>
		/// <param name="dungeonMap">The DungeonMap that the tile will belong to.</param>
		/// <param name="loc">The location of the tile within the DungeonMap.</param>
		public Tile(DungeonMap dungeonMap, Point loc)
		{
			Location = loc;

			DungeonMap = dungeonMap;
			TileData = DungeonMap.MapData.GetTileData(loc);
			Rectangle = new Rectangle(Location.X*DimPixels, Location.Y*DimPixels, DimPixels, DimPixels);
		}

		/// <summary>
		///     The TileObject currently occupying this tile, if there is one.
		/// </summary>
		public TileObject TileObject
		{
			get { return tileObject; }
			set
			{
				if (tileObject != null && value != null)
					throw new InvalidOperationException("Clobbered a TileObject");

				tileObject = value;

				NeedsRedraw = true;
			}
		}

		public bool IsInCurrentMap
		{
			get { return DungeonMap.Game.CurrentMap == DungeonMap; }
		}

		/// <summary>
		///     The TileData instance that represents static information about this tile.
		/// </summary>
		public TileData TileData { get; private set; }

		public bool NeedsRedraw { get; set; }

		/// <summary>
		///     Draws this tile, including its background image and that of the TileObject
		///     that is occupying this tile.
		/// </summary>
		/// <param name="graphics">The Graphics object of a DungeonContainer instance to draw to.</param>
		public void Draw(Graphics graphics)
		{
			if (TileData.Image != null)
				graphics.DrawImage(TileData.Image, Rectangle);

			if (TileObject != null)
				TileObject.Draw(graphics);
		}

		public bool CanBeOccupied()
		{
			return TileObject == null && !TileData.IsObstacle;
		}
	}
}
