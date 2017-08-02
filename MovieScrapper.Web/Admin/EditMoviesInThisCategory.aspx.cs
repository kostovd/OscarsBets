using MovieScrapper.Data;
using MovieScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.Admin
{
    public partial class EditMoviesInThisCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                LoadCategory(categoryId);
                LoadMovies(categoryId);
            }
        }

        private void LoadMovies(int categoryId)
        {

            var movieClient = new MoviesLocalDBClient();

            try
            {

                var movies = movieClient.ShowMoviesInCategory(categoryId);
                DataList1.DataSource = movies;
                DataList1.DataBind();

            }
            catch (Exception e)
            {

            }

        }

        private void LoadCategory(int categoryId)
        {

            var movieClient = new MoviesLocalDBClient();

            try
            {

                var category = movieClient.ShowCategory(categoryId);
                CategoryTitle.Text = category.CategoryTtle;
            }
            catch (Exception e)
            {

            }

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

        public string BuildPosterUrl(string path)
        {

            return "http://image.tmdb.org/t/p/w92" + path;
        }

        public string BuildUrlWithId(string id)
        {
            var categoryId = Request.QueryString["categoryId"];
            
                string encodedBackUrl = Server.UrlEncode("DBMovieDetails?id=" + id);
                return "EditMoviesInThisCategory?categoryId=" +  categoryId + "&back=" + encodedBackUrl;
                     
        }

        public string BuildUrlWithName(string name)
        {
            var categoryId = Request.QueryString["categoryId"];
            if (categoryId != null)
            {
                return "ShowMovies?name=" + name + "&categoryId=" + categoryId;
            }
            else
            {
                return "ShowMovies?name=" + name;
            }
        }

        protected void AddMovieToThisCategoryButton_Click(object sender, EventArgs e)
        {
            var categoryId = Request.QueryString["categoryId"];
            Response.Redirect("/CommonPages/ShowMovies.aspx?categoryId=" + categoryId);
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var movieId = e.CommandArgument.ToString();

                using (var ctx = new MovieContext())
                {
                    var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                    var databaseCategory = ctx.MovieCaterogries.Include("Movies").SingleOrDefault(x => x.Id == categoryId);
                    var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                    databaseCategory.Movies.Remove(foundedMovie);
                    //ctx.Entry(fosundedMovie).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }
                LoadMovies(categoryId);
            }                     

            if (e.CommandName == "ShowDetails")
            {
                var id= e.CommandArgument.ToString();
                
                Response.Redirect("/CommonPages/DBMovieDetails.aspx?id=" + id.ToString()+ "&back=/Admin/EditMoviesInThisCategory?categoryId="+ Request.QueryString["categoryId"]);
            }
        }

        protected void BackToEditCategoriesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categories.aspx" );
        }
    }
}