﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using MovieScrapper.Business;
using MovieScrapper.Entities;

namespace MovieScrapper.CommonPages
{
    public partial class ShowAllDBMovies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {                    
           if (User.Identity.IsAuthenticated)
            {
                Label1.Text = "Hello " + User.Identity.Name + "! Here you can mark the movies you have watched!";
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

            return "/CommonPages/DBMovieDetails.aspx?id=" + movieId + "&back=/CommonPages/ShowAllDBMovies";
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsWatched")
            {
                var userId = User.Identity.GetUserId();
                var movieId = int.Parse(e.CommandArgument.ToString());                                
                var service = new CategoryService();              
                
                if (service.GetUserWatchedEntity(userId)==null)
                {
                    var watchedEntity = new Watched() { UserId = userId, Movies = new List<Movie>() };
                    watchedEntity = service.AddWatchedEntity(watchedEntity);
                    Label2.Text = User.Identity.Name + " added new entity with userId= " + watchedEntity.UserId;
                }
                else
                {
                    Label2.Text = "This user has an entity.";
                }
                service.AddWatchedMovie(userId, movieId);
                Response.Redirect("/CommonPages/ShowAllDBMovies.aspx?userId=" + userId);
                
            }
        }
        //<%--Enabled='<%# !Item.UsersWatchedThisMovie.Any(x => x.UserId == User.Identity.GetUserId()) %>'--%>
        protected bool DoesUserWatchedThisMovie(ICollection<Watched> users)
        {
            return !users.Any(x => x.UserId == User.Identity.GetUserId());
        }

        protected string ChangeTextIfUserWatchedThisMovie(ICollection<Watched> users)
        {
            if (!users.Any(x => x.UserId == User.Identity.GetUserId()))
            {
                return "&#252;";
            }
            else
            {
                return "&#10004;";
            }
        }
    }
}