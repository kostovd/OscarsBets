using MovieScrapper.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities.StatisticsModels;

namespace MovieScrapper.CommonPages
{
    public partial class BetsStatistics : BasePage
    {
        private const string UserColumnName = "Email";
        private const string ScoresColumnName = "Scores";

        private bool? _gameIsRunning = null;
        private bool GameIsRunning()
        {
            if (!_gameIsRunning.HasValue)
            {
                var gamePropertyService = GetBuisnessService<IGamePropertyService>();
                _gameIsRunning = !gamePropertyService.IsGameStopped();
            }

            return _gameIsRunning.Value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            if (!this.IsPostBack)
            {
                GridViewInit();

                if (!GameIsRunning())
                {
                    Label1.Text = betStatisticServices.GetWinner();
                }
            }
        }

        private void GridViewInit()
        {
            IBetStatisticService betStatisticServices = GetBuisnessService<IBetStatisticService>();
            string[] categories = betStatisticServices.GetCategories();
            List<Winners> winners = betStatisticServices.GetWinners();

            InitGridViewColumns(categories, winners);

            // Create
            var dt = CreateDataTable(categories);

            // Fill
            dt = FillDataTable(dt);

            // Bind
            BindDataTableToGrid(new DataView(dt));
        }

        // CreateGridViewColumns()
        private void InitGridViewColumns(string[] categories, List<Winners> winners)
        {
            bool allWinnersAreSet = (winners.Count == categories.Count());

            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            GridView1.Columns.Add(field);

            field = new BoundField();
            field.HeaderText = "Scores";
            field.DataField = ScoresColumnName;
            field.SortExpression = ScoresColumnName;
            if (GameIsRunning())
            {
                field.Visible = false;
            }
            GridView1.Columns.Add(field);

            foreach (string category in categories)
            {
                field = new BoundField();
                if (GameIsRunning())
                {
                    field.HeaderText = "<span class='redFont'>" + category + "</span>";
                }
                else
                {
                    if (allWinnersAreSet)
                    {
                        var winner = winners.Where(x => x.Category == category).Select(x => x.Winner).Single();
                        field.HeaderText = "<span class='redFont'>" + category + "</span><br/><span style='color:rgb(237,192,116)'>" + winner + "</span>";
                    }
                    else
                    {
                        field.HeaderText = "<span class='redFont'>" + category + "</span><br/><span style='font-size:10px;'> Waiting to know the winner </span>";
                    }
                }
                field.DataField = category;
                field.HtmlEncode = false;
                GridView1.Columns.Add(field);
            }
        }

        // CreateDataTable()
        private DataTable CreateDataTable(string[] categories)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(UserColumnName, typeof(string));
            dt.Columns.Add(ScoresColumnName, typeof(int));

            foreach (string category in categories)
            {
                dt.Columns.Add(category);
            }
            return dt;
        }

        // FillDataTable()
        private DataTable FillDataTable(DataTable dt)
        {
            var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            var users = betStatisticServices.GetData();
            foreach (var user in users)
            {
                var row = dt.NewRow();
                row[UserColumnName] = user.UserEmail;

                int scores = 0;
                foreach (var bet in user.UserBets)
                {
                    //row[bet.CategoryTitle] = bet;
                    if (bet.IsRightGuess && !GameIsRunning())
                    {
                        row[bet.CategoryTitle] = bet.MovieTitle + "<span class='glyphicon glyphicon-thumbs-up'></span>";
                        scores++;
                    }
                    else
                    {
                        row[bet.CategoryTitle] = bet.MovieTitle;
                    }
                }

                row[ScoresColumnName] = scores;

                dt.Rows.Add(row);
            }
            return dt;
        }

        // BindDataTableToGrid()
        protected void BindDataTableToGrid(DataView dv)
        {
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        //-------------------SORTING---------------------------//

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            var categories = betStatisticServices.GetCategories();

            DataTable dt = CreateDataTable(categories);
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
            get { return ViewState["SortExpression"] as string; }
            set { ViewState["SortExpression"] = value; }
        }

    }
}