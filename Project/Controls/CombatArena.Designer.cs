namespace Project.Controls
{
	partial class CombatArena
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombatArena));
			this.woodButton2 = new Project.Controls.WoodButton();
			this.woodButton1 = new Project.Controls.WoodButton();
			this.retreatButton = new Project.Controls.WoodButton();
			this.SuspendLayout();
			// 
			// woodButton2
			// 
			this.woodButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.woodButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("woodButton2.BackgroundImage")));
			this.woodButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.woodButton2.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.woodButton2.ForeColor = System.Drawing.Color.Tan;
			this.woodButton2.Location = new System.Drawing.Point(-2, 354);
			this.woodButton2.Name = "woodButton2";
			this.woodButton2.Size = new System.Drawing.Size(242, 34);
			this.woodButton2.TabIndex = 2;
			this.woodButton2.UseVisualStyleBackColor = true;
			// 
			// woodButton1
			// 
			this.woodButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.woodButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("woodButton1.BackgroundImage")));
			this.woodButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.woodButton1.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.woodButton1.ForeColor = System.Drawing.Color.Tan;
			this.woodButton1.Location = new System.Drawing.Point(361, 354);
			this.woodButton1.Name = "woodButton1";
			this.woodButton1.Size = new System.Drawing.Size(241, 34);
			this.woodButton1.TabIndex = 1;
			this.woodButton1.UseVisualStyleBackColor = true;
			// 
			// retreatButton
			// 
			this.retreatButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.retreatButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("retreatButton.BackgroundImage")));
			this.retreatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.retreatButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.retreatButton.ForeColor = System.Drawing.Color.Tan;
			this.retreatButton.Location = new System.Drawing.Point(240, 354);
			this.retreatButton.Name = "retreatButton";
			this.retreatButton.Size = new System.Drawing.Size(121, 34);
			this.retreatButton.TabIndex = 0;
			this.retreatButton.Text = "RETREAT";
			this.retreatButton.UseVisualStyleBackColor = true;
			this.retreatButton.Click += new System.EventHandler(this.retreatButton_Click);
			// 
			// CombatArena
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.woodButton2);
			this.Controls.Add(this.woodButton1);
			this.Controls.Add(this.retreatButton);
			this.Name = "CombatArena";
			this.Size = new System.Drawing.Size(600, 388);
			this.ResumeLayout(false);

		}

		#endregion

		private WoodButton retreatButton;
		private WoodButton woodButton1;
		private WoodButton woodButton2;




	}
}
