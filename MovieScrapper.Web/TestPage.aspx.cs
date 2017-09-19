using Microsoft.AspNet.Identity;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper
{
    public partial class TestPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var gamePropertyService = GetBuisnessService<IGamePropertyService>();
            if (!User.Identity.IsAuthenticated)
            {
                GreatingLabel.Text = "You must be logged in to mark a movie as watched!";
            }
            else
            {
                GreatingLabel.CssClass = "hidden";
            }
            if (gamePropertyService.IsGameNotStartedYet())
            {
                GreatingLabel.CssClass = "hidden";
                WarningLabel.CssClass = "hidden";
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
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var currentUsereId = User.Identity.GetUserId();

            IEnumerable<Movie> movies = (IEnumerable<Movie>)e.ReturnValue;
            var moviesCount = movies.Count();
            //var bettedCategories = categories.Sum(x => x.Bets.Count(b => b.UserId == currentUsereId));
            var watchedMovies = movies.Sum(x => x.UsersWatchedThisMovie.Count(u => u.UserId == currentUsereId));

            var missedMovies = moviesCount - watchedMovies;
            if (CheckIfTheUserIsLogged() == true)
            {
                if (missedMovies > 0)
                {
                    if (missedMovies == 1)
                    {
                        WarningLabel.Text = "There are " + moviesCount + " nominated movies. " +
                            "You have " + (missedMovies) + " more movie to watch!";
                    }
                    else
                    {
                        WarningLabel.Text = "There are " + moviesCount + " nominated movies. " +
                            "You have " + (missedMovies) + " more movies to watch!";
                    }
                }
                else
                {
                    WarningLabel.CssClass = "goldBorder-left";
                    WarningLabel.Text = "Congretilations! You have watched all the " + moviesCount + " movies!";
                }
            }
            else
            {
                WarningLabel.CssClass = "hidden";
            }
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = GetBuisnessService<IMovieService>();
        }
    }
}