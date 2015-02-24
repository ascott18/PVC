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
			this.components = new System.ComponentModel.Container();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 50;
			this.toolTip.AutoPopDelay = 10000;
			this.toolTip.InitialDelay = 50;
			this.toolTip.ReshowDelay = 10;
			// 
			// SpellContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DoubleBuffered = true;
			this.Name = "SpellContainer";
			this.Size = new System.Drawing.Size(130, 16);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;


	}
}
