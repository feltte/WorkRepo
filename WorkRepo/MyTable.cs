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

		protected MyTable() { }		// データなしでの生成は不可

		public object ValueAt(int row, int column) { return values[row][column]; }
		public object SetValueAt(int row, int column, object value)
		{
			values[row][column] = value;
			return value;
		}
		public IReadOnlyList<object[]> Rows { get { return values; } }
		public IReadOnlyList<object[]> RowsFrom(int rowIndex){return values.Skip(rowIndex).ToArray();}
		public IReadOnlyList<object> RowAt(int row) { return values[row]; }

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

		public int ReplaceBlank(object to)
		{
			int count = 0;
			for(var i = 0; i<values.Length;i++)
			{
				for(var j = 0;j<values[0].Length;j++)
				{
					if (values[i][j] is string && string.IsNullOrWhiteSpace((string)values[i][j]))
					{
						values[i][j] = to;
						count++;
					}
				}
			}
			return count;
		}

		/// <summary>
		/// 内部データをDataTableとして取り出す
		/// </summary>
		/// <param name="startRowIndex">取り出し始めの行番号</param>
		/// <param name="endRowIndex">終わりの行番号</param>
		/// <returns></returns>
		public DataTable AsDataTable(int startRowIndex, int endRowIndex)
		{
			if (values == null) throw new ApplicationException("Data is not ready.");

			var columnCount = values[0].Length;	// データ読み込み時に子配列の要素数=列数が同じなことは確認済みなので、最初の配列だけを見れば良い。

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
		static private class TableIndexes
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

			/// <summary>
			/// DateTimeでなくTimeSpanとして扱いたいカラムのリスト
			/// 開始/終了時刻はDateTimeであるべきだが、ファイルでは月日未記入で起源日(1899/12/31)と認識されてしまうので
			/// あえてTimeSpanにしておく（行頭の日付値と足し算すれば時刻にできる）
			/// </summary>
			static public readonly int[] TimeSpanColumns = { WorkStartTime, WorkEndTime, TaskTime, };
		}

		static readonly DateTime ExcelOriginDate = new DateTime(1899, 12, 31);

		private WorkRecordTable() { }

		public IReadOnlyList<string> ErrorMessages { get; private set; }

		public WorkRecordTable(DataTable dt) : base(dt, null)
		{
			ReplaceBlank(null);
			FillBlankDate();
			FixTimeValues();
			ErrorMessages = CheckUpValues();
		}

		public DataTable AsDataTable()
		{
			return base.AsDataTable(TableIndexes.RecordStartRow, RowsCount);
		}

		private void FillBlankDate()
		{
			DateTime date = new DateTime();
			for(var i=TableIndexes.RecordStartRow;i<base.RowsCount;i++)
			{
				if (ValueAt(i, TableIndexes.Date) != null) date = (DateTime)ValueAt(i, TableIndexes.Date);
				else if (RowAt(i).Any(value => value != null)) SetValueAt(i, TableIndexes.Date, date);	// 1カ所でも記入されいてるセルがある場合は日付を入れる
			}
		}

		private void FixTimeValues()
		{
			// TimeSpanとして扱われるべき項目がDateTimeとして格納されいてるのをTimeSpanに直す
			for (var rowIdx = TableIndexes.RecordStartRow; rowIdx < base.RowsCount; rowIdx++)
			{
				foreach(var colIdx in TableIndexes.TimeSpanColumns)
				{
					if (ValueAt(rowIdx, colIdx) != null)
					{
						var value = (DateTime)ValueAt(rowIdx, colIdx);
						var span = value - ExcelOriginDate;
						SetValueAt(rowIdx, colIdx, span);
					}
				}
			}
		}

		public IReadOnlyList<string> CheckUpValues()
		{
			var messages = new List<string>();

			var records = Rows.Skip(TableIndexes.RecordStartRow);

			// 仕事タイプと開始・終了時刻の検証
			// 勤務種類, 開始時刻, 終了時刻のいずれかが記入されている行を抽出する
			var workTimeRecords = records
				.Where(record => record[TableIndexes.WorkType] != null || record[TableIndexes.WorkStartTime] != null || record[TableIndexes.WorkEndTime] != null)
				.Select(record => new 
				{
					Date = record[TableIndexes.Date],
					Type = record[TableIndexes.WorkType], 
					Start = record[TableIndexes.WorkStartTime], 
					End = record[TableIndexes.WorkEndTime]
				});
			// 時刻の記入不足がないか
			var incompleteWorkTimeRecords = workTimeRecords.Where(record => record.Start == null || record.End == null);
			if(incompleteWorkTimeRecords.Any())
			{
				foreach(var record in incompleteWorkTimeRecords)
				{
					messages.Add($"{((DateTime)(record.Date)).ToString("MM/dd")}の開始/終了時刻記録が不足しています");
				}
			}

			// 時刻が逆転していないか
			var invalidWorkTimeRecords = workTimeRecords
				.Where(record => record.Start != null && record.End != null && (TimeSpan)(record.Start) > (TimeSpan)(record.End));
			if(invalidWorkTimeRecords.Any())
			{
				foreach (var record in invalidWorkTimeRecords)
				{
					messages.Add($"{((DateTime)(record.Date)).ToString("MM/dd")}の開始/終了時刻記録が矛盾しています");
				}
			}

			// 各タスク時間記入の確認
			var incompleteTaskRecords = records.Where(record => record[TableIndexes.TaskType] != null || record[TableIndexes.TaskTime]!=null)
				.Select(record => new
				{
					Date = record[TableIndexes.Date],
					Type = record[TableIndexes.TaskType],
					Time = record[TableIndexes.TaskTime]
				})
				.Where(record => record.Time == null || record.Type == null);
			if(incompleteTaskRecords.Any())
			{
				foreach(var record in incompleteTaskRecords)
				{
					messages.Add($"{((DateTime)(record.Date)).ToString("MM/dd")}に業務タイプ/時刻未記入の記録があります");
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

			//var test = TaskElement.FromRecordRow(Rows[TableIndexes.RecordStartRow]);

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
				Type = "その他",
				Detail = "集計",
				Time = researchTaskTimeSum,
			};

			var elements = new List<TaskElement>();
			elements.AddRange(supportTasks.ToArray());
			elements.Add(supportTaskSumElement);
			elements.AddRange(researchTasks.ToArray());
			elements.Add(researchTaskSumElement);

			var dt = TableFrom(elements);
			return dt;
		}


	}
}
