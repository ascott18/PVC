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
			this.woodButton2 = new Project.Controls.WoodButton();
			this.endButton = new Project.Controls.WoodButton();
			this.inventoryButton = new Project.Controls.WoodButton();
			this.combatArena = new Project.Controls.CombatArena();
			this.dungeonContainer = new Project.Controls.DungeonContainer();
			this.statsButton = new Project.Controls.WoodButton();
			this.SuspendLayout();
			// 
			// woodButton2
			// 
			this.woodButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("woodButton2.BackgroundImage")));
			this.woodButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.woodButton2.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.woodButton2.ForeColor = System.Drawing.Color.Tan;
			this.woodButton2.Location = new System.Drawing.Point(132, 0);
			this.woodButton2.Name = "woodButton2";
			this.woodButton2.Size = new System.Drawing.Size(245, 30);
			this.woodButton2.TabIndex = 5;
			this.woodButton2.UseVisualStyleBackColor = true;
			// 
			// endButton
			// 
			this.endButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("endButton.BackgroundImage")));
			this.endButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.endButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.endButton.ForeColor = System.Drawing.Color.Tan;
			this.endButton.Location = new System.Drawing.Point(0, 0);
			this.endButton.Name = "endButton";
			this.endButton.Size = new System.Drawing.Size(132, 30);
			this.endButton.TabIndex = 4;
			this.endButton.Text = "END GAME";
			this.endButton.UseVisualStyleBackColor = true;
			this.endButton.Click += new System.EventHandler(this.endButton_Click);
			// 
			// inventoryButton
			// 
			this.inventoryButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("inventoryButton.BackgroundImage")));
			this.inventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.inventoryButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.inventoryButton.ForeColor = System.Drawing.Color.Tan;
			this.inventoryButton.Location = new System.Drawing.Point(501, 0);
			this.inventoryButton.Name = "inventoryButton";
			this.inventoryButton.Size = new System.Drawing.Size(151, 30);
			this.inventoryButton.TabIndex = 3;
			this.inventoryButton.Text = "INVENTORY";
			this.inventoryButton.UseVisualStyleBackColor = true;
			this.inventoryButton.Click += new System.EventHandler(this.inventoryButton_Click);
			// 
			// combatArena
			// 
			this.combatArena.BackColor = System.Drawing.SystemColors.Control;
			this.combatArena.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.combatArena.Location = new System.Drawing.Point(27, 153);
			this.combatArena.Name = "combatArena";
			this.combatArena.Size = new System.Drawing.Size(600, 368);
			this.combatArena.TabIndex = 1;
			this.combatArena.Visible = false;
			// 
			// dungeonContainer
			// 
			this.dungeonContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dungeonContainer.Location = new System.Drawing.Point(0, 30);
			this.dungeonContainer.Name = "dungeonContainer";
			this.dungeonContainer.Size = new System.Drawing.Size(652, 652);
			this.dungeonContainer.TabIndex = 0;
			// 
			// statsButton
			// 
			this.statsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("statsButton.BackgroundImage")));
			this.statsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.statsButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statsButton.ForeColor = System.Drawing.Color.Tan;
			this.statsButton.Location = new System.Drawing.Point(377, 0);
			this.statsButton.Name = "statsButton";
			this.statsButton.Size = new System.Drawing.Size(124, 30);
			this.statsButton.TabIndex = 5;
			this.statsButton.Text = "STATS";
			this.statsButton.UseVisualStyleBackColor = true;
			this.statsButton.Click += new System.EventHandler(this.statsButton_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 681);
			this.Controls.Add(this.statsButton);
			this.Controls.Add(this.woodButton2);
			this.Controls.Add(this.endButton);
			this.Controls.Add(this.inventoryButton);
			this.Controls.Add(this.combatArena);
			this.Controls.Add(this.dungeonContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "PVC";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
			this.ResumeLayout(false);

		}

		#endregion

		internal DungeonContainer dungeonContainer;
		internal CombatArena combatArena;
		private WoodButton inventoryButton;
		private WoodButton endButton;
		private WoodButton woodButton2;
		private WoodButton statsButton;







	}
}

