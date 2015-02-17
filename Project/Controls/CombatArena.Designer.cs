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
			this.retreatButton = new Project.Controls.WoodButton();
			this.SuspendLayout();
			// 
			// retreatButton
			// 
			this.retreatButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("retreatButton.BackgroundImage")));
			this.retreatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.retreatButton.Font = new System.Drawing.Font("Arial", 12F);
			this.retreatButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(244)))), ((int)(((byte)(164)))));
			this.retreatButton.Location = new System.Drawing.Point(480, 387);
			this.retreatButton.Name = "retreatButton";
			this.retreatButton.Size = new System.Drawing.Size(117, 34);
			this.retreatButton.TabIndex = 0;
			this.retreatButton.Text = "Retreat!";
			this.retreatButton.UseVisualStyleBackColor = true;
			this.retreatButton.Click += new System.EventHandler(this.retreatButton_Click);
			// 
			// CombatArena
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.retreatButton);
			this.Name = "CombatArena";
			this.Size = new System.Drawing.Size(600, 424);
			this.ResumeLayout(false);

		}

		#endregion

		private WoodButton retreatButton;




	}
}
