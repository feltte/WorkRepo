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

		public Form1()
		{
			InitializeComponent();
			listBoxSourceFiles.DataSource = sourceFilesList;
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
				excelWorkBook = excelReader.AsDataSet();
				AggregateAllSheets();
			}
		}

		private void AggregateAllSheets()
		{
			// {人名, {タスク種類, 合計時間}}の辞書
			var personalAggregates = new Dictionary<string, Dictionary<string, TimeSpan>>();
			// タスク種類集計用のハッシュ(非重複リストとして)
			var usedTaskTypesHash = new HashSet<string>();

			foreach(DataTable table in excelWorkBook.Tables)
			{
				if((table.Rows[0][0] as string)!=null && (table.Rows[0][0] as string)=="日付")	// 最初のセルが"日付"のシートをデータシートして扱う
				{
					var personalData = new WorkRecordTable(table);
					var aggregates = personalData.Aggregate();
					long timeSum = 0;
					foreach(var time in aggregates.Values)
					{
						timeSum += time.Ticks;
					}
					aggregates.Add("合計", new TimeSpan(timeSum));
					personalAggregates.Add(table.TableName, aggregates);
					foreach(var taskType in aggregates.Keys)
					{
						if(!string.IsNullOrWhiteSpace(taskType) && !string.IsNullOrEmpty(taskType))	usedTaskTypesHash.Add(taskType);
					}
				}
			}
			var usedTaskTypesList = usedTaskTypesHash.ToList();
			usedTaskTypesList.Sort();
			var dt = new DataTable();

			// カラム作成
			dt.Columns.Add("項目", typeof(string));
			foreach(var person in personalAggregates.Keys)
			{
				dt.Columns.Add(person, typeof(TimeSpan));
			}
			dt.Columns.Add("合計", typeof(TimeSpan));

			// 項目カラムにタスク種類を設定しつつ、各人のセルにセットすべき値があれば値をセット
			foreach (var taskType in usedTaskTypesList)
			{
				var row = dt.NewRow();
				row["項目"] = taskType;
				foreach(var person in personalAggregates.Keys)
				{
					if(personalAggregates[person].ContainsKey(taskType))
					{
						row[person] = (personalAggregates[person][taskType]);
					}
				}
				dt.Rows.Add(row);
			}

			// 各タスク種類の合計を計算
			foreach(DataRow row in dt.Rows)
			{
				long totalTime = 0;
				for (var i = 1; i < dt.Columns.Count - 2; i++)
				{
					Trace.Write($"{i}={row[i].GetType()},");
					if (!(row[i] is DBNull))
					{
						Trace.Write($"{row[i]}");
						totalTime += ((TimeSpan)row[i]).Ticks;
					}
					Trace.WriteLine("");
				}
				row["合計"] = new TimeSpan(totalTime);
			}

			dataGridView1.DataSource = dt;
			dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
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
			var dt = workRecord.ToDataTable();

			dataGridView1.DataSource = dt;

			dataGridView1.Columns[WorkRecordTable.ColumnParams["Date"].Index].DefaultCellStyle.Format = "MM/dd";
			dataGridView1.Columns[WorkRecordTable.ColumnParams["StartTime"].Index].DefaultCellStyle.Format = "HH:mm";
			dataGridView1.Columns[WorkRecordTable.ColumnParams["EndTime"].Index].DefaultCellStyle.Format = "HH:mm";
		}

		private void buttonStatistics_Click(object sender, EventArgs e)
		{
			//dataGridView1.DataSource = workRecord.GetStatistics();
		}

		private void buttonAggregate_Click(object sender, EventArgs e)
		{


		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.Value != null && e.Value != DBNull.Value && e.Value.GetType() == typeof(TimeSpan))
			{
				var value = (TimeSpan)e.Value;
				e.Value = (value.Days * 24 + value.Hours).ToString() + ":" + value.Minutes.ToString("00");
				e.FormattingApplied = true;
			}
		}
	}
}
