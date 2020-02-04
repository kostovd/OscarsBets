﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MovieScrapper.Business.Enums;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;

namespace MovieScrapper.CommonPages
{
    public partial class ShowAllDBMovies : BasePage
    {
        private const string NormalOpacity = "opacity: 1";
        private const string FadedOpacity = "opacity: 0.3";

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
                Session["CurrentUser"] = User.Identity.Name;
            }
            if (gamePropertyService.IsGameNotStartedYet())
            {
                GreatingLabel.CssClass = "hidden";
                WarningLabel.CssClass = "hidden";
            }
        }

        public string BuildPosterUrl(string path)
        {
            return "https://image.tmdb.org/t/p/w92" + path;
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
                if (IsGameRunning())
                {
                    var userId = User.Identity.Name;
                    int movieId = int.Parse((e.CommandArgument).ToString());


                    var watchedMovieService = GetBuisnessService<IWatchedMovieService>();
                    var movieService = GetBuisnessService<IMovieService>();
                    if (watchedMovieService.GetUserWatchedEntity(userId) == null)
                    {
                        var watchedEntity = new Watched() { UserId = userId, Movies = new List<Movie>() };
                        watchedEntity = watchedMovieService.AddWatchedEntity(watchedEntity);
                    }

                    movieService.ChangeMovieStatus(userId, movieId);
                    Repeater1.DataBind();
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    Response.Redirect("ShowAllDBMovies.aspx");
                }
            }
        }

        protected bool DoesUserWatchedThisMovie(ICollection<Watched> users)
        {
            return !users.Any(x => x.UserId == User.Identity.Name);
        }

        protected string ChangeTextIfUserWatchedThisMovie(ICollection<Watched> users)
        {
            if (!users.Any(x => x.UserId == User.Identity.Name))
            {
                return "<span class='check-button glyphicon glyphicon-unchecked'></span>";
            }
            else
            {
                return "<span class='check-button glyphicon glyphicon-check'></span>";
            }
        }

        protected string GetNominaionsInfo(Movie movie)
        {
            int nominationsCount = movie.Nominations.Count;

            if (nominationsCount == 1)
            {
                return "1 nomination";
            }
            else if (nominationsCount > 1)
            {
                return nominationsCount + " nominations";
            }
            else
            {
                return string.Empty;
            }
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var currentUsereId = User.Identity.Name;

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
                    WarningLabel.Text = "Congratulations! You have watched all the " + moviesCount + " movies!";
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

        protected string SetFadeFilter(Movie movie)
        {
            if (!User.Identity.IsAuthenticated)
                return NormalOpacity;

            int selectedFilter = int.Parse(DdlFilter.SelectedValue);

            if (selectedFilter == (int)FadeFilterType.Unwatched
                && !movie.UsersWatchedThisMovie.Select(x => x.UserId).Contains(User.Identity.Name))
            {
                return FadedOpacity;
            }

            if (selectedFilter == (int)FadeFilterType.Watched
                && movie.UsersWatchedThisMovie.Select(x => x.UserId).Contains(User.Identity.Name))
            {
                return FadedOpacity;
            }

            return NormalOpacity;
        }
    }
}