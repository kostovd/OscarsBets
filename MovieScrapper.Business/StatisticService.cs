using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class StatisticService
    {
        public DataTable GetData()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetData();
            int count = recievedTable.Rows.Count;
            //DataTable dt = new DataTable();
            //for(int i=0; i<=count; i++)
            //{
            //    dt.Columns.Add("Id", typeof(System.String));
            //    for (int j = 0; j <= count; i++)
            //    {
            //        var row = new DataRow[i];
            //        dt.Rows.Add(row);
            //    }
            //}

            ////foreach (DataRow row in dt.Rows)
            ////{
            ////    //need to set value to NewColumn column
            ////    row["Id"] = 0;   // or set it to some other value
            ////}

            //return dt;

            DataTable table = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;
            //DataView view;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Title";
            table.Columns.Add(column);

            // Create new DataRow objects and add to DataTable.    
            for (int i = 0; i < count; i++)
            {
                row = table.NewRow();
                row["Id"] = i;
                row["Title"] = recievedTable.Rows[i].ToString();
                table.Rows.Add(row);
            }

            // Create a DataView using the DataTable.
            //view = new DataView(table);

            //// Set a DataGrid control's DataSource to the DataView.
            //dataGrid1.DataSource = view;
            return table;
        }
    }
}

