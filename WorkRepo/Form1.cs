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
		private BindingList<string> sourceFilesList = new BindingList<string>();
		private BindingList<string> messagesList = new BindingList<string>();

		public Form1()
		{
			InitializeComponent();
			listBoxSourceFiles.DataSource = sourceFilesList;
			listBoxMessages.DataSource = messagesList;
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
			foreach(var file in droppedFiles)
			{
				if (File.Exists(file) &&!sourceFilesList.Contains(file))
					sourceFilesList.Add(file);
			}
		}

		DataSet excelWorkBook;

		private void listBoxSourceFiles_DoubleClick(object sender, EventArgs e)
		{
			var lb = sender as ListBox;
			if (lb.SelectedIndex == -1) return;

			var selectedFile = lb.SelectedItem.ToString();

			using (var fs = File.Open(selectedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var excelReader = ExcelReaderFactory.CreateReader(fs))
			{
				listBoxSheets.Items.Clear();
				excelWorkBook = excelReader.AsDataSet();
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
			ShowSourceRecords();
		}

		private void buttonShowRawData_Click(object sender, EventArgs e)
		{
			ShowSourceRecords();
		}

		private void ShowSourceRecords()
		{
			messagesList.Clear();
			if (workRecord.ErrorMessages.Count != 0)
			{
				foreach (var msg in workRecord.ErrorMessages)
					messagesList.Add(msg);
			}
			dataGridView1.DataSource = workRecord.AsDataTable();
//			dataGridView1.Columns[WorkRecordTable.ColumnIndexDate].DefaultCellStyle.Format = "MM/dd";
//			dataGridView1.Columns[WorkRecordTable.ColumnIndexWorkStartTime].DefaultCellStyle.Format = "HH:mm";
//			dataGridView1.Columns[WorkRecordTable.WorkEndTimeColumnIndex].DefaultCellStyle.Format = "HH:mm";
//			dataGridView1.Columns[WorkRecordTable.TaskTimeColumnIndex].DefaultCellStyle.Format = "HH:mm";
		}

		private void buttonStatistics_Click(object sender, EventArgs e)
		{

			dataGridView1.DataSource = workRecord.GetStatistics();
		}
	}
}
