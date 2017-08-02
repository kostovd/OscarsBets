using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ctx = new MovieContext())
            {
                var test = ctx.MovieCaterogries.ToList();
            }
        }
      

        protected void AddCategoryButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditCategory.aspx");
        }

        protected void EditCategoryButton_Click(object sender, EventArgs e)
        {
           
        }

        protected void EditMoviesInThisCategoryButton_Click(object sender, EventArgs e)
        {

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditCategory")
            {
                var id = e.CommandArgument;
                Response.Redirect("EditCategory.aspx?id=" + id.ToString());
            }
            
            if (e.CommandName == "EditMoviesInThisCategory")
            {
                var id = e.CommandArgument;
                Response.Redirect("EditMoviesInThisCategory.aspx?categoryId=" + id.ToString());
            }
        }

        
    }
}