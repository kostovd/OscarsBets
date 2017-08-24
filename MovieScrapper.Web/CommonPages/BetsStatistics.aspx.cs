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
    public partial class BetsStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                TemplateField tfield = new TemplateField();

                tfield.HeaderText = "Email";
                GridView1.Columns.Add(tfield);
                
                var service = new BetsStatisticService();
                var categories = service.GetCategories();
                var winners = service.GetWinners();

                foreach (var category in categories)
                {
                    foreach (var winner in winners)
                    {
                        var currentWinnerCategory = winner[0];
                        var currentWinnerTitle = winner[1];
                        if (currentWinnerCategory == category)
                        {
                            tfield = new TemplateField();
                            tfield.HeaderText = category + "<br /><span style='color: rgb(237,192,116);'>" + currentWinnerTitle + "</span>";
                            GridView1.Columns.Add(tfield);
                        }
                    }
                   
                }

            }

            this.BindGrid();
        }

        private void BindGrid()
        {
            var service = new BetsStatisticService();
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

            if (e.Row.RowType == DataControlRowType.DataRow && !this.IsPostBack)
            {

                var service = new BetsStatisticService();
                var dict = service.GetData();
                var allCategories = service.GetCategories();
                var arrayOfAllKeys = dict.Keys.ToArray();
                var index = e.Row.RowIndex;
                var userName = arrayOfAllKeys[index];
                var userCategoriesMovies = dict[userName];
                var winners = service.GetWinners();

                e.Row.Cells[0].Text = userName;
                e.Row.Cells[0].Attributes["width"] = "150px";

               
                for (int i = 0; i < allCategories.Count(); i++)
                {
                    for (int j = 0; j < userCategoriesMovies.Count; j++)
                   {
                        var currentMovieCategory = userCategoriesMovies[j];
                        var currentCategory = currentMovieCategory[0];
                        var currentMovie = currentMovieCategory[1];
                        if (allCategories[i] == currentCategory)
                        {
                            
                            foreach (var winner in winners)
                            {
                                var currentWinnerCategory = winner[0];
                                var currentWinnerTitle = winner[1];
                                if (currentWinnerCategory == currentCategory)
                                {
                                    e.Row.Cells[i + 1].Text = currentMovie;
                                    e.Row.Cells[i + 1].Attributes["width"] = "150px";
                                    if (currentWinnerTitle == currentMovie)
                                    {
                                        e.Row.Cells[i + 1].Text = currentMovie + "<span style='font-family:Wingdings;color:rgb(237,192,116); font-size:30px;'>&#67;</span>";
                                    }
                                }

                            }


                        }
                        
                    }

                }

            }

        }
    }
}