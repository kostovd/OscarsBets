using MovieScrapper.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using MovieScrapper.Business.Interfaces;

namespace MovieScrapper.CommonPages
{
    public partial class BetsStatistics : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                //var betStatisticServices = GetBuisnessService<IBetStatisticService>();
                //var winners = betStatisticServices.GetWinners();
                //var categories = betStatisticServices.GetCategories();
                //bool winnerListIsEmpty = !winners.Any();
                //bool allWinnersAreSet;
                //if (winners.Count == categories.Count())
                //{
                //    allWinnersAreSet = true;
                //}
                //else
                //{
                //    allWinnersAreSet = false;
                //}

                //// Email column
                //TemplateField tfield = new TemplateField();
                //tfield.HeaderText = "User";
                //GridView1.Columns.Add(tfield);

                //// Sum column 
                //if (!GameIsRunning() && allWinnersAreSet )
                //{
                //    tfield = new TemplateField();
                //    tfield.HeaderText = "Scores";
                //    GridView1.Columns.Add(tfield);
                //}

                ////Categories columns
                //foreach (var category in categories)
                //{
                //    if (GameIsRunning())
                //    {
                //        tfield = new TemplateField();
                //        tfield.HeaderText = "<span class='goldFont'>" + category + "</span>";
                //        GridView1.Columns.Add(tfield);
                //    }
                //    else //Game is stopped
                //    {
                //        if (allWinnersAreSet)
                //        {
                //            foreach (var winner in winners)
                //            {
                //                var currentWinnerCategory = winner[0];
                //                var currentWinnerTitle = winner[1];
                //                if (currentWinnerCategory == category)
                //                {
                //                    tfield = new TemplateField();
                //                    tfield.HeaderText = "<span class='goldFont'>" + category + "</span>" + "<br /><span class='redFont'>" + currentWinnerTitle + "</span>";
                //                    GridView1.Columns.Add(tfield);
                //                }
                //            }
                //            var theWinner = betStatisticServices.GetWinner();
                //            Label1.Text = theWinner;

                //        }
                //        else
                //        {
                //            tfield = new TemplateField();
                //            tfield.HeaderText = "<span class='goldFont'>" + category + "</span>" + "<br /><span style='font-size:10px;'> Waiting to know the winner </span>";
                //            GridView1.Columns.Add(tfield);

                //            Label1.Text = "Winners not set yet";
                //            Label1.CssClass = "redBorder";
                //        }
                //    }

                //}              

                BindGrid();
            }

            //this.BindGrid();
       
        }

        private void BindGrid1()
        {
            var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            var winners = betStatisticServices.GetWinners();
            var categories = betStatisticServices.GetCategories();
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
            tfield.HeaderText = "User";
            GridView1.Columns.Add(tfield);

            // Sum column 
            if (!GameIsRunning() && allWinnersAreSet)
            {
                tfield = new TemplateField();
                tfield.HeaderText = "Scores";
                GridView1.Columns.Add(tfield);
            }

            //Categories columns
            foreach (var category in categories)
            {
                if (GameIsRunning())
                {
                    tfield = new TemplateField();
                    tfield.HeaderText = "<span class='goldFont'>" + category + "</span>";
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
                                tfield.HeaderText = "<span class='goldFont'>" + category + "</span>" + "<br /><span class='redFont'>" + currentWinnerTitle + "</span>";
                                GridView1.Columns.Add(tfield);
                            }
                        }
                        var theWinner = betStatisticServices.GetWinner();
                        Label1.Text = theWinner;

                    }
                    else
                    {
                        tfield = new TemplateField();
                        tfield.HeaderText = "<span class='goldFont'>" + category + "</span>" + "<br /><span style='font-size:10px;'> Waiting to know the winner </span>";
                        GridView1.Columns.Add(tfield);

                        Label1.Text = "Winners not set yet";
                        Label1.CssClass = "redBorder";
                    }
                }
            }

            var usersBets = betStatisticServices.GetData();
        }

        private void BindGrid()
        {
            var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            
            var categories = betStatisticServices.GetCategories();

            DataTable dt = new DataTable();
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Scores", typeof(int));
            foreach (string category in categories)
            {
                dt.Columns.Add(category);
            }

            var users = betStatisticServices.GetData();
            foreach (var user in users)
            {
                var row = dt.NewRow();
                row["Email"] = user.UserEmail;

                int scores = 0;
                foreach (var bet in user.UserBets)
                {
                    row[bet.CategoryTitle] = bet;
                    if (bet.IsRightGuess)
                    {
                        scores++;
                    }
                }

                row["Scores"] = scores;

                dt.Rows.Add(row);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow && !this.IsPostBack)
            //{

            //    var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            //    var dict = betStatisticServices.GetData();
            //    var allCategories = betStatisticServices.GetCategories();
            //    var arrayOfAllKeys = dict.Keys.ToArray();
            //    var index = e.Row.RowIndex;
            //    var userName = arrayOfAllKeys[index];
            //    var userCategoriesMovies = dict[userName];
            //    var winners = betStatisticServices.GetWinners();
            //    bool winnerListIsEmpty = !winners.Any();
            //    bool allWinnersAreSet;
            //    if (winners.Count == allCategories.Count())
            //    {
            //        allWinnersAreSet = true;
            //    }
            //    else
            //    {
            //        allWinnersAreSet = false;
            //    }

            //    // Email column
            //    e.Row.Cells[0].Text = userName;
            //    e.Row.Cells[0].Attributes["width"] = "150px";

            //    // Gesses sum column
            //    if (allWinnersAreSet && !GameIsRunning())
            //    {
            //        var counter = 0;
                 
            //        foreach(var userCategoryMovie in userCategoriesMovies)
            //        {
            //            if (userCategoryMovie[2] == 1.ToString())
            //            {
            //                counter++;
            //            }
            //        }
            //        e.Row.Cells[1].Text = counter.ToString();
            //        e.Row.Cells[1].Attributes["width"] = "50px";
            //    }

            //    for (int i = 0; i < allCategories.Count(); i++)
            //    {
            //        for (int j = 0; j < userCategoriesMovies.Count; j++)
            //       {
            //            var currentMovieCategory = userCategoriesMovies[j];
            //            var currentCategory = currentMovieCategory[0];
            //            var currentMovie = currentMovieCategory[1];
            //            if (allCategories[i] == currentCategory)
            //            {
            //                if (GameIsRunning() == true)
            //                {
            //                    e.Row.Cells[i + 1].Text = currentMovie;
            //                    e.Row.Cells[i + 1].Attributes["width"] = "150px";
            //                }
            //                else
            //                {
            //                    if (!allWinnersAreSet || winnerListIsEmpty)
            //                    {
            //                        e.Row.Cells[i + 1].Text = currentMovie;
            //                        e.Row.Cells[i + 1].Attributes["width"] = "150px";
            //                    }
            //                    else if (allWinnersAreSet)
            //                    {
                                    
            //                        foreach (var winner in winners)
            //                        {
            //                            var currentWinnerCategory = winner[0];
            //                            var currentWinnerTitle = winner[1];
            //                            if (currentWinnerCategory == currentCategory)
            //                            {
            //                                e.Row.Cells[i + 2].Text = currentMovie;
            //                                e.Row.Cells[i + 2].Attributes["width"] = "150px";
            //                                if (currentWinnerTitle == currentMovie)
            //                                {
            //                                    e.Row.Cells[i + 2].Text = currentMovie + "<span style='font-family:Wingdings;color:rgb(237,192,116); font-size:30px;'>&#67;</span>";
                                               
            //                                }
            //                            }

            //                        }
                                    
            //                    }

            //                }
            //            }
                        
            //        }

            //    }

            //}

        }

        private bool GameIsRunning()
        {
            var gamePropertyService = GetBuisnessService<IGamePropertyService>();

            if (gamePropertyService.IsGameStopped() == false)
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