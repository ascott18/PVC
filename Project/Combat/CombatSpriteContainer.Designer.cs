namespace Project.Combat
{
	partial class CombatSpriteContainer
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
			this.image = new System.Windows.Forms.PictureBox();
			this.healthText = new System.Windows.Forms.Label();
			this.nameText = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
			this.SuspendLayout();
			// 
			// image
			// 
			this.image.Image = global::Project.Properties.Resources.stu;
			this.image.Location = new System.Drawing.Point(25, 0);
			this.image.Name = "image";
			this.image.Size = new System.Drawing.Size(50, 50);
			this.image.TabIndex = 0;
			this.image.TabStop = false;
			// 
			// healthText
			// 
			this.healthText.Location = new System.Drawing.Point(12, 68);
			this.healthText.Name = "healthText";
			this.healthText.Size = new System.Drawing.Size(75, 13);
			this.healthText.TabIndex = 1;
			this.healthText.Text = "100/100";
			this.healthText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nameText
			// 
			this.nameText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.nameText.Location = new System.Drawing.Point(3, 53);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(94, 14);
			this.nameText.TabIndex = 2;
			this.nameText.Text = "Stu Steiner";
			this.nameText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CombatSpriteContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nameText);
			this.Controls.Add(this.healthText);
			this.Controls.Add(this.image);
			this.Name = "CombatSpriteContainer";
			this.Size = new System.Drawing.Size(100, 94);
			((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox image;
		private System.Windows.Forms.Label healthText;
		private System.Windows.Forms.Label nameText;
	}
}
