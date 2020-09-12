using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkRepo
{
	class MyTable
	{
		private List<List<object>> _values;

		public object ValueAt(int row, int column) { return _values[row][column]; }
		public object SetValueAt(int row, int column, object value)
		{
			_values[row][column] = value;
			return value;
		}
		public IReadOnlyList<IReadOnlyList<object>> Rows { get { return _values; } }
		public IReadOnlyList<object> RowAt(int row) { return _values[row]; }

		protected MyTable() { }     // データなしでの生成は不可

		/// <summary>
		/// DataTableからデータを取り込む。DBNullはnullとして扱う
		/// </summary>
		/// <param name="dt">読み込むDataTable</param>
		public MyTable(DataTable dt)
		{
			_values = new List<List<object>>();

			foreach (DataRow row in dt.Rows)
			{
				var elements = new List<object>();
				for (var i = 0; i < dt.Columns.Count; i++)
				{
					if (row[i] is System.DBNull)
					{
						elements.Add(null);
					}
					else
					{
						elements.Add(row[i]);
					}
				}
				_values.Add(elements);
			}
		}

		/// <summary>
		/// 内部データの空白文字列(ホワイトスペース)を指定の値に置き換える
		/// </summary>
		/// <param name="to"></param>
		/// <returns></returns>
		public int ReplaceBlankCell(object to)
		{
			int count = 0;
			for(var i = 0; i<_values.Count; i++)
			{
				for(var j = 0;j<_values[0].Count;j++)
				{
					if (_values[i][j] is string && string.IsNullOrWhiteSpace((string)_values[i][j]))
					{
						_values[i][j] = to;
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
		/// <param name="count">取り出す行数</param>
		/// <returns></returns>
		public DataTable ToDataTable(int startRowIndex, int count)
		{
			if (_values == null) throw new ApplicationException("Data is not ready.");

			var columnCount = _values[0].Count;

			var dt = new DataTable();

			var scheme = Enumerable.Repeat<Type>(typeof(string), columnCount).ToArray();

			var targetRows = _values.Skip(startRowIndex).Take(count);

			// 列の型を決めるために全走査(最後に出てきたnull以外の型をその列の型とする)
			foreach(var row in targetRows)
			{
				for(var i=0;i<columnCount;i++)
				{
					if (row[i] != null && row[i].GetType() != typeof(string)) scheme[i] = row[i].GetType();
				}
			}
			for (var i = 0; i < columnCount; i++) dt.Columns.Add($"column{i}",scheme[i]);

			// 実データコピーのために再び全走査
			foreach(var row in targetRows)
			{
				var dr = dt.NewRow();
				for (var j = 0; j < columnCount; j++)
				{
					if (row[j] != null) dr[j] = (object)row[j];
				}
				dt.Rows.Add(dr);
			}
			return dt;
		}

		/// <summary>
		/// 列要素がすべてnullの行を取り除く
		/// </summary>
		public void RemoveEmptyRows()
		{
			for(var rowIdx = _values.Count-1;rowIdx>=0;rowIdx--)
			{
				if (_values[rowIdx].Any(obj => obj != null)) continue;
				_values.RemoveAt(rowIdx);
			}
		}


		/// <summary>
		/// 指定の行を取り除く
		/// </summary>
		/// <param name="index"></param>
		public void RemoveRow(int index)
		{
			_values.RemoveAt(index);
		}

		/// <summary>
		/// 指定範囲にある行を取り除く
		/// </summary>
		/// <param name="start"></param>
		/// <param name="count"></param>
		public void RemoveRows(int start, int count)
		{
			_values.RemoveRange(start, count);
		}

		/// <summary>
		/// 指定の列を取り除く
		/// </summary>
		/// <param name="index"></param>
		public void RemoveColumn(int index)
		{
			foreach(var row in _values)
			{
				row.RemoveAt(index);
			}
		}
	}

	class ItemParameter
	{
		public int Index { get; set; }
		public string Description { get; set; }
	}

	class RowParameter : ItemParameter { }
	class ColumnParameter : ItemParameter
	{
		/// <summary>
		/// データ型(デフォルトはstring)
		/// </summary>
		public Type Type { get; set; } = typeof(string);

		/// <summary>
		/// 区切りのための空項目かどうか
		/// </summary>
		public bool IsDelim { get; set; } = false;

		/// <summary>
		/// DateTime型のデータだが、記入時に日付省略されている可能性がある項目かどうか
		/// </summary>
		public bool MayBeIncompleteTime { get; set; } = false;
	}

	class WorkRecordTable : MyTable
	{
		private static readonly Dictionary<string, RowParameter> _rowParams = new Dictionary<string, RowParameter>
		{
			{"RecordStart", new RowParameter(){ Index = 1, Description = "記録開始行" } },
		};
		private static readonly Dictionary<string, ColumnParameter> _colParams = new Dictionary<string, ColumnParameter>
		{
			{"Date",        new ColumnParameter(){ Index = 0, Description = "日付",       Type =typeof(DateTime) } },
			{"WeekDay",     new ColumnParameter(){ Index = 1, Description = "曜日" } },
			{"PlanAM",      new ColumnParameter(){ Index = 2, Description = "午前計画" } },
			{"PlanPM",      new ColumnParameter(){ Index = 3, Description = "午後計画" } },
			{"ActAM",       new ColumnParameter(){ Index = 4, Description = "午前実行" } },
			{"ActPM",       new ColumnParameter(){ Index = 5, Description = "午後実行" } },
			{"Delim1",      new ColumnParameter(){ Index = 6, Description = "Delim1",   IsDelim = true } },
			{"TimeType",    new ColumnParameter(){ Index = 7, Description = "時刻種類" } },
			{"StartTime",   new ColumnParameter(){ Index = 8, Description = "開始時刻", Type = typeof(DateTime), MayBeIncompleteTime = true } },
			{"EndTime",		new ColumnParameter(){ Index = 9, Description = "終了時刻", Type = typeof(DateTime), MayBeIncompleteTime = true } },
			{"Delim2",		new ColumnParameter(){ Index = 10, Description = "Delim2",	IsDelim = true } },
			{"PlanTask",	new ColumnParameter(){ Index = 11, Description = "計画業務" } },
			{"ActTask",		new ColumnParameter(){ Index = 12, Description = "実行業務" } },
			{"TaskType",	new ColumnParameter(){ Index = 13, Description = "業務種類" } },
			{"TaskTime",	new ColumnParameter(){ Index = 14, Description = "業務時間",	Type = typeof(TimeSpan), MayBeIncompleteTime = true } },
		};

		public static IReadOnlyDictionary<string, ColumnParameter> ColumnParams { get { return _colParams; } }

		static readonly DateTime ExcelOriginDate = new DateTime(1899, 12, 31);

		private WorkRecordTable() { }

		private List<string> _messages = new List<string>();
		public IReadOnlyList<string> ErrorMessages { get { return _messages; } }

		public WorkRecordTable(DataTable dt) : base(dt)
		{
			ReplaceBlankCell(null);
			RemoveEmptyRows();
			FillBlankDate();
			FixTimeValues();
			CheckUpValues();
		}

		public DataTable ToDataTable()
		{
			var dt = base.ToDataTable(_rowParams["RecordStart"].Index, Rows.Count);
			foreach(var value in _colParams.Values)	// カラム名の設定
			{
				dt.Columns[value.Index].ColumnName = value.Description;
			}
			foreach(var item in _colParams.Values.Where(cp=>cp.IsDelim))	// 区切りカラムの除去
			{
				dt.Columns.Remove(item.Description);
			}
			return dt;
		}

		/// <summary>
		/// 日付が省略されている欄を日付で埋める
		/// </summary>
		private void FillBlankDate()
		{
			DateTime date = new DateTime();
			for(var i= _rowParams["RecordStart"].Index; i<base.Rows.Count; i++)
			{
				if (ValueAt(i, _colParams["Date"].Index) != null) date = (DateTime)ValueAt(i, _colParams["Date"].Index);
				else if (RowAt(i).Any(value => value != null)) SetValueAt(i, _colParams["Date"].Index, date);	// 1カ所でも記入されいてるセルがある場合は日付を入れる
			}
		}

		/// <summary>
		/// 日付が起源日に設定されているDateTime値、TimeSpanにすべきDateTime値を修正する
		/// </summary>
		private void FixTimeValues()
		{
			var cols = _colParams.Values.Where(item => item.MayBeIncompleteTime);//.Select(item => item.Index);
			foreach(var col in cols)
			{
				for (var rowIdx = _rowParams["RecordStart"].Index; rowIdx < base.Rows.Count; rowIdx++)
				{
					var cellValue = ValueAt(rowIdx, col.Index);
					if (cellValue == null) continue;
					if (cellValue is DateTime)
					{
						if (col.Type == typeof(DateTime))
						{
							var rowDate = (DateTime)ValueAt(rowIdx, _colParams["Date"].Index);
							SetValueAt(rowIdx, col.Index, new DateTime(rowDate.Year, rowDate.Month, rowDate.Day, ((DateTime)cellValue).Hour, ((DateTime)cellValue).Minute, 0));
						}
						else if (col.Type == typeof(TimeSpan))
						{
							SetValueAt(rowIdx, col.Index, new TimeSpan(((DateTime)cellValue).Hour, ((DateTime)cellValue).Minute, 0));
						}
						else
						{
							throw new ApplicationException();
						}
					}
					else
					{
						var rowDate = (DateTime)ValueAt(rowIdx, _colParams["Date"].Index);
						_messages.Add($"{rowDate.ToString("MM/dd")}の\"{cellValue.ToString()}\"を時刻として解釈できなかったので削除しました。");
						SetValueAt(rowIdx, col.Index, null);
					}
				}
			}

		}

		/// <summary>
		/// 表の内容を確認して、診断メッセージを作成する
		/// </summary>
		/// <returns></returns>
		public IReadOnlyList<string> CheckUpValues()
		{
			var messages = new List<string>();

			var rows = Rows.Skip(_rowParams["RecordStart"].Index).ToArray();

			// 【時刻タイプ、開始・終了時刻の検証】
			var timeRecords = rows.Select(record => new
			{
				Date = record[_colParams["Date"].Index],
				Type = record[_colParams["TimeType"].Index],
				Start = record[_colParams["StartTime"].Index],
				End = record[_colParams["EndTime"].Index],
			});
			// 中途半端な記入の抽出
			var incompleteTimeRecordGroups = timeRecords.Where(rec =>
				!(rec.Type == null && rec.Start == null && rec.End == null) &&
				!(rec.Type != null && rec.Start != null && rec.End != null))
				.GroupBy(rec => rec.Date);
			if(incompleteTimeRecordGroups.Any())
			{
				foreach(var group in incompleteTimeRecordGroups)
				{
					messages.Add($"{((DateTime)(group.Key)).ToString("MM/dd")}の時刻記録で記載が不足しています");
				}
			}
			// 時刻が逆転していないか
			var invalidTimeRecordGroups = timeRecords.Where(record => 
				record.Start != null && record.End != null && (DateTime)(record.Start) > (DateTime)(record.End))
				.GroupBy(rec => rec.Date);
			if (invalidTimeRecordGroups.Any())
			{
				foreach (var gropu in invalidTimeRecordGroups)
				{
					messages.Add($"{((DateTime)(gropu.Key)).ToString("MM/dd")}の時刻記録に矛盾があります(開始>終了)");
				}
			}

			// 【各タスク時間記入の確認】
			var incompleteTaskRecordGroups = rows.Where(record =>
				(record[_colParams["TaskType"].Index] != null && record[_colParams["TaskTime"].Index] == null) ||
				(record[_colParams["TaskType"].Index] == null && record[_colParams["TaskTime"].Index] != null))
				.Select(record => new
				{
					Date = record[_colParams["Date"].Index],
					Type = record[_colParams["TaskType"].Index],
					Time = record[_colParams["TaskTime"].Index]
				}).GroupBy(rec => rec.Date);

			if (incompleteTaskRecordGroups.Any())
			{
				foreach(var group in incompleteTaskRecordGroups)
				{
					messages.Add($"{((DateTime)(group.Key)).ToString("MM/dd")}の業務タイプ記録で記載が不足しています");
				}
			}
			return messages;
		}

		private class TaskInfo
		{
			/// <summary>
			/// タスクを実施した日付
			/// </summary>
			public DateTime Date { get; set; }
			/// <summary>
			/// タスクの種類
			/// </summary>
			public String Type { get; set; }
			/// <summary>
			/// タスクの詳細
			/// </summary>
			public String Detail { get; set; }
			/// <summary>
			/// タスクにかけた時間
			/// </summary>
			public TimeSpan Time { get; set; }

			static public TaskInfo FromRecordRow(IReadOnlyList<object> row)
			{
				return new TaskInfo()
				{
					Date = (row[_colParams["Date"].Index] != null) ? (DateTime)row[_colParams["Date"].Index] : new DateTime(0),
					Type = (row[_colParams["TaskType"].Index] != null) ? (string)row[_colParams["TaskType"].Index] : "",
					Detail = (row[_colParams["ActTask"].Index] != null) ? (string)row[_colParams["ActTask"].Index] : "",
					Time = (row[_colParams["TaskTime"].Index] != null) ? (TimeSpan)row[_colParams["TaskTime"].Index] : new TimeSpan(0),
				};
			}
		}

		private DataTable TableFrom(IReadOnlyList<TaskInfo> taskElements)
		{
			var dt = new DataTable();

			var propertyInfos = typeof(TaskInfo).GetProperties();

			foreach (var pi in propertyInfos)
			{
				if(pi.PropertyType == typeof(TimeSpan))
					dt.Columns.Add(pi.Name, typeof(string));	// TimeSpanはFormatが効かないので型を維持するメリットが少ない。表示を自分で変更するためstringにしておく
				else
					dt.Columns.Add(pi.Name, pi.PropertyType);
			}
			foreach (var element in taskElements)
			{
				var dr = dt.NewRow();
				foreach (var pi in propertyInfos)
				{
					if (pi.PropertyType == typeof(TimeSpan))
					{
						var ts = (TimeSpan)pi.GetValue(element);
						var hour = ts.Days * 24 + ts.Hours;
						dr[pi.Name] = $"{hour}:{ts.Minutes:00}";
					}
					else
					{
						dr[pi.Name] = pi.GetValue(element);
					}
				}
				dt.Rows.Add(dr);
			}
			return dt;
		}

		public Dictionary<string, TimeSpan> Aggregate()
		{
			var recordsGroupedByTaskType = base.Rows.Skip(_rowParams["RecordStart"].Index)
				.Select(row => TaskInfo.FromRecordRow(row))
				.GroupBy(task => task.Type);

			var aggregates = new Dictionary<string, TimeSpan>();
			foreach (var group in recordsGroupedByTaskType)
			{
				var time = new TimeSpan(group.Sum(task => task.Time.Ticks));
				aggregates.Add(group.Key, time);
			}
			return aggregates;
		}

		public DataTable GetStatistics()
		{
			var elements = new List<TaskInfo>();
			var allRecords = base.Rows.Skip(_rowParams["RecordStart"].Index).Select(row => TaskInfo.FromRecordRow(row));

			var grouped = allRecords.GroupBy(task => task.Type);

			var sums = new List<TaskInfo>();

			foreach(var group in grouped)
			{
				var sum = new TaskInfo()
				{
					//Date = (lastDate != null) ? (DateTime)lastDate : new DateTime(0),
					Type = group.Key,
					Detail = "集計",
					Time = new TimeSpan(group.Sum(task=>task.Time.Ticks)),
				};
				sums.Add(sum);
			}

			var dt = TableFrom(sums);
			return dt;

		}
	}
}
