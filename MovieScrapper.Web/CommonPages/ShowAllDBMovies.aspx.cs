using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using MovieScrapper.Business;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;

namespace MovieScrapper.CommonPages
{
    public partial class ShowAllDBMovies : BasePage
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

        public bool IsGameRunning()
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

        public bool IsGameNotStartedYet()
        {
            var gamePropertyService = GetBuisnessService<IGamePropertyService>();
            if (gamePropertyService.IsGameNotStartedYet() == true)
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

        protected string BuildUrl(int movieId)
        {

            return "/CommonPages/DBMovieDetails.aspx?id=" + movieId + "&back=/CommonPages/ShowAllDBMovies";
        }

        protected string BuildImdbUrl(string movieId)
        {

            return "http://www.imdb.com/title/" + movieId;
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsWatchedOrUnwatched")
            {
                var userId = User.Identity.GetUserId();
                int movieId = int.Parse((e.CommandArgument).ToString());
                

                var watchedMovieService = GetBuisnessService<IWatchedMovieService>();
                var movieService = GetBuisnessService<IMovieService>(); 
                if (watchedMovieService.GetUserWatchedEntity(userId)==null)
                {
                    var watchedEntity = new Watched() { UserId = userId, Movies = new List<Movie>() };
                    watchedEntity = watchedMovieService.AddWatchedEntity(watchedEntity);                   
                }
                
                movieService.ChangeMovieStatus(userId, movieId);
                Response.Redirect("/CommonPages/ShowAllDBMovies.aspx?userId=" + userId);
                
            }
        }
        
        protected bool DoesUserWatchedThisMovie(ICollection<Watched> users)
        {
            return !users.Any(x => x.UserId == User.Identity.GetUserId());
        }

        protected string ChangeTextIfUserWatchedThisMovie(ICollection<Watched> users)
        {
            if (!users.Any(x => x.UserId == User.Identity.GetUserId()))
            {
                return "o"; //code 111 in ASCI
            }
            else
            {
                return "þ"; //code 254 in ASCI
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