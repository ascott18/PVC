namespace Project.Controls
{
	partial class HeroSelectScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeroSelectScreen));
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.finishedButton = new Project.Controls.WoodButton();
			this.instructions = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(816, 381);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// finishedButton
			// 
			this.finishedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.finishedButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishedButton.BackgroundImage")));
			this.finishedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.finishedButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.finishedButton.ForeColor = System.Drawing.Color.Tan;
			this.finishedButton.Location = new System.Drawing.Point(682, 399);
			this.finishedButton.Name = "finishedButton";
			this.finishedButton.Size = new System.Drawing.Size(146, 36);
			this.finishedButton.TabIndex = 1;
			this.finishedButton.Text = "Lets Go!";
			this.finishedButton.UseVisualStyleBackColor = true;
			this.finishedButton.Click += new System.EventHandler(this.finishedButton_Click);
			// 
			// instructions
			// 
			this.instructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.instructions.AutoSize = true;
			this.instructions.Location = new System.Drawing.Point(12, 412);
			this.instructions.Name = "instructions";
			this.instructions.Size = new System.Drawing.Size(161, 13);
			this.instructions.TabIndex = 2;
			this.instructions.Text = "Select up to three allies to begin.";
			// 
			// HeroSelectScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(840, 439);
			this.ControlBox = false;
			this.Controls.Add(this.instructions);
			this.Controls.Add(this.finishedButton);
			this.Controls.Add(this.flowLayoutPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(367, 194);
			this.Name = "HeroSelectScreen";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Assemble your crew";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private WoodButton finishedButton;
		private System.Windows.Forms.Label instructions;
	}
}