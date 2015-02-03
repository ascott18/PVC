namespace Project.Controls
{
	partial class HeroInventoryContainer
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
			this.name = new System.Windows.Forms.Label();
			this.image = new System.Windows.Forms.PictureBox();
			this.equipmentContainer = new System.Windows.Forms.GroupBox();
			this.equipmentFlow = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
			this.equipmentContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// name
			// 
			this.name.AutoSize = true;
			this.name.Location = new System.Drawing.Point(4, 4);
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size(35, 13);
			this.name.TabIndex = 0;
			this.name.Text = "label1";
			// 
			// image
			// 
			this.image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.image.Location = new System.Drawing.Point(7, 21);
			this.image.Name = "image";
			this.image.Size = new System.Drawing.Size(50, 50);
			this.image.TabIndex = 1;
			this.image.TabStop = false;
			// 
			// equipmentContainer
			// 
			this.equipmentContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.equipmentContainer.Controls.Add(this.equipmentFlow);
			this.equipmentContainer.Location = new System.Drawing.Point(63, 4);
			this.equipmentContainer.Name = "equipmentContainer";
			this.equipmentContainer.Size = new System.Drawing.Size(242, 102);
			this.equipmentContainer.TabIndex = 2;
			this.equipmentContainer.TabStop = false;
			this.equipmentContainer.Text = "Equipment";
			// 
			// equipmentFlow
			// 
			this.equipmentFlow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.equipmentFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.equipmentFlow.Location = new System.Drawing.Point(3, 16);
			this.equipmentFlow.Name = "equipmentFlow";
			this.equipmentFlow.Size = new System.Drawing.Size(236, 83);
			this.equipmentFlow.TabIndex = 0;
			this.equipmentFlow.WrapContents = false;
			// 
			// HeroInventoryContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.equipmentContainer);
			this.Controls.Add(this.image);
			this.Controls.Add(this.name);
			this.Name = "HeroInventoryContainer";
			this.Size = new System.Drawing.Size(308, 121);
			((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
			this.equipmentContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label name;
		private System.Windows.Forms.PictureBox image;
		private System.Windows.Forms.GroupBox equipmentContainer;
		private System.Windows.Forms.FlowLayoutPanel equipmentFlow;
	}
}
