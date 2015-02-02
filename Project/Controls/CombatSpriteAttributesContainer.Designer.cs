namespace Project.Controls
{
	partial class CombatSpriteAttributesContainer
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
			this.image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.image.Image = global::Project.Properties.Resources.stu;
			this.image.Location = new System.Drawing.Point(25, 13);
			this.image.Name = "image";
			this.image.Size = new System.Drawing.Size(50, 50);
			this.image.TabIndex = 0;
			this.image.TabStop = false;
			// 
			// healthText
			// 
			this.healthText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.healthText.BackColor = System.Drawing.Color.Transparent;
			this.healthText.Location = new System.Drawing.Point(3, 0);
			this.healthText.Name = "healthText";
			this.healthText.Size = new System.Drawing.Size(94, 13);
			this.healthText.TabIndex = 1;
			this.healthText.Text = "0/0";
			this.healthText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nameText
			// 
			this.nameText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nameText.Location = new System.Drawing.Point(3, 65);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(94, 27);
			this.nameText.TabIndex = 2;
			this.nameText.Text = "Stu Steiner";
			this.nameText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// CombatSpriteAttributesContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.nameText);
			this.Controls.Add(this.healthText);
			this.Controls.Add(this.image);
			this.Name = "CombatSpriteAttributesContainer";
			this.Size = new System.Drawing.Size(100, 95);
			((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox image;
		private System.Windows.Forms.Label healthText;
		private System.Windows.Forms.Label nameText;
	}
}
