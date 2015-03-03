﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;
using Project.Items;
using Project.Sprites;

namespace Project.Dungeon
{
	class Server : TileObject
	{
		private readonly Timer timer;
		private const int RechargeTime = 1000;

		private long startTime;

		private readonly Image lightsImage = XmlData.LoadImage("serverLights");
		private readonly Image baseImage = XmlData.LoadImage("serverBase");

		protected Server(Point loc)
			: base(loc)
		{
			timer = new Timer(TimerCallback, null, 0, 500);
		}

		private void TimerCallback(object state)
		{
			CurrentTile.Invalidate();
		}

		public override void Draw(Graphics graphics)
		{
			var loc = CurrentTile.Location;

			var x = loc.X * Tile.DimPixels;
			var y = loc.Y * Tile.DimPixels;

			var rect = new Rectangle(x, y, Tile.DimPixels, Tile.DimPixels);
			var src = new Rectangle(0, 0, Tile.DimPixels, Tile.DimPixels);
			graphics.DrawImage(baseImage, rect, src, GraphicsUnit.Pixel);

			var rect2 = new Rectangle(x, y, Tile.DimPixels, Tile.DimPixels);
			var src2 = new Rectangle(0, 0, Tile.DimPixels, Tile.DimPixels);


			// from http://stackoverflow.com/questions/6020406/travel-through-pixels-in-bmp
			using (var bmp = new Bitmap(lightsImage))
			{
				for (int i = 0 ; i < bmp.Height*bmp.Width; ++i) {
					int row = i / bmp.Height;
					int col = i % bmp.Width;
					if (row%2 != 0) col = bmp.Width-col-1;

					var pixel = bmp.GetPixel(col, row);
					if (pixel.A > 0)
					{
						var color = colors[random.Next(colors.Length)];
						bmp.SetPixel(col, row, color);
					}
				}
				graphics.DrawImage(bmp, rect2, src2, GraphicsUnit.Pixel);
			}
		}

		private static readonly Color[] colors =
		{
			Color.Red, Color.Blue, Color.Orange, 
			Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black,
			Color.LimeGreen, Color.LimeGreen,
		};

		private static readonly Random random = new Random();



		public override void Interact(Game game)
		{
			// nothing
		}


		/// <summary>
		///     Parses an XML element and returns a Watercooler object that represents it.
		/// </summary>
		/// <param name="coolerElement">The XElement to parse</param>
		/// <returns>The Watercooler object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("Server")]
		public static TileObject XmlParser(XElement coolerElement)
		{
			var loc = new Point(int.Parse(coolerElement.Attribute("x").Value), int.Parse(coolerElement.Attribute("y").Value));

			var watercooler = new Server(loc);
			return watercooler;
		}
	}
}
