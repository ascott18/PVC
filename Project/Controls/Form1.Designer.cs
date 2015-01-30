namespace Project.Controls
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
			this.dungeonContainer = new DungeonContainer();
			this.combatArena = new CombatArena();
			this.SuspendLayout();
			// 
			// dungeonContainer
			// 
			this.dungeonContainer.Location = new System.Drawing.Point(0, 0);
			this.dungeonContainer.Name = "dungeonContainer";
			this.dungeonContainer.Size = new System.Drawing.Size(700, 700);
			this.dungeonContainer.TabIndex = 0;
			// 
			// combatArena
			// 
			this.combatArena.Location = new System.Drawing.Point(193, 62);
			this.combatArena.Name = "combatArena";
			this.combatArena.Size = new System.Drawing.Size(386, 424);
			this.combatArena.TabIndex = 1;
			this.combatArena.Visible = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 705);
			this.Controls.Add(this.combatArena);
			this.Controls.Add(this.dungeonContainer);
			this.KeyPreview = true;
			this.Name = "MainWindow";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		internal DungeonContainer dungeonContainer;
		internal CombatArena combatArena;







	}
}

