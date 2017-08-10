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
                string sortByAndArrangeBy = (e.CommandArgument).ToString();
                char[] separator = { '|' };
                string[] sortByAndArrangeByArray = sortByAndArrangeBy.Split(separator);
                var movieId = int.Parse(sortByAndArrangeByArray[0]);
                var categoryId = int.Parse(sortByAndArrangeByArray[1]);
                var service = new CategoryService();                               
                //var betEntity = new Bet() { UserId = userId, Movie= service.GetMovie(movieId), Category= service.GetCategory(categoryId) };
                var betEntity = service.MakeBetEntity(userId, movieId, categoryId);
                Label2.Text = User.Identity.Name + " added new entity with userId= " + betEntity.UserId + "  movieId= " + betEntity.Movie.Id +  " and categoryId= " + betEntity.Category.Id;
                               
                Response.Redirect("/CommonPages/ShowCategories.aspx?userId=" + userId);

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