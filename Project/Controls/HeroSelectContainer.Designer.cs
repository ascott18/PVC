namespace Project.Controls
{
	partial class HeroSelectContainer
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
			this.desc = new System.Windows.Forms.Label();
			this.heroContainer = new Project.Controls.HeroContainer();
			this.SuspendLayout();
			// 
			// desc
			// 
			this.desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.desc.Location = new System.Drawing.Point(226, 5);
			this.desc.Name = "desc";
			this.desc.Size = new System.Drawing.Size(170, 90);
			this.desc.TabIndex = 1;
			this.desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// heroContainer
			// 
			this.heroContainer.BackColor = System.Drawing.Color.Transparent;
			this.heroContainer.Location = new System.Drawing.Point(0, 5);
			this.heroContainer.Name = "heroContainer";
			this.heroContainer.Size = new System.Drawing.Size(220, 95);
			this.heroContainer.TabIndex = 0;
			this.heroContainer.Target = null;
			// 
			// HeroSelectContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.desc);
			this.Controls.Add(this.heroContainer);
			this.Name = "HeroSelectContainer";
			this.Size = new System.Drawing.Size(401, 100);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.HeroSelectContainer_Paint);
			this.ResumeLayout(false);

		}

		#endregion

		private HeroContainer heroContainer;
		private System.Windows.Forms.Label desc;
	}
}
