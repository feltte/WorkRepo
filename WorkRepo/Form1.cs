using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;
using System.Diagnostics;

namespace WorkRepo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void listBoxSourceFiles_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void listBoxSourceFiles_DragDrop(object sender, DragEventArgs e)
		{
			var droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			listBoxSourceFiles.Items.AddRange(droppedFiles);
		}

		DataSet excelWorkBook;

		private void listBoxSourceFiles_DoubleClick(object sender, EventArgs e)
		{
			var lb = sender as ListBox;
			if (lb.SelectedIndex == -1) return;

			var selectedFile = lb.SelectedItem.ToString();
			if (!File.Exists(selectedFile))
			{
				MessageBox.Show("存在しないファイルでした", "エラー", MessageBoxButtons.OK);
				listBoxSourceFiles.Items.Remove(selectedFile);
				return;
			}

			using (var fs = File.Open(selectedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var er = ExcelReaderFactory.CreateReader(fs))
			{
				excelWorkBook = er.AsDataSet();
				foreach (DataTable dt in excelWorkBook.Tables)
				{
					listBoxSheets.Items.Add(dt.TableName);
				}
			}
		}

		WorkRecordTable workRecord;

		private void listBoxSheets_DoubleClick(object sender, EventArgs e)
		{
			var lb = sender as ListBox;
			if (lb.SelectedIndex == -1) return;

			var sheetName = lb.SelectedItem.ToString();
			var dt = excelWorkBook.Tables[sheetName];
			workRecord = new WorkRecordTable(dt);

			ShowRawData();

		}

		private void buttonShowRawData_Click(object sender, EventArgs e)
		{
			ShowRawData();
		}

		private void ShowRawData()
		{
			if (workRecord.ErrorMessages.Count != 0)
			{
				dataGridView1.DataSource = workRecord.ErrorMessages;
				return;
			}
			dataGridView1.DataSource = workRecord.AsTable(0, workRecord.RowsCount);
//			dataGridView1.Columns[WorkRecordTable.ColumnIndexDate].DefaultCellStyle.Format = "MM/dd";
//			dataGridView1.Columns[WorkRecordTable.ColumnIndexWorkStartTime].DefaultCellStyle.Format = "HH:mm";
//			dataGridView1.Columns[WorkRecordTable.WorkEndTimeColumnIndex].DefaultCellStyle.Format = "HH:mm";
//			dataGridView1.Columns[WorkRecordTable.TaskTimeColumnIndex].DefaultCellStyle.Format = "HH:mm";

		}

		private void buttonStatistics_Click(object sender, EventArgs e)
		{
			if (workRecord.ErrorMessages.Count != 0)
			{
				dataGridView1.DataSource = workRecord.ErrorMessages;
				return;
			}
			dataGridView1.DataSource = workRecord.GetStatistics();
		}
	}
}
