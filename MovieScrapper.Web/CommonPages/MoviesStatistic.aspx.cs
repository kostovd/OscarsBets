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
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Email", typeof(string))});

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
                //TextBox txtUser = new TextBox();
                //txtUser.ID = "txtUser";
                //txtUser.Text = (e.Row.DataItem as DataRowView).Row["Email"].ToString();
                //e.Row.Cells[0].Controls.Add(txtUser);
                var service = new StatisticService();
                var dict = service.GetData();
                var arrayOfAllKeys = dict.Keys.ToArray();

                e.Row.Cells[0].Text = arrayOfAllKeys.ToString();


            }
        }


    }
}