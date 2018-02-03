
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
      

        public string BuildPosterUrl(string path)
        {
            return "http://image.tmdb.org/t/p/w300" + path;
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

            return "/CommonPages/MovieDetails.aspx?id=" + movieId + "&back=" + Request.Url.PathAndQuery;
        }

        protected string BuildImdbUrl(string movieId)
        {

            return "http://www.imdb.com/title/" + movieId;
        }

    }
}