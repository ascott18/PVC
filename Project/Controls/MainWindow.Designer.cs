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
			this.combatArena = new Project.Controls.CombatArena();
			this.dungeonContainer = new Project.Controls.DungeonContainer();
			this.SuspendLayout();
			// 
			// combatArena
			// 
			this.combatArena.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.combatArena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.combatArena.Location = new System.Drawing.Point(12, 115);
			this.combatArena.Name = "combatArena";
			this.combatArena.Size = new System.Drawing.Size(600, 424);
			this.combatArena.TabIndex = 1;
			this.combatArena.Visible = false;
			// 
			// dungeonContainer
			// 
			this.dungeonContainer.Location = new System.Drawing.Point(0, 0);
			this.dungeonContainer.Name = "dungeonContainer";
			this.dungeonContainer.Size = new System.Drawing.Size(700, 700);
			this.dungeonContainer.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 701);
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

