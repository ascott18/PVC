namespace Project.Controls
{
	partial class SpellContainer
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
			this.spellName = new System.Windows.Forms.Label();
			this.castControlLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// spellName
			// 
			this.spellName.BackColor = System.Drawing.Color.Transparent;
			this.spellName.Dock = System.Windows.Forms.DockStyle.Right;
			this.spellName.Location = new System.Drawing.Point(14, 0);
			this.spellName.Name = "spellName";
			this.spellName.Size = new System.Drawing.Size(106, 16);
			this.spellName.TabIndex = 0;
			this.spellName.Text = "<Spell>";
			this.spellName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// castControlLabel
			// 
			this.castControlLabel.BackColor = System.Drawing.Color.Transparent;
			this.castControlLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.castControlLabel.Location = new System.Drawing.Point(0, 0);
			this.castControlLabel.Name = "castControlLabel";
			this.castControlLabel.Size = new System.Drawing.Size(12, 16);
			this.castControlLabel.TabIndex = 1;
			this.castControlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SpellContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.castControlLabel);
			this.Controls.Add(this.spellName);
			this.Name = "SpellContainer";
			this.Size = new System.Drawing.Size(120, 16);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label spellName;
		private System.Windows.Forms.Label castControlLabel;
	}
}
