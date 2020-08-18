namespace WorkRepo
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.listBoxSourceFiles = new System.Windows.Forms.ListBox();
			this.listBoxSheets = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listBoxSourceFiles
			// 
			this.listBoxSourceFiles.AllowDrop = true;
			this.listBoxSourceFiles.FormattingEnabled = true;
			this.listBoxSourceFiles.ItemHeight = 12;
			this.listBoxSourceFiles.Location = new System.Drawing.Point(12, 12);
			this.listBoxSourceFiles.Name = "listBoxSourceFiles";
			this.listBoxSourceFiles.Size = new System.Drawing.Size(650, 88);
			this.listBoxSourceFiles.TabIndex = 0;
			this.listBoxSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragDrop);
			this.listBoxSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragEnter);
			this.listBoxSourceFiles.DoubleClick += new System.EventHandler(this.listBoxSourceFiles_DoubleClick);
			// 
			// listBoxSheets
			// 
			this.listBoxSheets.FormattingEnabled = true;
			this.listBoxSheets.ItemHeight = 12;
			this.listBoxSheets.Location = new System.Drawing.Point(668, 12);
			this.listBoxSheets.Name = "listBoxSheets";
			this.listBoxSheets.Size = new System.Drawing.Size(120, 88);
			this.listBoxSheets.TabIndex = 1;
			this.listBoxSheets.DoubleClick += new System.EventHandler(this.listBoxSheets_DoubleClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.listBoxSheets);
			this.Controls.Add(this.listBoxSourceFiles);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxSourceFiles;
		private System.Windows.Forms.ListBox listBoxSheets;
	}
}

