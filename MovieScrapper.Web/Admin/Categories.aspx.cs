﻿using MovieScrapper.Business;
using MovieScrapper.Business.Interfaces;
using System;
using System.Web.UI.WebControls;


namespace MovieScrapper.Admin
{
    public partial class Categories : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }        

        protected void AddCategoryButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditCategory.aspx");
        }
       

        protected void ShowChangeDateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Calendar.aspx");

        }

        protected void EditUsersButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");

        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditCategory")
            {
                var id = e.CommandArgument;
                Response.Redirect("EditCategory.aspx?id=" + id.ToString());
            }

            if (e.CommandName == "ShowMoviesInThisCategory")
            {
                var id = e.CommandArgument;
                Response.Redirect("EditMoviesInThisCategory.aspx?categoryId=" + id.ToString());
            }
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = GetBuisnessService<ICategoryService>();
        }
    }
}