﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
		public static int[] Columns = new[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
		};

		private DungeonContainer container;
		private Controller controller = new Controller();
		private Timer dungeonTimer;
		private Timer updateTimer;

		private FoldingManager foldingManager;
		private XmlFoldingStrategy foldingStrategy = new XmlFoldingStrategy(){ShowAttributesWhenFolded = true};
		private string currentFile;

		private string lastSavedText = "";

		public MainWindow()
		{
			InitializeComponent();

			dungeonTimer = new Timer(DungeonTimerCallback, null, 0, 10);
			updateTimer = new Timer(UpdateCallback, null, Timeout.Infinite, Timeout.Infinite);
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

		private void UpdateCallback(object state)
		{
			updateTimer.Change(Timeout.Infinite, Timeout.Infinite);

			Dispatcher.Invoke(Update);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Create the MaskedTextBox control.
			Host.Child = container = new DungeonContainer
			{
				Location = new System.Drawing.Point(0, 0)
			};

			container.Paint += container_Paint;
			controller.SetMap(1);

			TextEditor.TextChanged += TextEditor_TextChanged;
			TextEditor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
			TextEditor.Options.ConvertTabsToSpaces = true;
			TextEditor.Options.ShowTabs = true;
			TextEditor.Options.IndentationSize = 2;
			
			foldingManager = FoldingManager.Install(TextEditor.TextArea);
		}

		void Update()
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

		void QueueUpdate()
		{
			updateTimer.Change(200, Timeout.Infinite);
		}

		void Caret_PositionChanged(object sender, EventArgs e)
		{
			QueueUpdate();
		}

		void TextEditor_TextChanged(object sender, EventArgs e)
		{
			foldingStrategy = new XmlFoldingStrategy()
			{
				ShowAttributesWhenFolded = true
			};
			foldingStrategy.UpdateFoldings(foldingManager, TextEditor.Document);

			QueueUpdate();
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
			if (lastSavedText != TextEditor.Text)
			{
				var result = MessageBox.Show("You have unsaved changes. Are you sure you want to reload?", "Unsaved changes", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.Cancel)
					return;
			}

			try
			{
				var text = File.ReadAllText(FilePathBox.Text);
				lastSavedText = text;
				TextEditor.Text = text;
				currentFile = FilePathBox.Text;
				ErrorText.Text = "Loaded " + new FileInfo(currentFile).FullName;
				SaveButton.IsEnabled = true;

				foreach (FoldingSection fm in foldingManager.AllFoldings)
				{
					if (fm.Title.StartsWith("<Map "))
						fm.IsFolded = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (String.IsNullOrEmpty(currentFile))
					return;

				var fout = new StreamWriter(File.OpenWrite(currentFile));
				fout.Write(TextEditor.Text);
				ErrorText.Text = "Saved to " + new FileInfo(currentFile).FullName;
				fout.Close();
				lastSavedText = TextEditor.Text;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (lastSavedText != TextEditor.Text)
			{
				var result = MessageBox.Show("You have unsaved changes. Are you sure you want to exit?", "Unsaved changes", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.Cancel)
					e.Cancel = true;
			}
		}
	}
}