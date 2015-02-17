namespace Project.Controls
{
	partial class HeroStatsContainer
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
			this.csac = new Project.Controls.CombatSpriteAttributesContainer();
			this.statsLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// csac
			// 
			this.csac.BackColor = System.Drawing.Color.Transparent;
			this.csac.Location = new System.Drawing.Point(3, 3);
			this.csac.Name = "csac";
			this.csac.Size = new System.Drawing.Size(100, 95);
			this.csac.TabIndex = 0;
			// 
			// statsLabel
			// 
			this.statsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statsLabel.Location = new System.Drawing.Point(79, 16);
			this.statsLabel.Name = "statsLabel";
			this.statsLabel.Size = new System.Drawing.Size(97, 57);
			this.statsLabel.TabIndex = 1;
			this.statsLabel.Text = "Stamina : 1\r\nStrength : 2\r\nIntellect: 4\r\nAgility: 3";
			// 
			// HeroStatsContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.statsLabel);
			this.Controls.Add(this.csac);
			this.Name = "HeroStatsContainer";
			this.Size = new System.Drawing.Size(179, 100);
			this.ResumeLayout(false);

		}

		#endregion

		private CombatSpriteAttributesContainer csac;
		private System.Windows.Forms.Label statsLabel;
	}
}
