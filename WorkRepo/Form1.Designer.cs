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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonShowRawData = new System.Windows.Forms.Button();
			this.buttonStatistics = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxSourceFiles
			// 
			this.listBoxSourceFiles.AllowDrop = true;
			this.listBoxSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSourceFiles.FormattingEnabled = true;
			this.listBoxSourceFiles.ItemHeight = 12;
			this.listBoxSourceFiles.Location = new System.Drawing.Point(12, 24);
			this.listBoxSourceFiles.Name = "listBoxSourceFiles";
			this.listBoxSourceFiles.Size = new System.Drawing.Size(652, 172);
			this.listBoxSourceFiles.TabIndex = 0;
			this.listBoxSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragDrop);
			this.listBoxSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragEnter);
			this.listBoxSourceFiles.DoubleClick += new System.EventHandler(this.listBoxSourceFiles_DoubleClick);
			// 
			// listBoxSheets
			// 
			this.listBoxSheets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSheets.FormattingEnabled = true;
			this.listBoxSheets.ItemHeight = 12;
			this.listBoxSheets.Location = new System.Drawing.Point(670, 24);
			this.listBoxSheets.Name = "listBoxSheets";
			this.listBoxSheets.Size = new System.Drawing.Size(118, 172);
			this.listBoxSheets.TabIndex = 1;
			this.listBoxSheets.DoubleClick += new System.EventHandler(this.listBoxSheets_DoubleClick);
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(11, 31);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(778, 197);
			this.dataGridView1.TabIndex = 2;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.listBoxSourceFiles);
			this.splitContainer1.Panel1.Controls.Add(this.listBoxSheets);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonStatistics);
			this.splitContainer1.Panel2.Controls.Add(this.buttonShowRawData);
			this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainer1.Size = new System.Drawing.Size(800, 450);
			this.splitContainer1.SplitterDistance = 207;
			this.splitContainer1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "業務記録Excelファイル登録&&選択";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(668, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "シート選択";
			// 
			// buttonShowRawData
			// 
			this.buttonShowRawData.Location = new System.Drawing.Point(12, 3);
			this.buttonShowRawData.Name = "buttonShowRawData";
			this.buttonShowRawData.Size = new System.Drawing.Size(75, 23);
			this.buttonShowRawData.TabIndex = 3;
			this.buttonShowRawData.Text = "生データ";
			this.buttonShowRawData.UseVisualStyleBackColor = true;
			this.buttonShowRawData.Click += new System.EventHandler(this.buttonShowRawData_Click);
			// 
			// buttonStatistics
			// 
			this.buttonStatistics.Location = new System.Drawing.Point(107, 3);
			this.buttonStatistics.Name = "buttonStatistics";
			this.buttonStatistics.Size = new System.Drawing.Size(75, 23);
			this.buttonStatistics.TabIndex = 4;
			this.buttonStatistics.Text = "統計";
			this.buttonStatistics.UseVisualStyleBackColor = true;
			this.buttonStatistics.Click += new System.EventHandler(this.buttonStatistics_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxSourceFiles;
		private System.Windows.Forms.ListBox listBoxSheets;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonStatistics;
		private System.Windows.Forms.Button buttonShowRawData;
	}
}

