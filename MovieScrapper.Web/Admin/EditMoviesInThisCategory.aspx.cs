using MovieScrapper.Business;
using System;
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
                var service = new CategoryService();
                var category = service.GetCategory(categoryId);
                CategoryTitle.Text = category.CategoryTtle;      
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
                var movieId = Int32.Parse(e.CommandArgument.ToString());
                var service = new CategoryService();
                service.RemoveMovieFromCategory(categoryId, movieId);
                Response.Redirect("EditMoviesInThisCategory?categoryId=" + categoryId);
            }                     

            if (e.CommandName == "ShowDetails")
            {
                var id= e.CommandArgument.ToString();
                
                Response.Redirect("/CommonPages/DBMovieDetails.aspx?id=" + id.ToString()+ "&back=/Admin/EditMoviesInThisCategory?categoryId="+ Request.QueryString["categoryId"]);
            }

            if (e.CommandName == "MarkAsWinner")
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var movieId = Int32.Parse(e.CommandArgument.ToString());
                var service = new CategoryService();
                service.MarkAsWinner(categoryId, movieId);
                Response.Redirect("EditMoviesInThisCategory?categoryId=" + categoryId);
            }
        }

        protected string CheckIfWinner( int currentMovieId)
        {
            var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
            var service = new CategoryService();
            var category= service.GetCategory(categoryId);
            var winner = category.Winner;

            if (winner == null)
            {
                return "";
            }
            else
            {
                if (winner.Id.ToString() == currentMovieId.ToString())
                {
                    return "winner";
                }
                else
                {
                    return "notWinner";
                }
            }

        }

        protected string CheckIfWinnerImage(int currentMovieId)
        {
            var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
            var service = new CategoryService();
            var category = service.GetCategory(categoryId);
            var winner = category.Winner;

            if (winner == null)
            {
                return "";
            }
            else
            {
                if (winner.Id.ToString() == currentMovieId.ToString())
                {
                    return "/Oscar_logo.png";
                }
                else
                {
                    return "";
                }
            }

        }

        protected void BackToEditCategoriesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categories.aspx" );
        }
    }
}