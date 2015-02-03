namespace Project.Controls
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
			this.AttributesContainer = new Project.Controls.CombatSpriteAttributesContainer();
			this.targetImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.targetImage)).BeginInit();
			this.SuspendLayout();
			// 
			// AttributesContainer
			// 
			this.AttributesContainer.BackColor = System.Drawing.Color.Transparent;
			this.AttributesContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.AttributesContainer.Location = new System.Drawing.Point(109, 0);
			this.AttributesContainer.Name = "AttributesContainer";
			this.AttributesContainer.Size = new System.Drawing.Size(100, 95);
			this.AttributesContainer.TabIndex = 0;
			// 
			// targetImage
			// 
			this.targetImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.targetImage.Location = new System.Drawing.Point(62, 15);
			this.targetImage.Name = "targetImage";
			this.targetImage.Size = new System.Drawing.Size(24, 24);
			this.targetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.targetImage.TabIndex = 1;
			this.targetImage.TabStop = false;
			this.targetImage.EnabledChanged += new System.EventHandler(this.targetImage_EnabledChanged);
			// 
			// CombatSpriteContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.targetImage);
			this.Controls.Add(this.AttributesContainer);
			this.Name = "CombatSpriteContainer";
			this.Size = new System.Drawing.Size(209, 95);
			((System.ComponentModel.ISupportInitialize)(this.targetImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion


		protected CombatSpriteAttributesContainer AttributesContainer;
		protected internal System.Windows.Forms.PictureBox targetImage;
	}
}
