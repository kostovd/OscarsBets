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

            // Bind
            BindDataTableToGrid(new DataView(dt));
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
                field.HeaderText = "<span class='goldFont'>" + title + "</span>";                            
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
                        row[title] = "<span style='font-family:Wingdings;color:rgb(179,0,0); text-align: center;font-size:30px;'>&#252;</span>";
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

        //private void BindGrid()
        //{
        //    var watcheMoviesStatisticService = GetBuisnessService<IWatcheMoviesStatisticService>();
        //    var users = watcheMoviesStatisticService.GetData();
        //    DataTable dt = new DataTable();

        //    dt.Columns.AddRange(new DataColumn[2] { new DataColumn("User", typeof(string)), new DataColumn("Sum", typeof(string)),});
        //    foreach (var user in users)
        //    {
        //        dt.Rows.Add(user);

        //    }           

        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();

        //}

        //protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    if (e.Row.RowType == DataControlRowType.DataRow && !this.IsPostBack)
        //    {

        //        var watcheMoviesStatisticService = GetBuisnessService<IWatcheMoviesStatisticService>();
        //        var watchedObjects = watcheMoviesStatisticService.GetData();
        //        var allTitles = watcheMoviesStatisticService.GetTitles();
        //        var allUsers = watchedObjects.Select(x=>x.UserEmail).ToArray();               
        //        var index = e.Row.RowIndex;
        //        var userName = allUsers[index];
        //        var userTitles = watchedObjects[index].MovieTitles;

        //        e.Row.Cells[0].Text = userName;               

        //        e.Row.Cells[1].Text = userTitles.Count.ToString();
        //        e.Row.Cells[1].CssClass = "columnscss";

        //        for (int i = 0; i <allTitles.Count(); i++)
        //        {
        //            for(int j = 0; j < userTitles.Count; j++)
        //            {
        //                if (allTitles[i] == userTitles[j])
        //                {

        //                        e.Row.Cells[i + 2].Text = "<span style='font-family:Wingdings;color:rgb(179,0,0); text-align: center;font-size:30px;'>&#252;</span>";                              
        //                        e.Row.Cells[i+2].CssClass = "columnscss";
        //                }
        //            }

        //        }

        //    }

        //}

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            //var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            //var categories = betStatisticServices.GetCategories();

            //DataTable dt = CreateDataTable(categories);
            //dt = FillDataTable(dt);
            //DataView dv = new DataView(dt);

            //SortDirection sortDirection = GetSortDiraction(e.SortExpression);

            //if (sortDirection == SortDirection.Ascending)
            //{
            //    dv.Sort = e.SortExpression + " ASC";
            //}
            //else
            //{
            //    dv.Sort = e.SortExpression + " DESC";
            //}

            //e.SortDirection = sortDirection;
            //BindDataTableToGrid(dv);

            //GridViewSortExpression = e.SortExpression;
            //GridViewSortDirection = sortDirection;
        }
    }
}