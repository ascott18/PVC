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
			this.jokes = new System.Windows.Forms.Label();
			this.debugStart = new System.Windows.Forms.Button();
			this.debugStartMapID = new System.Windows.Forms.TextBox();
			this.exitButton = new Project.Controls.WoodButton();
			this.newGameButton = new Project.Controls.WoodButton();
			this.panel = new System.Windows.Forms.Panel();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Font = new System.Drawing.Font("Arial", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.titleLabel.ForeColor = System.Drawing.Color.Black;
			this.titleLabel.Location = new System.Drawing.Point(24, 9);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(202, 89);
			this.titleLabel.TabIndex = 1;
			this.titleLabel.Text = "PVC";
			// 
			// jokes
			// 
			this.jokes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.jokes.Location = new System.Drawing.Point(24, 85);
			this.jokes.Name = "jokes";
			this.jokes.Size = new System.Drawing.Size(202, 13);
			this.jokes.TabIndex = 2;
			this.jokes.Text = "(no relation to polyvinyl chloride)";
			this.jokes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// debugStart
			// 
			this.debugStart.Location = new System.Drawing.Point(27, 116);
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
			this.debugStartMapID.Location = new System.Drawing.Point(134, 118);
			this.debugStartMapID.Name = "debugStartMapID";
			this.debugStartMapID.Size = new System.Drawing.Size(86, 20);
			this.debugStartMapID.TabIndex = 4;
			this.debugStartMapID.Text = "111";
			this.debugStartMapID.Visible = false;
			// 
			// exitButton
			// 
			this.exitButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exitButton.BackgroundImage")));
			this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.exitButton.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exitButton.ForeColor = System.Drawing.Color.Tan;
			this.exitButton.Location = new System.Drawing.Point(57, 200);
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
			this.newGameButton.Location = new System.Drawing.Point(57, 157);
			this.newGameButton.Name = "newGameButton";
			this.newGameButton.Size = new System.Drawing.Size(135, 37);
			this.newGameButton.TabIndex = 0;
			this.newGameButton.Text = "New Game";
			this.newGameButton.UseVisualStyleBackColor = true;
			this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
			// 
			// panel
			// 
			this.panel.BackColor = System.Drawing.Color.Transparent;
			this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel.Controls.Add(this.debugStartMapID);
			this.panel.Controls.Add(this.debugStart);
			this.panel.Controls.Add(this.jokes);
			this.panel.Controls.Add(this.titleLabel);
			this.panel.Controls.Add(this.exitButton);
			this.panel.Controls.Add(this.newGameButton);
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(250, 261);
			this.panel.TabIndex = 5;
			// 
			// SplashScreen
			// 
			this.AcceptButton = this.newGameButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.exitButton;
			this.ClientSize = new System.Drawing.Size(250, 261);
			this.Controls.Add(this.panel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SplashScreen";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			this.panel.ResumeLayout(false);
			this.panel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private WoodButton newGameButton;
		private System.Windows.Forms.Label titleLabel;
		private WoodButton exitButton;
		private System.Windows.Forms.Label jokes;
		private System.Windows.Forms.Button debugStart;
		private System.Windows.Forms.TextBox debugStartMapID;
		private System.Windows.Forms.Panel panel;
	}
}