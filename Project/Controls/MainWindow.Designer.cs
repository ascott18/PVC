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
			this.inventoryScreen1 = new Project.Controls.InventoryScreen();
			this.combatArena = new Project.Controls.CombatArena();
			this.dungeonContainer = new Project.Controls.DungeonContainer();
			this.SuspendLayout();
			// 
			// inventoryScreen1
			// 
			this.inventoryScreen1.Location = new System.Drawing.Point(706, 12);
			this.inventoryScreen1.Name = "inventoryScreen1";
			this.inventoryScreen1.Party = null;
			this.inventoryScreen1.Size = new System.Drawing.Size(349, 677);
			this.inventoryScreen1.TabIndex = 2;
			// 
			// combatArena
			// 
			this.combatArena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.combatArena.Location = new System.Drawing.Point(47, 121);
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
			this.ClientSize = new System.Drawing.Size(1095, 701);
			this.Controls.Add(this.inventoryScreen1);
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
		private InventoryScreen inventoryScreen1;







	}
}

