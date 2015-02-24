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
			this.woodButton1 = new Project.Controls.WoodButton();
			this.inventoryButton = new Project.Controls.WoodButton();
			this.combatArena = new Project.Controls.CombatArena();
			this.dungeonContainer = new Project.Controls.DungeonContainer();
			this.SuspendLayout();
			// 
			// woodButton2
			// 
			this.woodButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("woodButton2.BackgroundImage")));
			this.woodButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.woodButton2.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.woodButton2.ForeColor = System.Drawing.Color.Tan;
			this.woodButton2.Location = new System.Drawing.Point(249, 0);
			this.woodButton2.Name = "woodButton2";
			this.woodButton2.Size = new System.Drawing.Size(252, 30);
			this.woodButton2.TabIndex = 5;
			this.woodButton2.UseVisualStyleBackColor = true;
			// 
			// woodButton1
			// 
			this.woodButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("woodButton1.BackgroundImage")));
			this.woodButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.woodButton1.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.woodButton1.ForeColor = System.Drawing.Color.Tan;
			this.woodButton1.Location = new System.Drawing.Point(0, 0);
			this.woodButton1.Name = "woodButton1";
			this.woodButton1.Size = new System.Drawing.Size(249, 30);
			this.woodButton1.TabIndex = 4;
			this.woodButton1.UseVisualStyleBackColor = true;
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
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 681);
			this.Controls.Add(this.woodButton2);
			this.Controls.Add(this.woodButton1);
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
		private WoodButton woodButton1;
		private WoodButton woodButton2;







	}
}

