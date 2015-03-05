using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;

using Project;
using Project.Controls;
using Project.Data;
using MessageBox = System.Windows.MessageBox;
using Timer = System.Threading.Timer;

namespace MapEdit
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DungeonContainer container;
		private Controller controller = new Controller();
		private Timer dungeonTimer;

		private FoldingManager foldingManager;
		private XmlFoldingStrategy foldingStrategy = new XmlFoldingStrategy(){ShowAttributesWhenFolded = true};

		public MainWindow()
		{
			InitializeComponent();

			dungeonTimer = new Timer(DungeonTimerCallback, null, 0, 10);
		}

		private void DungeonTimerCallback(object state)
		{
			var needRedraw = false;
			if (controller != null && controller.CurrentMap != null)
			{
				foreach (var tile in controller.CurrentMap.Tiles.Where(tile => tile.NeedsRedraw))
				{
					tile.NeedsRedraw = false;
					needRedraw = true;
				}
			}

			if (needRedraw)
				container.Invalidate();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Create the interop host control.
			var host = new System.Windows.Forms.Integration.WindowsFormsHost();

			// Create the MaskedTextBox control.
			container = new DungeonContainer
			{
				Location = new System.Drawing.Point(0, 0)
			};

			container.Paint += container_Paint;
			controller.SetMap(1);
			host.Child = container;
			Grid.Children.Add(host);

			TextEditor.TextChanged += TextEditor_TextChanged;
			TextEditor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
			TextEditor.Options.ConvertTabsToSpaces = true;
			TextEditor.Options.ShowTabs = true;
			TextEditor.Options.IndentationSize = 2;
			
			foldingManager = FoldingManager.Install(TextEditor.TextArea);
		}


		void Caret_PositionChanged(object sender, EventArgs e)
		{
			TextEditorBorder.BorderBrush = Brushes.Black;
			ErrorText.Text = "";

			var curPos = TextEditor.CaretOffset;
			foreach (FoldingSection fm in foldingManager.AllFoldings)
			{
				if (fm.Title.StartsWith("<Map ") && fm.StartOffset <= curPos && fm.EndOffset >= curPos)
				{
					try
					{
						var match = Regex.Match(fm.Title, @"\d+");
						int id = Convert.ToInt32(match.Value);

						LoadMap(id);
					}
					catch (Exception ex)
					{
						TextEditorBorder.BorderBrush = Brushes.Red;
						ErrorText.Text = ex.Message;
					}
					return;
				}
			}
			try
			{
				XDocument.Load(new StringReader(TextEditor.Text));
			}
			catch (XmlException ex)
			{
				TextEditorBorder.BorderBrush = Brushes.Red;
				ErrorText.Text = ex.Message;
			}
		}

		void TextEditor_TextChanged(object sender, EventArgs e)
		{
			foldingStrategy = new XmlFoldingStrategy()
			{
				ShowAttributesWhenFolded = true
			};
			foldingStrategy.UpdateFoldings(foldingManager, TextEditor.Document);
		}

		void container_Paint(object sender, PaintEventArgs e)
		{
			if (controller != null && controller.CurrentMap != null)
				controller.CurrentMap.Draw(e.Graphics);
		}

		void LoadMap(int mapID)
		{
			var doc = XDocument.Load(new StringReader(TextEditor.Text));
			var element = XmlData.GetXElementByID(doc, "Maps", mapID);
			controller.SetMap(mapID, element);
			container.Invalidate();
		}

		private void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var text = File.ReadAllText(FilePathBox.Text);
				TextEditor.Text = text;

				foreach (FoldingSection fm in foldingManager.AllFoldings)
				{
					if (fm.Title.StartsWith("<Map "))
						fm.IsFolded = true;
				}

			}
			catch (Exception)
			{
				MessageBox.Show("File not found");
			}
		}
	}
}
