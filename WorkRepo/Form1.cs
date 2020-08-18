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
			var droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			listBoxSourceFiles.Items.AddRange(droppedFiles);

		}

		private void listBoxSourceFiles_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
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

		private void listBoxSheets_DoubleClick(object sender, EventArgs e)
		{
			var lb = sender as ListBox;
			if (lb.SelectedIndex == -1) return;

			var sheetName = lb.SelectedItem.ToString();
			var dt = excelWorkBook.Tables[sheetName];
			var rows = GetValuesAsArraysFrom(dt);

			foreach(var obj in rows[4])
			{
				Trace.WriteLine($"{obj?.ToString()},  {obj?.GetType()}");
			}
		}

		private object[][] GetValuesAsArraysFrom(DataTable dt)
		{
			var rows = new List<object[]>();
			foreach (DataRow row in dt.Rows)
			{
				var elemtns = new List<object>();
				for (var i = 0; i < dt.Columns.Count; i++)
				{
					elemtns.Add(row[i]);
				}
				rows.Add(elemtns.Select(obj=>obj is System.DBNull?null:obj).ToArray());
			}
			return rows.ToArray();
		}

	}
}
