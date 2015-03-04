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

		private readonly object currentImageLock = new object();

		public Server(Point loc)
			: base(loc)
		{
			timer = new Timer(TimerCallback, false, 0, 500);
			TimerCallback(true);
		}

		private void TimerCallback(object state)
		{
			var forceDraw = state as bool?;

			if (forceDraw == true || (CurrentTile != null && CurrentTile.IsInCurrentMap))
			lock (currentImageLock)
			{
				bool isFirstGen = false;
				if (currentImage == null)
				{
					isFirstGen = true;
					currentImage = new Bitmap(lightsImage);
				}

				for (int i = 0; i < currentImage.Height * currentImage.Width; ++i)
				{

					int shouldChangeRandom;
					lock(random)
						shouldChangeRandom = !isFirstGen ? random.Next(101) : 0;

					const int shouldChangePercent = 2;

					if (isFirstGen || shouldChangeRandom < shouldChangePercent)
					{
						int row = i / currentImage.Height;
						int col = i % currentImage.Width;
						if (row % 2 != 0) col = currentImage.Width - col - 1;

						var pixel = currentImage.GetPixel(col, row);
						if (pixel.A > 0)
						{
							const int blackPercent = 95;
							int colorRandom;
							lock (random)
								colorRandom = random.Next(101);

							Color color;
							if (colorRandom < blackPercent)
								color = Color.Black;
							else
							{
								lock (random)
									colorRandom = random.Next(colors.Length);
								color = colors[colorRandom];
							}

							if (pixel != color)
							{
								currentImage.SetPixel(col, row, color);
								
								if (CurrentTile != null)
									CurrentTile.NeedsRedraw = true;
							}
						}
					}
				}
				lock (random)
					timer.Change(random.Next(50, 100), 50000);
			}
			else
				lock (random)
					timer.Change(random.Next(1000, 2000), 50000);


		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawImage(baseImage, CurrentTile.Rectangle);

			lock(currentImageLock)
			if (currentImage != null)
				graphics.DrawImage(currentImage, CurrentTile.Rectangle);
		}

		private static readonly Color[] colors =
		{
			Color.Red, Color.Blue, Color.Orange, 
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
		/// <param name="xElement">The XElement to parse</param>
		/// <returns>The Watercooler object parsed from the XML.</returns>
		[XmlData.XmlParserAttribute("Server")]
		public static TileObject XmlParser(XElement xElement)
		{
			var loc = new Point(int.Parse(xElement.Attribute("x").Value), int.Parse(xElement.Attribute("y").Value));

			var watercooler = new Server(loc);
			return watercooler;
		}
	}
}
