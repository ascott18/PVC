namespace Project
{
	partial class CombatCompleteDialog
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
			this.partyPortrait = new System.Windows.Forms.PictureBox();
			this.monsterPortrait = new System.Windows.Forms.PictureBox();
			this.versusLabel = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.itemFlowPanel1 = new Project.Controls.ItemFlowPanel();
			((System.ComponentModel.ISupportInitialize)(this.partyPortrait)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.monsterPortrait)).BeginInit();
			this.SuspendLayout();
			// 
			// partyPortrait
			// 
			this.partyPortrait.Location = new System.Drawing.Point(13, 13);
			this.partyPortrait.Name = "partyPortrait";
			this.partyPortrait.Size = new System.Drawing.Size(50, 50);
			this.partyPortrait.TabIndex = 0;
			this.partyPortrait.TabStop = false;
			// 
			// monsterPortrait
			// 
			this.monsterPortrait.Location = new System.Drawing.Point(13, 86);
			this.monsterPortrait.Name = "monsterPortrait";
			this.monsterPortrait.Size = new System.Drawing.Size(50, 50);
			this.monsterPortrait.TabIndex = 1;
			this.monsterPortrait.TabStop = false;
			// 
			// versusLabel
			// 
			this.versusLabel.Location = new System.Drawing.Point(12, 66);
			this.versusLabel.Name = "versusLabel";
			this.versusLabel.Size = new System.Drawing.Size(51, 17);
			this.versusLabel.TabIndex = 2;
			this.versusLabel.Text = "Versus";
			this.versusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(317, 115);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 4;
			this.closeButton.Text = "Neato!";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(95, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Looted Items:";
			// 
			// itemFlowPanel1
			// 
			this.itemFlowPanel1.AutoScroll = true;
			this.itemFlowPanel1.Location = new System.Drawing.Point(172, 13);
			this.itemFlowPanel1.Name = "itemFlowPanel1";
			this.itemFlowPanel1.Size = new System.Drawing.Size(220, 96);
			this.itemFlowPanel1.TabIndex = 5;
			// 
			// CombatCompleteDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 150);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.itemFlowPanel1);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.versusLabel);
			this.Controls.Add(this.monsterPortrait);
			this.Controls.Add(this.partyPortrait);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CombatCompleteDialog";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "You Won!";
			((System.ComponentModel.ISupportInitialize)(this.partyPortrait)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.monsterPortrait)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox partyPortrait;
		private System.Windows.Forms.PictureBox monsterPortrait;
		private System.Windows.Forms.Label versusLabel;
		private System.Windows.Forms.Button closeButton;
		private Controls.ItemFlowPanel itemFlowPanel1;
		private System.Windows.Forms.Label label1;
	}
}