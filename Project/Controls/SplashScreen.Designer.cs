namespace Project.Controls
{
	partial class SplashScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
			this.titleLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.debugStart = new System.Windows.Forms.Button();
			this.debugStartMapID = new System.Windows.Forms.TextBox();
			this.exitButton = new Project.Controls.WoodButton();
			this.newGameButton = new Project.Controls.WoodButton();
			this.SuspendLayout();
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.titleLabel.ForeColor = System.Drawing.Color.Black;
			this.titleLabel.Location = new System.Drawing.Point(82, 9);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(123, 55);
			this.titleLabel.TabIndex = 1;
			this.titleLabel.Text = "PVC";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(67, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(156, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "(no relation to polyvinyl chloride)";
			// 
			// debugStart
			// 
			this.debugStart.Location = new System.Drawing.Point(49, 108);
			this.debugStart.Name = "debugStart";
			this.debugStart.Size = new System.Drawing.Size(100, 23);
			this.debugStart.TabIndex = 3;
			this.debugStart.Text = "Just testing, boss!";
			this.debugStart.UseVisualStyleBackColor = true;
			this.debugStart.Visible = false;
			this.debugStart.Click += new System.EventHandler(this.debugStart_Click);
			// 
			// debugStartMapID
			// 
			this.debugStartMapID.Location = new System.Drawing.Point(156, 110);
			this.debugStartMapID.Name = "debugStartMapID";
			this.debugStartMapID.Size = new System.Drawing.Size(86, 20);
			this.debugStartMapID.TabIndex = 4;
			this.debugStartMapID.Text = "111";
			this.debugStartMapID.Visible = false;
			// 
			// exitButton
			// 
			this.exitButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exitButton.BackgroundImage")));
			this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.exitButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exitButton.ForeColor = System.Drawing.Color.Tan;
			this.exitButton.Location = new System.Drawing.Point(79, 200);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(135, 37);
			this.exitButton.TabIndex = 0;
			this.exitButton.Text = "Exit";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// newGameButton
			// 
			this.newGameButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("newGameButton.BackgroundImage")));
			this.newGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.newGameButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.newGameButton.ForeColor = System.Drawing.Color.Tan;
			this.newGameButton.Location = new System.Drawing.Point(79, 157);
			this.newGameButton.Name = "newGameButton";
			this.newGameButton.Size = new System.Drawing.Size(135, 37);
			this.newGameButton.TabIndex = 0;
			this.newGameButton.Text = "New Game";
			this.newGameButton.UseVisualStyleBackColor = true;
			this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
			// 
			// SplashScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.debugStartMapID);
			this.Controls.Add(this.debugStart);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.titleLabel);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.newGameButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SplashScreen";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private WoodButton newGameButton;
		private System.Windows.Forms.Label titleLabel;
		private WoodButton exitButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button debugStart;
		private System.Windows.Forms.TextBox debugStartMapID;
	}
}