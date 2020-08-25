namespace MakeSchedule
{
	partial class schedule_MainForm
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
			this.textBoxFolderPath = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonBrowseFolder = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPersonsNames = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxFileNamePrefix = new System.Windows.Forms.TextBox();
			this.listBoxLogMessage = new System.Windows.Forms.ListBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.dataGridViewScheduleTable = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewScheduleTable)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxFolderPath
			// 
			this.textBoxFolderPath.Location = new System.Drawing.Point(126, 7);
			this.textBoxFolderPath.Name = "textBoxFolderPath";
			this.textBoxFolderPath.ReadOnly = true;
			this.textBoxFolderPath.Size = new System.Drawing.Size(512, 19);
			this.textBoxFolderPath.TabIndex = 1;
			this.textBoxFolderPath.TextChanged += new System.EventHandler(this.textBoxFolderPath_TextChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(658, 42);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(109, 55);
			this.button1.TabIndex = 9;
			this.button1.Text = "しゅうけい！";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "集計対象フォルダ";
			// 
			// buttonBrowseFolder
			// 
			this.buttonBrowseFolder.Location = new System.Drawing.Point(658, 4);
			this.buttonBrowseFolder.Name = "buttonBrowseFolder";
			this.buttonBrowseFolder.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseFolder.TabIndex = 2;
			this.buttonBrowseFolder.Text = "参照";
			this.buttonBrowseFolder.UseVisualStyleBackColor = true;
			this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "集計対象者";
			// 
			// textBoxPersonsNames
			// 
			this.textBoxPersonsNames.Location = new System.Drawing.Point(126, 57);
			this.textBoxPersonsNames.Name = "textBoxPersonsNames";
			this.textBoxPersonsNames.Size = new System.Drawing.Size(512, 19);
			this.textBoxPersonsNames.TabIndex = 6;
			this.textBoxPersonsNames.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPersonsNames_Validating);
			this.textBoxPersonsNames.Validated += new System.EventHandler(this.textBoxPersonsNames_Validated);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "ファイル名プレフィックス";
			// 
			// textBoxFileNamePrefix
			// 
			this.textBoxFileNamePrefix.Location = new System.Drawing.Point(126, 32);
			this.textBoxFileNamePrefix.Name = "textBoxFileNamePrefix";
			this.textBoxFileNamePrefix.Size = new System.Drawing.Size(264, 19);
			this.textBoxFileNamePrefix.TabIndex = 4;
			this.textBoxFileNamePrefix.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFileNamePrefix_Validating);
			this.textBoxFileNamePrefix.Validated += new System.EventHandler(this.textBoxFileNamePrefix_Validated);
			// 
			// listBoxLogMessage
			// 
			this.listBoxLogMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxLogMessage.FormattingEnabled = true;
			this.listBoxLogMessage.ItemHeight = 12;
			this.listBoxLogMessage.Location = new System.Drawing.Point(14, 107);
			this.listBoxLogMessage.Name = "listBoxLogMessage";
			this.listBoxLogMessage.Size = new System.Drawing.Size(776, 100);
			this.listBoxLogMessage.TabIndex = 10;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(14, 82);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(200, 19);
			this.dateTimePicker1.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(220, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124, 12);
			this.label4.TabIndex = 8;
			this.label4.Text = "を含む週予定を取得する";
			// 
			// dataGridViewScheduleTable
			// 
			this.dataGridViewScheduleTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewScheduleTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewScheduleTable.Location = new System.Drawing.Point(14, 213);
			this.dataGridViewScheduleTable.Name = "dataGridViewScheduleTable";
			this.dataGridViewScheduleTable.RowTemplate.Height = 21;
			this.dataGridViewScheduleTable.Size = new System.Drawing.Size(776, 225);
			this.dataGridViewScheduleTable.TabIndex = 11;
			// 
			// schedule_MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.dataGridViewScheduleTable);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.listBoxLogMessage);
			this.Controls.Add(this.textBoxFileNamePrefix);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxPersonsNames);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonBrowseFolder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBoxFolderPath);
			this.Name = "schedule_MainForm";
			this.Text = "出社予定集計";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.schedule_MainForm_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewScheduleTable)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxFolderPath;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonBrowseFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPersonsNames;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxFileNamePrefix;
		private System.Windows.Forms.ListBox listBoxLogMessage;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView dataGridViewScheduleTable;
	}
}

