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
using Newtonsoft.Json;
using System.Diagnostics;
using ExcelDataReader;
using WorkRepo;

namespace MakeSchedule
{
	public partial class schedule_MainForm : Form
	{
		static readonly string _jsonFileName = "settings.json";
		private Settings _settings;

		string ArrayToString(IReadOnlyList<object> array, string delim)
		{
			var sb = new StringBuilder();
			foreach(var obj in array)
			{
				sb.Append(obj.ToString());
				sb.Append(delim);
			}
			sb.Length = sb.Length - delim.Length;
			return sb.ToString();
		}

		T[] StringToArray<T>(string str, string delim, Func<string,T> parser)
		{
			var objs = str.Split(new string[] { delim }, StringSplitOptions.RemoveEmptyEntries);
			var outList = new List<T>();
			foreach(var obj in objs)
			{
				outList.Add(parser(obj.Trim()));
			}
			return outList.ToArray();
		}

		public schedule_MainForm()
		{
			InitializeComponent();
			
			if(File.Exists(_jsonFileName))
			{
				using (var sr = new StreamReader(_jsonFileName))
				{
					var json = sr.ReadToEnd();
					_settings = JsonConvert.DeserializeObject<Settings>(json);
					textBoxFolderPath.Text = _settings.FolderPath;
					textBoxFileNamePrefix.Text = _settings.FilenamePrefix;
					textBoxPersonsNames.Text = ArrayToString(_settings.Names, ", ");
				}
			}

		}

		class ScheduleElement
		{
			public DateTime Date { get; set; }
			public string PlanAM { get; set; }
			public string PlanPM { get; set; }
			public string ActAM { get; set; }
			public string ActPM { get; set; }
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var targetDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
			var startDate = targetDate.AddDays(-(int)targetDate.DayOfWeek+1);	// 月曜日から
			var endDate = targetDate.AddDays(5-(int)targetDate.DayOfWeek);		// 金曜日まで
			Trace.WriteLine($"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}");

			var files = Directory.GetFiles(_settings.FolderPath);

			var allSchedules = new Dictionary<string, ScheduleElement[]>();
			foreach (var personName in _settings.Names)
			{
				var filename = _settings.FilenamePrefix + personName + ".xlsx";
				var fullPath = _settings.FolderPath + "//" + filename;
				if (!File.Exists(fullPath))
				{
					listBoxLogMessage.Items.Add($"\"{filename}\" は見つかりませんでした。");
					continue;
				}
				using (var fs = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var excelReader = ExcelReaderFactory.CreateReader(fs))
				{
					var excelBook = excelReader.AsDataSet();
					var sheetNames = new List<string>();
					foreach(DataTable dt in excelBook.Tables)
					{
						sheetNames.Add(dt.TableName);
					}
					string firstSheetName = null;
					string secondSheetName = null;
					firstSheetName = sheetNames.Where(name => name.StartsWith($"{(startDate.Month):00}")).FirstOrDefault();
					if(firstSheetName == null)
					{
						listBoxLogMessage.Items.Add($"{personName}さんの{startDate.Month}月シートが見つかりませんでした。シート名が\"MMdd-MMdd\"形式になっているか確認ください.");
						continue;
					}
					if (startDate.Month != endDate.Month)
						secondSheetName = sheetNames.Where(name => name.StartsWith($"{(endDate.Month):00}")).FirstOrDefault();
					var schedules = ScanForSchedule(excelBook.Tables[firstSheetName]);
					var filtered = schedules.Where(item => item.Date >= startDate && item.Date <= endDate).ToList();
					if(secondSheetName!=null)
					{
						schedules = ScanForSchedule(excelBook.Tables[firstSheetName]);
						filtered.AddRange(schedules.Where(item => item.Date >= startDate && item.Date <= endDate).ToArray());
					}
					allSchedules[personName] = filtered.ToArray();
				}
			}
			var outputDt = new DataTable();
			outputDt.Columns.Add("日付", typeof(DateTime));
			foreach (var name in _settings.Names) outputDt.Columns.Add(name);
			for(var date = startDate; date<=endDate;date=date.AddDays(1))
			{
				var row = outputDt.NewRow();
				row["日付"] = date;
				foreach(var personsName in _settings.Names)
				{
					if(allSchedules.ContainsKey(personsName))
					{
						var sche = allSchedules[personsName].Where(item => item.Date == date).FirstOrDefault();//.Select(item=>item.PlanAM + item.PlanPM)
						row[personsName] = sche?.PlanAM;
						if (sche?.PlanAM != sche?.PlanPM) row[personsName] = sche.PlanAM +"/"+sche.PlanPM;

					}
				}
				outputDt.Rows.Add(row);
			}
			dataGridViewScheduleTable.DataSource = outputDt;
			dataGridViewScheduleTable.Columns["日付"].DefaultCellStyle.Format = "MM/dd";
			dataGridViewScheduleTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
//			dataGridViewScheduleTable.Columns["日付"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		}

		ScheduleElement[] ScanForSchedule(DataTable srcDt)
		{
			var table = new WorkRecordTable(srcDt);

			var scheduleRows = table.Rows.Where(row =>
				
				row[WorkRecordTable.ColumnParams["Date"].Index]?.GetType() == typeof(DateTime) &&
				row[WorkRecordTable.ColumnParams["PlanAM"].Index] != null &&
				row[WorkRecordTable.ColumnParams["PlanPM"].Index] != null );

			var list = new List<ScheduleElement>();

			foreach(var row in scheduleRows)
			{
				var item = new ScheduleElement
				{
					Date = (DateTime)row[WorkRecordTable.ColumnParams["Date"].Index],
					PlanAM = (string)row[WorkRecordTable.ColumnParams["PlanAM"].Index],
					PlanPM = (string)row[WorkRecordTable.ColumnParams["PlanAM"].Index],
				};
				list.Add(item);
			}
			return list.ToArray();
		}


		private void buttonBrowseFolder_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog() { FileName = "SelectFolder", Filter = "Folder|.", CheckFileExists = false })
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					textBoxFolderPath.Text = Path.GetDirectoryName(ofd.FileName);
				}
			}
		}

		static readonly char[] _invalidChars = { '\\', '/', ':', '*', '?', '\"', '<', '>', '|', };

		private void textBoxPersonsNames_Validating(object sender, CancelEventArgs e)
		{
			foreach (var ch in _invalidChars)
			{
				if (textBoxPersonsNames.Text.Contains(ch))
				{
					MessageBox.Show("人名欄に使用できない文字(\\ / : * ? \" < > |)が含まれています。", "エラー");
					e.Cancel = true;
					continue;
				}
			}
		}

		private void textBoxPersonsNames_Validated(object sender, EventArgs e)
		{
			_settings.Names = StringToArray<string>(textBoxPersonsNames.Text, ",", str => str.ToString());
		}

		private void textBoxFolderPath_TextChanged(object sender, EventArgs e)
		{
			_settings.FolderPath = textBoxFolderPath.Text;
			// readonlyで値はフォルダダイアログから拾ってるので検証不要で使える。
		}

		private void textBoxFileNamePrefix_Validating(object sender, CancelEventArgs e)
		{
			foreach (var ch in _invalidChars)
			{
				if (textBoxFileNamePrefix.Text.Contains(ch))
				{
					MessageBox.Show("ファイル名に使用できない文字(\\ / : * ? \" < > |)が含まれています。", "エラー");
					e.Cancel = true;
					continue;
				}
			}

		}

		private void textBoxFileNamePrefix_Validated(object sender, EventArgs e)
		{
			_settings.FilenamePrefix = textBoxFileNamePrefix.Text;
		}

		private void schedule_MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			using (var wr = new StreamWriter(_jsonFileName))
			{
				var json = JsonConvert.SerializeObject(_settings);
				wr.Write(json);
			}

		}
	}
}
