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
            //Calendar1.SelectedDate = DateTime.Today;

        }

        protected void StartGameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (StartGameCalendar.SelectedDate == null || StartGameCalendar.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0))// not click any date
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        protected void StopGameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (StopGameCalendar.SelectedDate == null || StopGameCalendar.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0))// not click any date
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void ChangeStartDateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var service = new CategoryService();
                var changeDate = StartGameCalendar.SelectedDate;
                service.ChangeGameStartDate(changeDate);
                Response.Redirect("Categories.aspx");
            }
        }

        protected void ChangeStopDateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var service = new CategoryService();
                var changeDate = StopGameCalendar.SelectedDate;
                service.ChangeGameStopDate(changeDate);
                Response.Redirect("Categories.aspx");
            }
        }
       
    }
}