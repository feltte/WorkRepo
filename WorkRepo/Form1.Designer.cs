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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.buttonAggregate = new System.Windows.Forms.Button();
			this.buttonShowRawData = new System.Windows.Forms.Button();
			this.buttonStatistics = new System.Windows.Forms.Button();
			this.listBoxMessages = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listBoxSheets = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxSourceFiles
			// 
			this.listBoxSourceFiles.AllowDrop = true;
			this.listBoxSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSourceFiles.FormattingEnabled = true;
			this.listBoxSourceFiles.ItemHeight = 15;
			this.listBoxSourceFiles.Location = new System.Drawing.Point(16, 30);
			this.listBoxSourceFiles.Margin = new System.Windows.Forms.Padding(4);
			this.listBoxSourceFiles.Name = "listBoxSourceFiles";
			this.listBoxSourceFiles.Size = new System.Drawing.Size(859, 94);
			this.listBoxSourceFiles.TabIndex = 0;
			this.listBoxSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragDrop);
			this.listBoxSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxSourceFiles_DragEnter);
			this.listBoxSourceFiles.DoubleClick += new System.EventHandler(this.listBoxSourceFiles_DoubleClick);
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(4, 40);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(1063, 263);
			this.dataGridView1.TabIndex = 2;
			this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
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
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1067, 562);
			this.splitContainer1.SplitterDistance = 151;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(212, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "業務記録Excelファイル登録&&選択";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.buttonAggregate);
			this.splitContainer2.Panel1.Controls.Add(this.buttonShowRawData);
			this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
			this.splitContainer2.Panel1.Controls.Add(this.buttonStatistics);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.listBoxMessages);
			this.splitContainer2.Size = new System.Drawing.Size(1067, 406);
			this.splitContainer2.SplitterDistance = 303;
			this.splitContainer2.TabIndex = 5;
			// 
			// buttonAggregate
			// 
			this.buttonAggregate.Location = new System.Drawing.Point(240, 10);
			this.buttonAggregate.Name = "buttonAggregate";
			this.buttonAggregate.Size = new System.Drawing.Size(75, 23);
			this.buttonAggregate.TabIndex = 5;
			this.buttonAggregate.Text = "全集計";
			this.buttonAggregate.UseVisualStyleBackColor = true;
			this.buttonAggregate.Click += new System.EventHandler(this.buttonAggregate_Click);
			// 
			// buttonShowRawData
			// 
			this.buttonShowRawData.Location = new System.Drawing.Point(4, 4);
			this.buttonShowRawData.Margin = new System.Windows.Forms.Padding(4);
			this.buttonShowRawData.Name = "buttonShowRawData";
			this.buttonShowRawData.Size = new System.Drawing.Size(100, 29);
			this.buttonShowRawData.TabIndex = 3;
			this.buttonShowRawData.Text = "生データ";
			this.buttonShowRawData.UseVisualStyleBackColor = true;
			this.buttonShowRawData.Click += new System.EventHandler(this.buttonShowRawData_Click);
			// 
			// buttonStatistics
			// 
			this.buttonStatistics.Location = new System.Drawing.Point(112, 4);
			this.buttonStatistics.Margin = new System.Windows.Forms.Padding(4);
			this.buttonStatistics.Name = "buttonStatistics";
			this.buttonStatistics.Size = new System.Drawing.Size(100, 29);
			this.buttonStatistics.TabIndex = 4;
			this.buttonStatistics.Text = "統計";
			this.buttonStatistics.UseVisualStyleBackColor = true;
			this.buttonStatistics.Click += new System.EventHandler(this.buttonStatistics_Click);
			// 
			// listBoxMessages
			// 
			this.listBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxMessages.FormattingEnabled = true;
			this.listBoxMessages.HorizontalScrollbar = true;
			this.listBoxMessages.ItemHeight = 15;
			this.listBoxMessages.Location = new System.Drawing.Point(0, 0);
			this.listBoxMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.listBoxMessages.Name = "listBoxMessages";
			this.listBoxMessages.Size = new System.Drawing.Size(1067, 99);
			this.listBoxMessages.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(895, 11);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "シート選択";
			// 
			// listBoxSheets
			// 
			this.listBoxSheets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSheets.FormattingEnabled = true;
			this.listBoxSheets.ItemHeight = 15;
			this.listBoxSheets.Location = new System.Drawing.Point(898, 30);
			this.listBoxSheets.Margin = new System.Windows.Forms.Padding(4);
			this.listBoxSheets.Name = "listBoxSheets";
			this.listBoxSheets.Size = new System.Drawing.Size(156, 94);
			this.listBoxSheets.TabIndex = 1;
			this.listBoxSheets.DoubleClick += new System.EventHandler(this.listBoxSheets_DoubleClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 562);
			this.Controls.Add(this.splitContainer1);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxSourceFiles;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonStatistics;
		private System.Windows.Forms.Button buttonShowRawData;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListBox listBoxMessages;
		private System.Windows.Forms.Button buttonAggregate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listBoxSheets;
	}
}

