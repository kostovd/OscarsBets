using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.Admin
{
    public partial class EditMoviesInThisCategory : BasePage
    {
        private ICategoryService GetCategoryService()
        {
            return GetBuisnessService<ICategoryService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var service = GetCategoryService();
                var category = service.GetCategory(categoryId);
                CategoryTitle.Text = category.CategoryTtle;      
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
                var service = GetCategoryService();
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
                var service = GetCategoryService();
                service.MarkAsWinner(categoryId, movieId);
                Response.Redirect("EditMoviesInThisCategory?categoryId=" + categoryId);
            }
        }

        protected string CheckIfWinnerImage(Nomination nomination)
        {
            return nomination.IsWinner ?
                    "/images/Oscar_logo.png" :
                    "";
        }

        protected void BackToEditCategoriesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categories.aspx" );
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = GetBuisnessService<IMovieService>();
        }
    }
}