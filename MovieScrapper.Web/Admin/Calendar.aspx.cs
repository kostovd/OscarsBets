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
            if (!Page.IsPostBack)
            {
                var service = new GamePropertyService();
                StartGameCalendar.SelectedDate = service.GetGameStartDate();
                StartGameCalendar.VisibleDate = service.GetGameStartDate();
                StopGameCalendar.SelectedDate = service.GetGameStopDate();
                StopGameCalendar.VisibleDate = service.GetGameStopDate();
            }
        }

        protected void StopGameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (StartGameCalendar.SelectedDate == null 
                || StartGameCalendar.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0)
                ||StopGameCalendar.SelectedDate == null 
                || StopGameCalendar.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0)
                || StartGameCalendar.SelectedDate>=StopGameCalendar.SelectedDate)// not click any date
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        protected void ChangeDateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var service = new GamePropertyService();
                var startDate = StartGameCalendar.SelectedDate;
                service.ChangeGameStartDate(startDate);
                var stopDate = StopGameCalendar.SelectedDate;
                service.ChangeGameStopDate(stopDate);
                Response.Redirect("Categories.aspx");
            }
        }

    }
}