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
            if (User.Identity.IsAuthenticated)
            {
                Label1.Text = "Hello " + User.Identity.Name + "! Here you can bet on the movie which you think will win!";
            }
            else
            {
                Label1.Text = "You must be logged in to mark a movie as watched!";
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
            
            return "/CommonPages/DBMovieDetails.aspx?id=" + movieId + "&back=/CommonPages/ShowCategories";
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsBetted")
            {
                var userId = User.Identity.GetUserId();
                var movieId = int.Parse(e.CommandArgument.ToString());
                var service = new CategoryService();
                Label2.Text = User.Identity.Name + " betted on movie with Id = " + movieId;
                //if (service.GetUserWatchedEntity(userId) == null)
                //{
                //    var watchedEntity = new Watched() { UserId = userId, Movies = new List<Movie>() };
                //    watchedEntity = service.AddWatchedEntity(watchedEntity);

                //}

                //service.AddWatchedMovie(userId, movieId);
               // Response.Redirect("/CommonPages/ShowCategories?userId=" + userId);

            }
        }

        protected bool DoesUserBetOnThisMovie(ICollection<Bet> users)
        {
            //return !users.Any(x => x.UserId == User.Identity.GetUserId());
            return true;
        }


        protected string ChangeTextIfUserBettedOnThisMovie(ICollection<Bet> users)
        {
            //if (!users.Any(x => x.UserId == User.Identity.GetUserId()))
            if(true)
            {
                return "o"; //code 111 in ASCI
            }
            else
            {
                return "þ"; //code 254 in ASCI
            }
        }
    }
}