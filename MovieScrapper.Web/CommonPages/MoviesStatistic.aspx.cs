using MovieScrapper.Business;
using MovieScrapper.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class MoviesStatistic : BasePage
    {
        private const string UserColumnName = "Email";
        private const string ScoresColumnName = "Scores";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"]= "Scores";
                }
                GridViewInit();
            }

        }

        private void GridViewInit()
        {
            var watcheMoviesStatisticService = GetBuisnessService<IWatcheMoviesStatisticService>();
            string[] titles = watcheMoviesStatisticService.GetTitles();

            InitGridViewColumns(titles);

            // Create
            var dt = CreateDataTable(titles);

            // Fill
            dt = FillDataTable(dt);

            //Sort
            DataView sortedView = DefaultTableSort(dt, " DESC", ScoresColumnName);

            // Bind
            BindDataTableToGrid(sortedView);
        }

        // CreateGridViewColumns()
        private void InitGridViewColumns(string[] titles)
        {            
            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            GridView1.Columns.Add(field);

            field = new BoundField();
            field.HeaderText = "Scores";
            field.DataField = ScoresColumnName;
            field.SortExpression = ScoresColumnName;
            
            GridView1.Columns.Add(field);

            foreach (string title in titles)
            {
                field = new BoundField();
                field.HeaderStyle.Width = Unit.Pixel(100);
                field.HeaderText = "<span class='redFont'>" + title + "</span>";                            
                field.DataField = title;
                field.HtmlEncode = false;
                GridView1.Columns.Add(field);
            }
        }

        // CreateDataTable()
        private DataTable CreateDataTable(string[] titles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(UserColumnName, typeof(string));
            dt.Columns.Add(ScoresColumnName, typeof(int));

            foreach (string title in titles)
            {
                dt.Columns.Add(title);
            }
            return dt;
        }

        // FillDataTable()
        private DataTable FillDataTable(DataTable dt)
        {
            var watcheMoviesStatisticService = GetBuisnessService<IWatcheMoviesStatisticService>();
            var users = watcheMoviesStatisticService.GetData();
            foreach (var user in users)
            {
                var row = dt.NewRow();
                row[UserColumnName] = user.UserEmail;

                int scores = 0;
                foreach (var title in user.MovieTitles)
                {                  
                        row[title] = "<span class='	glyphicon glyphicon-ok'></span>";
                        scores++;                 
                }

                row[ScoresColumnName] = scores;

                dt.Rows.Add(row);
            }
            return dt;
        }

        protected void BindDataTableToGrid(DataView dv)
        {
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        DataView DefaultTableSort (DataTable dt, string sortDirection, string sortExpresion)
        {
            DataView dv= new DataView(dt);
            dv.Sort = sortExpresion + sortDirection;

            return dv;
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            //var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            var watcheMoviesStatisticService = GetBuisnessService<IWatcheMoviesStatisticService>();
            var titles = watcheMoviesStatisticService.GetTitles();

            DataTable dt = CreateDataTable(titles);
            dt = FillDataTable(dt);
            DataView dv = new DataView(dt);

            SortDirection sortDirection = GetSortDiraction(e.SortExpression);

            if (sortDirection == SortDirection.Ascending)
            {
                dv.Sort = e.SortExpression + " ASC";
            }
            else
            {
                dv.Sort = e.SortExpression + " DESC";
            }

            e.SortDirection = sortDirection;
            BindDataTableToGrid(dv);

            GridViewSortExpression = e.SortExpression;
            GridViewSortDirection = sortDirection;
        }

        private SortDirection GetSortDiraction(string sortExpression)
        {
            //if (sortExpression != GridViewSortExpression)
            //{
            //    return SortDirection.Ascending;
            //}

            //return (GridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending);


            SortDirection res;

            if (sortExpression == GridViewSortExpression)
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    res = SortDirection.Descending;
                }
                else
                {
                    res = SortDirection.Ascending;
                }
            }
            else
            {
                res = SortDirection.Ascending;
            }

            return res;
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                    ViewState["SortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["SortDirection"];
            }
            set { ViewState["SortDirection"] = value; }
        }


        private string GridViewSortExpression
        {
            get { return ViewState["SortExpression"] as string;}
            set { ViewState["SortExpression"] = value; }
        }
    }
}