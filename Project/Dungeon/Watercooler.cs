using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Project.Data;
using Project.Sprites;

namespace Project.Dungeon
{
	internal class Watercooler : TileObject
	{
		private const int RechargeTime = 1000;

		private readonly Image emptyImage = XmlData.LoadImage("watercoolerEmpty");
		private readonly Image fullImage = XmlData.LoadImage("watercooler");
		private readonly Stopwatch stopwatch;
		private readonly Timer timer;
		private int pixelsFilled = Tile.DimPixels;
		private long startTime;

		protected Watercooler(Point loc)
			: base(loc)
		{
			stopwatch = new Stopwatch();
			stopwatch.Start();
			timer = new Timer(TimerCallback, null, Timeout.Infinite, 10);
		}

		private double GetPercentFilled()
		{
			if (startTime == 0)
				return 1;

			return (double)(stopwatch.ElapsedMilliseconds - startTime) / RechargeTime;
		}

		private void TimerCallback(object state)
		{
			var percent = GetPercentFilled();
			if (percent >= 1)
			{
				pixelsFilled = Tile.DimPixels;
				timer.Change(Timeout.Infinite, 10);
			}
			else
			{
				const int basePixels = 34;
				pixelsFilled = (int)(basePixels + (Tile.DimPixels - basePixels) * percent);
			}

			CurrentTile.NeedsRedraw = true;
		}

		public override void Draw(Graphics graphics)
		{
			var loc = CurrentTile.Location;

			var x = loc.X * Tile.DimPixels;
			var y = loc.Y * Tile.DimPixels;
			var yoffs = Tile.DimPixels - pixelsFilled;

			var rect = new Rectangle(x, y, Tile.DimPixels, yoffs);
			var src = new Rectangle(0, 0, Tile.DimPixels, yoffs);
			graphics.DrawImage(emptyImage, rect, src, GraphicsUnit.Pixel);

			var rect2 = new Rectangle(x, y + yoffs, Tile.DimPixels, pixelsFilled);
			var src2 = new Rectangle(0, yoffs, Tile.DimPixels, pixelsFilled);
			graphics.DrawImage(fullImage, rect2, src2, GraphicsUnit.Pixel);
		}


		public override void Interact(Game game)
		{
			if (GetPercentFilled() < 1)
				return;

			foreach (var sprite in game.Party.Members)
			{
				var hero = sprite as Hero;
				hero.Health += hero.MaxHealth / 10;
			}

			startTime = stopwatch.ElapsedMilliseconds;
			timer.Change(0, 10);
		}


		/// <summary>
		///     Parses an XML element and returns a Watercooler object that represents it.
		/// </summary>
		/// <param name="coolerElement">The XElement to parse</param>
		/// <returns>The Watercooler object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("Watercooler")]
		public static TileObject XmlParser(XElement coolerElement)
		{
			var loc = new Point(int.Parse(coolerElement.Attribute("x").Value), int.Parse(coolerElement.Attribute("y").Value));

			var watercooler = new Watercooler(loc);

			return watercooler;
		}
	}
}
