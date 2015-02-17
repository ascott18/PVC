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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.inventoryButton = new Project.Controls.WoodButton();
			this.combatArena = new Project.Controls.CombatArena();
			this.dungeonContainer = new Project.Controls.DungeonContainer();
			this.SuspendLayout();
			// 
			// inventoryButton
			// 
			this.inventoryButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("inventoryButton.BackgroundImage")));
			this.inventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.inventoryButton.Font = new System.Drawing.Font("Arial", 12F);
			this.inventoryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(244)))), ((int)(((byte)(164)))));
			this.inventoryButton.Location = new System.Drawing.Point(581, 0);
			this.inventoryButton.Name = "inventoryButton";
			this.inventoryButton.Size = new System.Drawing.Size(119, 30);
			this.inventoryButton.TabIndex = 3;
			this.inventoryButton.Text = "Inventory";
			this.inventoryButton.UseVisualStyleBackColor = true;
			this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click);
			// 
			// combatArena
			// 
			this.combatArena.BackColor = System.Drawing.Color.Transparent;
			this.combatArena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.combatArena.Location = new System.Drawing.Point(47, 121);
			this.combatArena.Name = "combatArena";
			this.combatArena.Size = new System.Drawing.Size(600, 424);
			this.combatArena.TabIndex = 1;
			this.combatArena.Visible = false;
			// 
			// dungeonContainer
			// 
			this.dungeonContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dungeonContainer.Location = new System.Drawing.Point(0, 30);
			this.dungeonContainer.Name = "dungeonContainer";
			this.dungeonContainer.Size = new System.Drawing.Size(700, 700);
			this.dungeonContainer.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 731);
			this.Controls.Add(this.inventoryButton);
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
		private WoodButton inventoryButton;







	}
}

