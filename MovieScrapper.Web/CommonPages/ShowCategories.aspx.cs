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
                //if (!Page.IsPostBack)
                //{
                //    Repeater1.DataBind();
                //    IEnumerable<Category> data = (IEnumerable<Category>)Repeater1.DataSource;
                //    DisplayPageSummary(data);
                //}
            }
            else
            {
                GreatingLabel.Text = "You must be logged in to mark a movie as watched!";
            }
        }

        private void DisplayPageSummary(IEnumerable<Category> data)
        {
            //var service = new CategoryService();
            //var categoryCount = service.GetAll().Count();
            //Label1.Text = "Hello " + User.Identity.Name + "! Here you can bet on the movie which you think will win!";
            //Label3.Text = "You betted in " + service.GetAllUserBets(User.Identity.GetUserId()).Count() + " categories. " + "Categories are " + categoryCount ;
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

        protected string BuildImdbUrl(int movieId)
        {

            return "https://www.themoviedb.org/movie/" + movieId;
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
                //IEnumerable<Category> data = (IEnumerable<Category>)Repeater1.DataSource;
                //DisplayPageSummary(data);
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

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var currentUsereId = User.Identity.GetUserId();

            IEnumerable<Category> categories = (IEnumerable<Category>)e.ReturnValue;
            var categoryCount = categories.Count();
            var bettedCategories = categories.Sum(x => x.Bets.Count(b => b.UserId == currentUsereId));

            //int sum = 0;
            //foreach (Category cat in categories)
            //{
            //    sum += cat.Bets.Where(x => x.UserId == currentUsereId).Count();
            //}

           // var service = new CategoryService();
            //var bettedCategories = service.GetAllUserBets(currentUsereId).Count();
            GreatingLabel.Text = "Hello " + User.Identity.Name + "! Here you can bet on the movie which you think will win!";
            var missedCategories = categoryCount - bettedCategories;
            if (missedCategories > 0)
            {
                if (missedCategories == 1)
                {
                    WarningLabel.Text = "Don't miss your chance! " + " You have " + (missedCategories) + " more category to bet!";
                }
                else
                {
                    WarningLabel.Text = "Don't miss your chance! " + " You have " + (missedCategories) + " more categories to bet!";
                }
            }
            else
            {
                WarningLabel.CssClass = "hidden";
                //WarningLabel.Attributes.Add("class", "hidden");
            }
        }
    }
}