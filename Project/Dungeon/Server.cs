using System;
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

		private readonly Image lightsImage = XmlData.LoadImage("serverLights");
		private readonly Image baseImage = XmlData.LoadImage("serverBase");
		private Bitmap currentImage;

		protected Server(Point loc)
			: base(loc)
		{
			timer = new Timer(TimerCallback, null, 0, 500);
		}

		private void TimerCallback(object state)
		{
			Debug.WriteLine("draw server");

			if (CurrentTile == null)
				return;

			// from http://stackoverflow.com/questions/6020406/travel-through-pixels-in-bmp
			if (currentImage != null)
				currentImage.Dispose();

			var bmp = currentImage = new Bitmap(lightsImage);

			for (int i = 0; i < bmp.Height * bmp.Width; ++i)
			{
				int row = i / bmp.Height;
				int col = i % bmp.Width;
				if (row % 2 != 0) col = bmp.Width - col - 1;

				var pixel = bmp.GetPixel(col, row);
				if (pixel.A > 0)
				{
					var color = colors[random.Next(colors.Length)];
					bmp.SetPixel(col, row, color);
				}
			}

			CurrentTile.NeedsRedraw = true;
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawImage(baseImage, CurrentTile.Rectangle);
			graphics.DrawImage(currentImage, CurrentTile.Rectangle);
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
