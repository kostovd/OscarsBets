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

                //tfield.HeaderText = "Email";
                GridView1.Columns.Add(tfield);

                //tfield.HeaderText = "Sum";
                GridView1.Columns.Add(tfield);

                var service = new WatcheMoviesStatisticService();
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
            var service = new WatcheMoviesStatisticService();
            var users = service.GetData();
            DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Email", typeof(string)) });

            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("User", typeof(string)), new DataColumn("Sum", typeof(string)),});
            foreach (var user in users)
            {
                dt.Rows.Add(user);

            }           

            GridView1.DataSource = dt;
            GridView1.DataBind();
            
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow && !this.IsPostBack)
            {
                
                var service = new WatcheMoviesStatisticService();
                var dict = service.GetData();
                var allTitles = service.GetTitles();
                var arrayOfAllKeys = dict.Keys.ToArray();               
                var index = e.Row.RowIndex;
                var userName = arrayOfAllKeys[index];
                var userTitles = dict[userName];

                e.Row.Cells[0].Text = userName;
                e.Row.Cells[0].Attributes["width"] = "150px";

                e.Row.Cells[1].Text = userTitles.Count.ToString();
                e.Row.Cells[1].Attributes["width"] = "30px";

                for (int i = 0; i <allTitles.Count(); i++)
                {
                    for(int j = 0; j < userTitles.Count; j++)
                    {
                        if (allTitles[i] == userTitles[j])
                        {
                            e.Row.Cells[i+2].Text = "X";
                            e.Row.Cells[i+2].Attributes["width"] = "100px";
                        }
                    }
                    
                }
                
            }

        }
       

    }
}