using Microsoft.AspNet.Identity;
using MovieScrapper.Business;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class ShowCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = new CategoryService();

            if (!User.Identity.IsAuthenticated)
            {
                GreatingLabel.Text = "You must be logged in to bet!";
            }
            else
            {
                GreatingLabel.CssClass = "hidden";
            }

            if (service.IsGameNotStartedYet())
            {
                WarningLabel.CssClass = WarningLabel.CssClass.Replace("warning", "");
                GreatingLabel.CssClass = "hidden";
                WarningLabel.CssClass = "hidden";
            }
        }

        public bool IsGameRunning()
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

        public bool IsGameNotStartedYet()
        {
            var service = new CategoryService();
            if (service.IsGameNotStartedYet()==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string BuildPosterUrl(string path)
        {
            return "http://image.tmdb.org/t/p/w92" + path;
        }

        public string DisplayYear(string dateString)
        {
            DateTime res;

            if (DateTime.TryParse(dateString, out res))
            {
                return res.Year.ToString();
            }
            else
            {
                return dateString;
            }

        }

        protected string BuildImdbUrl(string movieId)
        {

            return "http://www.imdb.com/title/" + movieId;
        }

        protected string BuildUrl(int movieId)
        {

            return "/CommonPages/DBMovieDetails.aspx?id=" + movieId + "&back=/CommonPages/ShowCategories";
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsBetted")
            {
                var userId = User.Identity.GetUserId();
                string sortByAndArrangeBy = (e.CommandArgument).ToString();
                char[] separator = { '|' };
                string[] sortByAndArrangeByArray = sortByAndArrangeBy.Split(separator);
                var movieId = int.Parse(sortByAndArrangeByArray[0]);
                var categoryId = int.Parse(sortByAndArrangeByArray[1]);
                var service = new CategoryService();
                var betEntity = service.MakeBetEntity(userId, movieId, categoryId);
                Repeater1.DataBind();

            }
        }

        protected bool CheckIfTheUserIsLogged()
        {
            if (User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string ChangeTextIfUserBettedOnThisMovie(ICollection<Bet> categoryBets, int movieId)
        {
            string currentUserId = User.Identity.GetUserId();
            if (categoryBets.Any(x => x.UserId == currentUserId && x.Movie.Id == movieId))
            {
                return "þ"; //code 254 in ASCI
            }
            else
            {
                return "o"; //code 111 in ASCI
            }
        }

        protected string CheckIfWinner(object winner, int currentMovieId)
        {
            if (IsGameRunning() == true)
            {
                return "";
            }
            else
            {
                if (winner == null)
                {
                    return "";
                }
                else
                {
                    if (winner.ToString() == currentMovieId.ToString())
                    {
                        return "winner";
                    }
                    else
                    {
                        return "notWinner";
                    }
                }
            }

        }

        protected string CheckIfWinnerImage(object winner, int currentMovieId)
        {
            if (IsGameRunning() == true)
            {
                return "";
            }
            else
            {
                if (winner == null)
                {
                    return "";
                }
                else
                {
                    if (winner.ToString() == currentMovieId.ToString())
                    {
                        return "/Oscar_logo.png";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var currentUsereId = User.Identity.GetUserId();

            IEnumerable<Category> categories = (IEnumerable<Category>)e.ReturnValue;
            var categoryCount = categories.Count();
            var bettedCategories = categories.Sum(x => x.Bets.Count(b => b.UserId == currentUsereId));
            var missedCategories = categoryCount - bettedCategories;
            var winners = categories.Select(c => c.Winner).ToList();
            bool winnersAreSet = !winners.Any(x => x == null);
            var counter = 0;

            foreach (var category in categories)
            {
                if (category.Winner != null)
                {
                    var winner = category.Winner.Id;
                    Bet bet = category.Bets.Where(x => x.UserId == currentUsereId).SingleOrDefault();
                    if (bet != null)
                    {
                        if (winner != 0)
                        {
                            if (bet.Movie.Id == winner)
                            {
                                counter++;
                            }
                        }
                    }
                }
            }

            if (CheckIfTheUserIsLogged() == true && IsGameRunning() == true)
            {
                if (missedCategories > 0)
                {
                    if (missedCategories == 1)
                    {
                        WarningLabel.Text = "Here you can bet in " + categoryCount + " different categories. " +
                            "You have " + (missedCategories) + " more category to bet.";
                    }
                    else
                    {
                        WarningLabel.Text = "Here you can bet in " + categoryCount + " different categories. " +
                            "You have " + (missedCategories) + " more categories to bet.";
                    }
                }
                else
                {
                    WarningLabel.CssClass = "goldBorder";
                    WarningLabel.Text = "Congretilations! You betted in all the " + categoryCount + " categories.";
                }

            }
            else
            {
                WarningLabel.CssClass = "hidden";
            }

            //////////////// Show right gestures statistic label /////////////////////

            if (CheckIfTheUserIsLogged() == true && IsGameRunning() == false)
            {
                if (winnersAreSet)
                {
                    if (counter > 0)
                    {
                        if (counter == categoryCount)
                        {
                            WinnerLabel.Text = "Yayyyyyyyyy! You guessed right in all the categories!";
                            WinnerLabel.CssClass = "goldBorder";
                        }
                        else if (counter == 1)
                        {
                            WinnerLabel.Text = "Congretulations! You guessed right in " + counter + " category.";
                        }
                        else
                        {
                            WinnerLabel.Text = "Congretulations! You guessed right in " + counter + " categories.";
                        }
                    }
                    else
                    {
                        WinnerLabel.Text = "Sorry, you don't have right gestures";
                    }
                }
                else
                {
                    WinnerLabel.Text = "The game is stopped, but we are waiting to know the winners.";
                }

            }

            else
            {
                WinnerLabel.CssClass = "hidden";
            }
        }
    }
}