﻿namespace Project.Controls
{
	partial class ItemContainer
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
			this.components = new System.ComponentModel.Container();
			this.nameLabel = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameLabel.Location = new System.Drawing.Point(0, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(200, 15);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.Text = "label1";
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 50;
			this.toolTip.AutoPopDelay = 10000;
			this.toolTip.InitialDelay = 50;
			this.toolTip.ReshowDelay = 10;
			this.toolTip.ShowAlways = true;
			// 
			// ItemContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
			this.Name = "ItemContainer";
			this.Size = new System.Drawing.Size(200, 15);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
