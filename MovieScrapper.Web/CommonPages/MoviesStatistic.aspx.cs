using MovieScrapper.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class MoviesStatistic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                TemplateField tfield = new TemplateField();

                tfield.HeaderText = "Email";
                GridView1.Columns.Add(tfield);

                var service = new StatisticService();
                var titles = service.GetTitles();

                foreach (var title in titles)
                {
                    tfield = new TemplateField();
                    tfield.HeaderText = title;
                    GridView1.Columns.Add(tfield);
                }

            }
            this.BindGrid();

        }

        private void BindGrid()
        {
            var service = new StatisticService();
            var users = service.GetData();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Email", typeof(string)) });

            foreach (var user in users)
            {
                dt.Rows.Add(user);

            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                var service = new StatisticService();
                var dict = service.GetData();
                var titles = service.GetTitles();
                var arrayOfAllKeys = dict.Keys.ToArray();
                var listOfAllValues = dict.Values.ToList();
                var index = e.Row.RowIndex;
                e.Row.Cells[0].Text = arrayOfAllKeys[index];
                
                for (int i = 1; i <= titles.Count(); i++)
                {

                    //string headerText = ((DataTable)((GridView)sender).DataSource).Columns[i].ColumnName;
                    e.Row.Cells[i].Text = titles[i-1]+" "+ arrayOfAllKeys[index];

                }
            }


        }
    }
}