using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace MovieScrapper.Secured
{
    public partial class MyMovies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TextBox1.Focus();

            if (!Page.IsPostBack)
            {
                var name = Request.QueryString["name"];
                var categoryId = Request.QueryString["categoryId"];
                if (name != null)
                {
                    if (name != String.Empty)
                    {
                        TextBox1.Text = name;
                        RegisterAsyncTask(new PageAsyncTask(LoadMoviesAsync));
                    }

                    else
                    {
                        TextBox1.Text = "Please enter a title";
                    }
                }
                                
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(BuildUrlWithName(TextBox1.Text));

            RegisterAsyncTask(new PageAsyncTask(LoadMoviesAsync));
        }

        private async Task LoadMoviesAsync()
        {
            var environmentKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
            var movieClient = new MovieClient(environmentKey);
            var searchedMovie = TextBox1.Text;
            try
            {

                    var movies = await movieClient.SearchMovieAsync(searchedMovie);
                    MoviesDataList.DataSource = movies.Results;
                    MoviesDataList.DataBind();

            }
            catch (Exception e)
            {
                TextBox1.Text = e.Message;
            }
            

        }

        protected string BuildUrl(string path)
        {

            return "http://image.tmdb.org/t/p/w92" + path;
        }

        protected string BuildUrlWithName(string name)
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

        protected string BuildUrlWithId(string id)
        {
            var categoryId = Request.QueryString["categoryId"];
            if (categoryId != null)
            {
                string encodedBackUrl = Server.UrlEncode("ShowMovies?name=" + TextBox1.Text + "&categoryId=" + categoryId);
                return "MovieDetails.aspx?id=" + id + "&categoryId=" + categoryId + "&back=" + encodedBackUrl;
            }
            else
            {
                return "MovieDetails.aspx?id=" + id + "&back=ShowMovies?name=" + Server.UrlEncode(TextBox1.Text);
            }


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

        protected void MoviesDataList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BackToEditMoviesInThisCategoryButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/EditMoviesInThisCategory.aspx?categoryId=" + Request.QueryString["categoryId"]);
        }
    }
}
