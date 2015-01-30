using System;
using System.Drawing;
using Project.Properties;

namespace Project.Dungeon
{
	/// <summary>
	/// TileObject represends some entity that can occupy a Tile of a Map.
	/// This might include the player, an enemy, or a door, and can be extended to be anthing else.
	/// TileObjects are represented in Maps.xml inside the Objects element, and are constructed from XML
	/// using methods that are marked with XmlParserAttribute.
	/// </summary>
	class TileObject
	{
		public Tile CurrentTile { get; private set; }
		public Point InitialLocation { get; private set; }

		public Image Image { get; protected set; }

		public virtual void Interact(Game game)
		{
			throw new NotImplementedException();
		}


		protected TileObject(Point loc)
		{
			Image = Resources.lumbergh; // TODO: temp
			InitialLocation = loc;
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
	}
}
