
using Microsoft.Practices.Unity;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Web.Providers.Entities;

namespace MovieScrapper
{
    public partial class MovieControl : System.Web.UI.UserControl
    {
        public Movie Item { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected T GetBuisnessService<T>()
        {
            IUnityContainer container = (IUnityContainer)Application["EntLibContainer"];
            return container.Resolve<T>();
        }

    }
}