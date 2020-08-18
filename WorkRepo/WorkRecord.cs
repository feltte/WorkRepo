using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkRepo
{
	class WorkRecord
	{
		private readonly int RecordStartRowIndex = 4;

		private object[][] values;

		private WorkRecord() { }

		public WorkRecord(DataTable dt)
		{
			var rows = new List<object[]>();
			foreach (DataRow row in dt.Rows)
			{
				var elemtns = new List<object>();
				for (var i = 0; i < dt.Columns.Count; i++)
				{
					elemtns.Add(row[i]);
				}
				rows.Add(elemtns.Select(obj => obj is System.DBNull ? String.Empty : obj).ToArray());
			}
			this.values = rows.ToArray();
		}

		public DataTable AsTable(int startRowIndex)
		{
			var dt = new DataTable();
			for(var i=startRowIndex; i<values.Length;i++)
			{
				dt.
			}
		}
	}
}
