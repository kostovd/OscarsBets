using MovieScrapper.Business;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using MovieScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.Admin
{
    public partial class EditCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];
            if (!IsPostBack)
            {               
                if (id != null)
                {
                    var service = new CategoryService();
                    var category = service.GetCategory(int.Parse(id));
                    EditCategoryTitleTextBox.Text = category.CategoryTtle;
                    EditCategoryDescriptionTextBox.Text = category.CategoryDescription;
                }
            }

            if (id != null)
            {
                DeleteCategoryButton.Visible = true;
            }
            else
            {
                DeleteCategoryButton.Visible = false;
            }
        }

        protected void SaveChangesButton_Click(object sender, EventArgs e)
        {

            //int id = Int32.Parse(Request.QueryString["id"]);
            string categoryTitle = EditCategoryTitleTextBox.Text;
            string categoryDescription = EditCategoryDescriptionTextBox.Text;
            MovieCategory category = new MovieCategory() { CategoryTtle = categoryTitle, CategoryDescription = categoryDescription };
            var service = new CategoryService();
            
            try
            {
                service.AddCategory(category);
                Response.Redirect("Categories.aspx");
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            
        }

        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["id"]);
            var service = new CategoryService();

            try
            {
                service.DeleteCategory(id);
                Response.Redirect("Categories.aspx");
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categories.aspx");
        }
    }
}