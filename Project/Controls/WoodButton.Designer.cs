namespace Project.Controls
{
	partial class WoodButton
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
			this.SuspendLayout();
			// 
			// WoodButton
			// 
			this.BackgroundImage = global::Project.Properties.Resources.wood;
			this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.WoodButton_Paint);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
