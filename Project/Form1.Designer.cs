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
			this.dungeonContainer = new Project.DungeonContainer();
			this.combatArena1 = new Project.Shared.Items.CombatArena();
			this.SuspendLayout();
			// 
			// dungeonContainer
			// 
			this.dungeonContainer.Location = new System.Drawing.Point(0, 0);
			this.dungeonContainer.Name = "dungeonContainer";
			this.dungeonContainer.Size = new System.Drawing.Size(700, 700);
			this.dungeonContainer.TabIndex = 0;
			// 
			// combatArena1
			// 
			this.combatArena1.Location = new System.Drawing.Point(777, 96);
			this.combatArena1.Name = "combatArena1";
			this.combatArena1.Size = new System.Drawing.Size(386, 424);
			this.combatArena1.TabIndex = 1;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 705);
			this.Controls.Add(this.combatArena1);
			this.Controls.Add(this.dungeonContainer);
			this.KeyPreview = true;
			this.Name = "MainWindow";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		internal DungeonContainer dungeonContainer;
		internal Shared.Items.CombatArena combatArena1;







	}
}

