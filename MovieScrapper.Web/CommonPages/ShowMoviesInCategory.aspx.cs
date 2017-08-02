using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class ShowMoviesInCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
            LoadMovies(categoryId);
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

        public string BuildUrl(string path)
        {

            return "http://image.tmdb.org/t/p/w92" + path;
        }

        //public string BuildUrlWithId(string id)
        //{
        //    var categoryId = Request.QueryString["categoryId"];
        //    if (categoryId != null)
        //    {
        //        string encodedBackUrl = Server.UrlEncode("MyMovies?name=" + TextBox1.Text + "&categoryId=" + categoryId);
        //        return "MovieDetails.aspx?id=" + id + "&categoryId=" + categoryId + "&back=" + encodedBackUrl;
        //    }
        //    else
        //    {
        //        return "MovieDetails.aspx?id=" + id + "&back=MyMovies?name=" + Server.UrlEncode(TextBox1.Text);
        //    }

        //}

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

    }
}