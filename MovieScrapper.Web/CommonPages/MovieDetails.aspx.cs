using Microsoft.Practices.Unity;
using MovieScrapper.Business;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace MovieScrapper
{
    public partial class MovieDetails : BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {      
            
            RegisterAsyncTask(new PageAsyncTask(LoadMovieDetailsAsync));

            if (HttpContext.Current.User.IsInRole("admin") & Request.QueryString["categoryId"]!=null)
            {
                AddMovieToCategoryButton.Visible = true;
            }
            else
            {
                AddMovieToCategoryButton.Visible = false;
            }

        }

        private async Task LoadMovieDetailsAsync()
        {
            var environmentKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
            var movieClient = new MovieClient(environmentKey);
            var id= Request.QueryString["id"];
            var movie = await movieClient.GetMovieAsync(id);

            DetailsView1.DataSource = new Movie[] { movie };
            DetailsView1.DataBind();

            ViewState["Movie"] = movie;

        }

        protected string BuildPosterUrl(string path)
        {
            return "http://image.tmdb.org/t/p/w185" + path;
        }

        protected string BuildBackUrl()
        {
            
            string backUrl = Request.QueryString["back"];
            return backUrl;
        }

        protected string BuildImdbUrl(string movieId)
        {

            return "http://www.imdb.com/title/" + movieId;
        }

        protected string DisplayYear(string dateString)
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

        protected void AddMovieToCategoryButton_Click(object sender, EventArgs e)
        {           
            var movie = ViewState["Movie"] as Movie;

            if (movie != null)
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var movieId = Int32.Parse(Request.QueryString["id"]);
                var movieService = GetBuisnessService<IMovieService>();
                var categoryService = GetBuisnessService<ICategoryService>();
                categoryService.AddMovieInCategory(categoryId, movie);              

                Response.Redirect("/Admin/EditMoviesInThisCategory?categoryId=" + categoryId);               
            }
        }       
        
    }
}