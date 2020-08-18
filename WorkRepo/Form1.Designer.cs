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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// listBoxSourceFiles
			// 
			this.listBoxSourceFiles.AllowDrop = true;
			this.listBoxSourceFiles.FormattingEnabled = true;
			this.listBoxSourceFiles.ItemHeight = 15;
			this.listBoxSourceFiles.Location = new System.Drawing.Point(16, 15);
			this.listBoxSourceFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listBoxSourceFiles.Name = "listBoxSourceFiles";
			this.listBoxSourceFiles.Size = new System.Drawing.Size(865, 109);
			this.listBoxSourceFiles.TabIndex = 0;
			this.listBoxSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragDrop);
			this.listBoxSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragEnter);
			this.listBoxSourceFiles.DoubleClick += new System.EventHandler(this.listBoxSourceFiles_DoubleClick);
			// 
			// listBoxSheets
			// 
			this.listBoxSheets.FormattingEnabled = true;
			this.listBoxSheets.ItemHeight = 15;
			this.listBoxSheets.Location = new System.Drawing.Point(891, 15);
			this.listBoxSheets.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listBoxSheets.Name = "listBoxSheets";
			this.listBoxSheets.Size = new System.Drawing.Size(159, 109);
			this.listBoxSheets.TabIndex = 1;
			this.listBoxSheets.DoubleClick += new System.EventHandler(this.listBoxSheets_DoubleClick);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(16, 131);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(1034, 419);
			this.dataGridView1.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 562);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.listBoxSheets);
			this.Controls.Add(this.listBoxSourceFiles);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxSourceFiles;
		private System.Windows.Forms.ListBox listBoxSheets;
		private System.Windows.Forms.DataGridView dataGridView1;
	}
}

