using MovieScrapper.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.Admin
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Calendar1.SelectedDate == null || Calendar1.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0))// not click any date
                args.IsValid = false;
            else
                args.IsValid = true;
        }
       

        protected void ChangeDateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var service = new CategoryService();
                var changeDate = Calendar1.SelectedDate;
                service.ChangeGameStopDate(changeDate);
                Response.Redirect("Categories.aspx");
            }

        }
    }
}