namespace Project.Controls
{
	partial class InventoryScreen
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
			this.inventoryContainer = new System.Windows.Forms.GroupBox();
			this.inventoryFlow = new Project.Controls.ItemFlowPanel();
			this.inventoryContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// inventoryContainer
			// 
			this.inventoryContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inventoryContainer.Controls.Add(this.inventoryFlow);
			this.inventoryContainer.Location = new System.Drawing.Point(338, 0);
			this.inventoryContainer.Name = "inventoryContainer";
			this.inventoryContainer.Size = new System.Drawing.Size(246, 373);
			this.inventoryContainer.TabIndex = 3;
			this.inventoryContainer.TabStop = false;
			this.inventoryContainer.Text = "Inventory";
			// 
			// inventoryFlow
			// 
			this.inventoryFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inventoryFlow.AutoScroll = true;
			this.inventoryFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.inventoryFlow.Location = new System.Drawing.Point(3, 16);
			this.inventoryFlow.Name = "inventoryFlow";
			this.inventoryFlow.Size = new System.Drawing.Size(240, 354);
			this.inventoryFlow.TabIndex = 0;
			this.inventoryFlow.WrapContents = false;
			// 
			// InventoryScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(589, 376);
			this.Controls.Add(this.inventoryContainer);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InventoryScreen";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Inventory";
			this.inventoryContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox inventoryContainer;
		private ItemFlowPanel inventoryFlow;
	}
}
