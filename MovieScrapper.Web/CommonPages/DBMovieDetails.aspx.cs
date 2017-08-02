using MovieScrapper.Data;
using MovieScrapper.Entities;
using MovieScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class DBMovieDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var movieId= Request.QueryString["id"];

            using (var ctx = new MovieContext())
            {
                var movie = ctx.Movies.FirstOrDefault(m => m.Id == movieId);
                DetailsView1.DataSource= new Movie[] { movie };
                DetailsView1.DataBind();
            }

        }

        protected string BuildPosterUrl(string path)
        {
            return "http://image.tmdb.org/t/p/w300" + path;
        }
        protected string BuildBackUrl()
        {

            string backUrl = Request.QueryString["back"];
            return backUrl;
        }
    }
}