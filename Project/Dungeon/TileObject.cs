﻿using System;
using System.Drawing;
using System.Linq;

namespace Project.Dungeon
{
	/// <summary>
	///     TileObject represends some entity that can occupy a Tile of a Map.
	///     This might include the player, an enemy, or a door, and can be extended to be anthing else.
	///     TileObjects are represented in Maps.xml inside the Objects element, and are constructed from XML
	///     using methods that are marked with XmlParserAttribute.
	/// </summary>
	public abstract class TileObject
	{
		protected TileObject(Point loc)
		{
			InitialLocation = loc;
		}

		public Tile CurrentTile { get; private set; }
		public Point InitialLocation { get; private set; }

		public Image Image { get; protected set; }

		public virtual void Interact(Game game)
		{
			throw new NotImplementedException();
		}


		public void SetLocation(Tile tile)
		{
			if (CurrentTile != null)
			{
				CurrentTile.TileObject = null;
			}

			CurrentTile = tile;
			if (tile != null)
			{
				tile.TileObject = this;
			}
		}

		public virtual void Draw(Graphics graphics)
		{
			var loc = CurrentTile.Location;

			graphics.DrawImage(Image, loc.X * Tile.DimPixels, loc.Y * Tile.DimPixels, Tile.DimPixels, Tile.DimPixels);
		}
	}
}
