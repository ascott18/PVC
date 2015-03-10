namespace Project
{
    partial class ChestLootDialog
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
			this.closeButton = new System.Windows.Forms.Button();
			this.lootedItemsLabel = new System.Windows.Forms.Label();
			this.itemFlowPanel = new Project.Controls.ItemFlowPanel();
			this.chestImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.chestImage)).BeginInit();
			this.SuspendLayout();
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
			// lootedItemsLabel
			// 
			this.lootedItemsLabel.AutoSize = true;
			this.lootedItemsLabel.Location = new System.Drawing.Point(95, 13);
			this.lootedItemsLabel.Name = "lootedItemsLabel";
			this.lootedItemsLabel.Size = new System.Drawing.Size(71, 13);
			this.lootedItemsLabel.TabIndex = 6;
			this.lootedItemsLabel.Text = "Looted Items:";
			// 
			// itemFlowPanel
			// 
			this.itemFlowPanel.AutoScroll = true;
			this.itemFlowPanel.Location = new System.Drawing.Point(172, 13);
			this.itemFlowPanel.Name = "itemFlowPanel";
			this.itemFlowPanel.Size = new System.Drawing.Size(220, 96);
			this.itemFlowPanel.TabIndex = 5;
			// 
			// chestImage
			// 
			this.chestImage.Location = new System.Drawing.Point(13, 13);
			this.chestImage.Name = "chestImage";
			this.chestImage.Size = new System.Drawing.Size(50, 50);
			this.chestImage.TabIndex = 7;
			this.chestImage.TabStop = false;
			// 
			// ChestLootDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 150);
			this.Controls.Add(this.chestImage);
			this.Controls.Add(this.lootedItemsLabel);
			this.Controls.Add(this.itemFlowPanel);
			this.Controls.Add(this.closeButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChestLootDialog";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "You received loot!";
			((System.ComponentModel.ISupportInitialize)(this.chestImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button closeButton;
		private Controls.ItemFlowPanel itemFlowPanel;
		private System.Windows.Forms.Label lootedItemsLabel;
		private System.Windows.Forms.PictureBox chestImage;
	}
}