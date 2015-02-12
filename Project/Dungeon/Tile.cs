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

				DungeonMap.Game.RedrawDungeon();
			}
		}

		/// <summary>
		///     The TileData instance that represents static information about this tile.
		/// </summary>
		public TileData TileData { get; private set; }

		/// <summary>
		///     Draws this tile, including its background image and that of the TileObject
		///     that is occupying this tile.
		/// </summary>
		/// <param name="graphics">The Graphics object of a DungeonContainer instance to draw to.</param>
		public void Draw(Graphics graphics)
		{
			graphics.DrawImage(TileData.Image, Location.X * DimPixels, Location.Y * DimPixels, DimPixels, DimPixels);

			if (TileObject != null)
				graphics.DrawImage(TileObject.Image, Location.X * DimPixels, Location.Y * DimPixels, DimPixels, DimPixels);
		}

		public bool CanBeOccupied()
		{
			return TileObject == null && !TileData.IsObstable;
		}
	}
}
