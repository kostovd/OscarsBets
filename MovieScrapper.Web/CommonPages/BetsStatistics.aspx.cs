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
                var service = new BetsStatisticService();
                var winners = service.GetWinners();
                var categories = service.GetCategories();
                bool winnerListIsEmpty = !winners.Any();
                bool allWinnersAreSet;
                if (winners.Count == categories.Count())
                {
                    allWinnersAreSet = true;
                }
                else
                {
                    allWinnersAreSet = false;
                }

                // Email column
                TemplateField tfield = new TemplateField();              
                GridView1.Columns.Add(tfield);

                // Sum column 
                if (!GameIsRunning() && allWinnersAreSet )
                {                   
                    GridView1.Columns.Add(tfield);
                }
                                           
                //Categories columns
                foreach (var category in categories)
                {
                    if (GameIsRunning())
                    {
                        tfield = new TemplateField();
                        tfield.HeaderText = "<span style='color: rgb(237,192,116);'>" + category + "</span>";
                        GridView1.Columns.Add(tfield);
                    }
                    else //Game is stopped
                    {
                        if (allWinnersAreSet)
                        {
                            foreach (var winner in winners)
                            {
                                var currentWinnerCategory = winner[0];
                                var currentWinnerTitle = winner[1];
                                if (currentWinnerCategory == category)
                                {
                                    tfield = new TemplateField();
                                    tfield.HeaderText = "<span style='color: rgb(237,192,116);'>" + category + "</span>" + "<br /><span style='color: rgb(179,0,0);'>" + currentWinnerTitle + "</span>";
                                    GridView1.Columns.Add(tfield);
                                }
                            }
                            var theWinner = service.GetWinner();
                            Label1.Text = theWinner;
                            Label1.CssClass = "goldBorder";
                        }
                        else
                        {
                            tfield = new TemplateField();
                            tfield.HeaderText = "<span style='color: rgb(237,192,116);'>" + category + "</span>" + "<br /><span style='font-size:10px;'> Waiting to know the winner </span>";
                            GridView1.Columns.Add(tfield);

                            Label1.Text = "Winners not set yet";
                            Label1.CssClass = "goldBorder";
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

                var statisticsService = new BetsStatisticService();
                var dict = statisticsService.GetData();
                var allCategories = statisticsService.GetCategories();
                var arrayOfAllKeys = dict.Keys.ToArray();
                var index = e.Row.RowIndex;
                var userName = arrayOfAllKeys[index];
                var userCategoriesMovies = dict[userName];
                var winners = statisticsService.GetWinners();
                bool winnerListIsEmpty = !winners.Any();
                bool allWinnersAreSet;
                if (winners.Count == allCategories.Count())
                {
                    allWinnersAreSet = true;
                }
                else
                {
                    allWinnersAreSet = false;
                }

                // Email column
                e.Row.Cells[0].Text = userName;
                e.Row.Cells[0].Attributes["width"] = "150px";

                // Gesses sum column
                if (allWinnersAreSet)
                {
                    var counter = 0;
                    //for (int i = 0; i < allCategories.Count(); i++)
                    //{
                    //    for (int j = 0; j < userCategoriesMovies.Count; j++)
                    //    {
                    //        var currentMovieCategory = userCategoriesMovies[j];
                    //        var currentCategory = currentMovieCategory[0];
                    //        var currentMovie = currentMovieCategory[1];

                    //        foreach (var winner in winners)
                    //        {
                    //            var currentWinnerCategory = winner[0];
                    //            var currentWinnerTitle = winner[1];
                    //            if (currentWinnerCategory == currentCategory)
                    //            {                                    
                    //                if (currentWinnerTitle == currentMovie)
                    //                {
                    //                    counter++;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    foreach(var userCategoryMovie in userCategoriesMovies)
                    {
                        if (userCategoryMovie[2] == 1.ToString())
                        {
                            counter++;
                        }
                    }
                    e.Row.Cells[1].Text = counter.ToString();
                    e.Row.Cells[1].Attributes["width"] = "50px";
                }

                for (int i = 0; i < allCategories.Count(); i++)
                {
                    for (int j = 0; j < userCategoriesMovies.Count; j++)
                   {
                        var currentMovieCategory = userCategoriesMovies[j];
                        var currentCategory = currentMovieCategory[0];
                        var currentMovie = currentMovieCategory[1];
                        if (allCategories[i] == currentCategory)
                        {
                            if (GameIsRunning() == true)
                            {
                                e.Row.Cells[i + 1].Text = currentMovie;
                                e.Row.Cells[i + 1].Attributes["width"] = "150px";
                            }
                            else
                            {
                                if (!allWinnersAreSet || winnerListIsEmpty)
                                {
                                    e.Row.Cells[i + 1].Text = currentMovie;
                                    e.Row.Cells[i + 1].Attributes["width"] = "150px";
                                }
                                else if (allWinnersAreSet)
                                {
                                    
                                    foreach (var winner in winners)
                                    {
                                        var currentWinnerCategory = winner[0];
                                        var currentWinnerTitle = winner[1];
                                        if (currentWinnerCategory == currentCategory)
                                        {
                                            e.Row.Cells[i + 2].Text = currentMovie;
                                            e.Row.Cells[i + 2].Attributes["width"] = "150px";
                                            if (currentWinnerTitle == currentMovie)
                                            {
                                                e.Row.Cells[i + 2].Text = currentMovie + "<span style='font-family:Wingdings;color:rgb(237,192,116); font-size:30px;'>&#67;</span>";
                                               
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

        private bool GameIsRunning()
        {
            var service = new CategoryService();
            if (service.IsGameStopped() == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}