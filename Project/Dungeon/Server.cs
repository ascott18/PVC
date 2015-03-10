using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;

namespace Project.Dungeon
{
	internal class Server : TileObject
	{
		private static readonly Color[] colors =
		{
			Color.Red, Color.Cyan, Color.Orange,
			Color.LimeGreen, Color.LimeGreen
		};

		private static readonly Random randomSeeder = new Random();

		private readonly Image baseImage = XmlData.LoadImage("serverBase");
		private readonly object currentImageLock = new object();
		private readonly Image lightsImage = XmlData.LoadImage("serverLights");
		private readonly Random random;
		private readonly Timer timer;
		private Bitmap currentImage;

		public Server(Point loc)
			: base(loc)
		{
			// Make a random for this class (to prevent excessive locking on a shared random),
			// and seed it with a shared random (to prevent all instance randoms from having the same seed).
			lock (randomSeeder)
				random = new Random(randomSeeder.Next());

			timer = new Timer(Render, false, Timeout.Infinite, Timeout.Infinite);

			// Do an update asap.
			Task.Run(() => Render(true));
		}

		private void Render(object state)
		{
			var forceDraw = state as bool?;

			if (forceDraw == true || (CurrentTile != null && CurrentTile.IsInCurrentMap))
			{
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
						int shouldChangeRandom = !isFirstGen ? random.Next(101) : 0;

						const int shouldChangePercent = 2;

						// Only change a pixel 2% of the time (or if this is the first render).
						if (isFirstGen || shouldChangeRandom < shouldChangePercent)
						{
							int row = i / currentImage.Height;
							int col = i % currentImage.Width;
							if (row % 2 != 0) col = currentImage.Width - col - 1;

							var pixel = currentImage.GetPixel(col, row);
							if (pixel.A > 0)
							{
								// We want it to be 95% black.
								const int blackPercent = 95;

								Color color;
								if (random.Next(101) < blackPercent)
									color = Color.DimGray;
								else
									color = colors[random.Next(colors.Length)];

								if (pixel != color)
								{
									currentImage.SetPixel(col, row, color);

									if (CurrentTile != null)
										CurrentTile.NeedsRedraw = true;
								}
							}
						}
					}

					// Queue another update for a random amount of time in the future.
					timer.Change(random.Next(100, 200), Timeout.Infinite);
				}
			}
			else
			{
				// Sleep for an extended amount of time while it doesn't need to be redrawn.
				// Still keep it random so it looks more natural when the player enters 
				// the room again.
				timer.Change(random.Next(1000, 2000), Timeout.Infinite);
			}
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawImage(baseImage, CurrentTile.Rectangle);

			lock (currentImageLock)
				if (currentImage != null)
					graphics.DrawImage(currentImage, CurrentTile.Rectangle);
		}


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

			return new Server(loc);
		}
	}
}
