namespace Project
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dungeonContainer1 = new Project.DungeonContainer();
			this.SuspendLayout();
			// 
			// dungeonContainer1
			// 
			this.dungeonContainer1.Location = new System.Drawing.Point(0, 0);
			this.dungeonContainer1.Name = "dungeonContainer1";
			this.dungeonContainer1.Size = new System.Drawing.Size(700, 700);
			this.dungeonContainer1.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(705, 705);
			this.Controls.Add(this.dungeonContainer1);
			this.Name = "MainWindow";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private DungeonContainer dungeonContainer1;


	}
}

