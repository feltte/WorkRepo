using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkRepo
{
	class MyTable
	{
		private object[][] values;

		public int RowsCount { get { return values.Length; } }

		protected MyTable() { } // データなしでの生成は認めない

		public object ValueAt(int row, int column) { return values[row][column]; }
		public object SetValueAt(int row, int column, object value)
		{
			values[row][column] = value;
			return value;
		}

		public IReadOnlyList<object[]> Rows { get { return values; } }
		public IReadOnlyList<object> RowAt(int row)
		{
			return values[row];
		}
		public IReadOnlyList<object[]> RowsFrom(int rowIndex)
		{
			return values.Skip(rowIndex).ToArray();
		}

		/// <summary>
		/// DataTableからデータを読み込んでインスタンス化する
		/// </summary>
		/// <param name="dt">読み込むDataTable</param>
		/// <param name="nullValue">空欄(DBNull)の代わりに代入する値</param>
		public MyTable(DataTable dt, object nullValue)
		{
			var columnCount = -1;
			var rows = new List<object[]>();
			foreach (DataRow row in dt.Rows)
			{
				var elemtns = new List<object>();
				for (var i = 0; i < dt.Columns.Count; i++)
				{
					elemtns.Add(row[i]);
				}
				if(columnCount == -1)
				{
					columnCount = elemtns.Count;
				}
				else if(columnCount != elemtns.Count)
				{
					throw new ApplicationException("Column counts are not same.");
				}
				rows.Add(elemtns.Select(obj => obj is System.DBNull ? nullValue : obj).ToArray());
			}
			this.values = rows.ToArray();
		}

		/// <summary>
		/// 内部データをDataTableとして取り出す
		/// </summary>
		/// <param name="startRowIndex">取り出し始めの行番号</param>
		/// <param name="endRowIndex">終わりの行番号</param>
		/// <returns></returns>
		public DataTable AsTable(int startRowIndex, int endRowIndex)
		{
			if (values == null) throw new ApplicationException("Data is not ready.");

			var columnCount = values[0].Length;	// データ読み込み時に子配列の要素数=列数が同じなことが担保されているので、最初の横配列だけを見れば良い。

			var dt = new DataTable();
			for (var i = 0; i < columnCount; i++)	dt.Columns.Add();

			for (var i = startRowIndex; i < endRowIndex; i++)
			{
				var dr = dt.NewRow();
				for (var j = 0; j < columnCount; j++)	dr[j] = values[i][j];
				dt.Rows.Add(dr);
			}
			return dt;
		}
	}

	class WorkRecordTable : MyTable
	{
		static class TableIndexes
		{
			static public readonly int RecordStartRow = 4;

			static public readonly int Date = 0;
			static public readonly int PlanAM = 2;
			static public readonly int PlanPM = 3;
			static public readonly int ActualAM = 4;
			static public readonly int ActualPM = 5;
			static public readonly int WorkType= 7;
			static public readonly int WorkStartTime = 8;
			static public readonly int WorkEndTime = 9;
			static public readonly int PlanTask = 11;
			static public readonly int DoneTask = 12;
			static public readonly int TaskType = 13;
			static public readonly int TaskTime = 14;
		}

		private WorkRecordTable() { }

		public IReadOnlyList<string> ErrorMessages { get; private set; }

		public WorkRecordTable(DataTable dt) : base(dt, null)
		{
			FillBlankDate();
			ErrorMessages = ConfirmValues();
		}

		public void FillBlankDate()
		{
			DateTime date = new DateTime();
			for(var i=TableIndexes.RecordStartRow;i<base.RowsCount;i++)
			{
				if (ValueAt(i, TableIndexes.Date) != null) date = (DateTime)ValueAt(i, TableIndexes.Date);
				else if (RowAt(i).Any(value => value != null)) SetValueAt(i, TableIndexes.Date, date);
			}
		}

		public IReadOnlyList<string> ConfirmValues()
		{
			var messages = new List<string>();

			var records = Rows.Skip(TableIndexes.RecordStartRow);

			// 仕事タイプと開始・終了時刻の検証
			var workTimeRecords = records
				.Where(record => record[TableIndexes.WorkType] != null || record[TableIndexes.WorkStartTime] != null || record[TableIndexes.WorkEndTime] != null)
				.Select(record => new 
				{
					Date = record[TableIndexes.Date],
					Type = record[TableIndexes.WorkType], 
					Start = record[TableIndexes.WorkStartTime], 
					End = record[TableIndexes.WorkEndTime]
				});
			// 入力に不足がないか
			var incompleteWorkTimeRecords = workTimeRecords.Where(record => record.Type == null || record.Start == null || record.End == null);
			if(incompleteWorkTimeRecords.Any())
			{
				foreach(var record in incompleteWorkTimeRecords)
				{
					messages.Add($"{((DateTime)(record.Date)).ToString("MM/dd")}の開始/終了時刻記録が不足しています");
				}
			}
			// 時刻が逆転していないか
			var invalidWorkTimeRecords = workTimeRecords.Where(record => (DateTime)(record.Start) > (DateTime)(record.End));
			if(invalidWorkTimeRecords.Any())
			{
				foreach (var record in invalidWorkTimeRecords)
				{
					messages.Add($"{((DateTime)(record.Date)).ToString("MM/dd")}の開始/終了時刻記録が矛盾しています");
				}
			}

			// 通算時間の確認
			var incompleteTaskRecords = records.Where(record => record[TableIndexes.DoneTask] != null)
				.Select(record => new
				{
					Date = record[TableIndexes.Date],
					Time = record[TableIndexes.TaskTime]
				})
				.Where(record => record.Time == null);
			if(incompleteTaskRecords.Any())
			{
				foreach(var record in incompleteTaskRecords)
				{
					messages.Add($"{((DateTime)(record.Date)).ToString("MM/dd")}に時刻未記入の業務記載があります");
				}
			}
			return messages;
		}


		private class TaskElement
		{
			public DateTime Date { get; set; }
			public String Type { get; set; }
			public String Detail { get; set; }
			public TimeSpan Time { get; set; }

			static public TaskElement FromRecordRow(IReadOnlyList<object> row)
			{
				return new TaskElement()
				{
					Date = (DateTime)row[TableIndexes.Date],
					Type = (string)row[TableIndexes.TaskType],
					Detail = (string)(string)row[TableIndexes.DoneTask],
					Time = (TimeSpan)row[TableIndexes.TaskTime]
				};
			}
		}

		private DataTable TableFrom(IReadOnlyList<TaskElement> taskElements)
		{
			var dt = new DataTable();

			var propertyInfos = typeof(TaskElement).GetProperties();

			foreach (var pi in propertyInfos)
			{
				dt.Columns.Add(pi.Name);
			}
			foreach(var element in taskElements)
			{
				var dr = dt.NewRow();
				foreach (var pi in propertyInfos)
				{
					dr[pi.Name] = pi.GetValue(element);
				}
				dt.Rows.Add(dr);
			}
			return dt;
		}

		public DataTable GetStatistics()
		{
			var allRecords = base.Rows.Skip(TableIndexes.RecordStartRow);

			var supportTasks = allRecords
				.Where(row => (row[TableIndexes.TaskType] as string) == "事業支援")
				.Select(row => TaskElement.FromRecordRow(row));
			var supportTaskTimeSum = new TimeSpan(supportTasks.Sum(record => record.Time.Ticks));
			var supportTaskSumElement = new TaskElement()
			{
				Date = (DateTime)supportTasks.Last().Date,
				Type = "事業支援",
				Detail = "集計",
				Time = supportTaskTimeSum,
			};

			var researchTasks = allRecords
				.Where(row => (row[TableIndexes.TaskType] as string) == "その他")
				.Select(row => TaskElement.FromRecordRow(row));
			var researchTaskTimeSum = new TimeSpan(researchTasks.Sum(record => record.Time.Ticks));
			var researchTaskSumElement = new TaskElement()
			{
				Date = (DateTime)researchTasks.Last().Date,
				Type = "開発",
				Detail = "集計",
				Time = researchTaskTimeSum,
			};

			var elements = new List<TaskElement>();
			elements.AddRange(supportTasks.ToArray());
			elements.Add(supportTaskSumElement);
			elements.AddRange(researchTasks.ToArray());
			elements.Add(researchTaskSumElement);

			return TableFrom(elements);
		}


	}
}
