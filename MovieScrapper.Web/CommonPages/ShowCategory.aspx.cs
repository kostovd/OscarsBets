using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class ShowCategory : BasePage
    {
        private const string UserColumnName = "Email";

        #region SortDirectionProperties

        private SortDirection MoviesScoresGridViewSortDirection
        {
            get
            {
                if (ViewState["MoviesScoresSortDirection"] == null)
                    ViewState["MoviesScoresSortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["MoviesScoresSortDirection"];
            }

            set { ViewState["MoviesScoresSortDirection"] = value; }
        }

        private SortDirection UserVotesGridViewSortDirection
        {
            get
            {
                if (ViewState["UserVotesSortDirection"] == null)
                    ViewState["UserVotesSortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["UserVotesSortDirection"];
            }

            set { ViewState["UserVotesSortDirection"] = value; }
        }

        private SortDirection UserWatchedGridViewSortDirection
        {
            get
            {
                if (ViewState["UserWatchedSortDirection"] == null)
                    ViewState["UserWatchedSortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["UserWatchedSortDirection"];
            }

            set { ViewState["UserWatchedSortDirection"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
                DataBind();
            }

            if (!CheckIfTheUserIsLogged())
            {
                GreatingLabel.Text = "You must be logged in to bet!";
            }
            else
            {
                GreatingLabel.CssClass = "hidden";
            }

            if (GetBuisnessService<IGamePropertyService>().IsGameNotStartedYet())
            {
                WarningLabel.CssClass = WarningLabel.CssClass.Replace("warning", "");
                GreatingLabel.CssClass = "hidden";
                WarningLabel.CssClass = "hidden";
            }
        }
             
        private void BindCategory()
        {
            Category currentCategory = GetCurrentCategory();

            NominationsRepeater.DataSource = currentCategory.Nominations;

            CategoryTtleLabel.Text = currentCategory.CategoryTtle;
            CategoryTtleLabel.ToolTip = currentCategory.CategoryDescription;

            MoviesScoresGridView.DataSource = currentCategory.Nominations;

            CreateAndFillUserVotesDataTable(currentCategory);
            CreateAndFillUserWatchedDataTable(currentCategory);
        }

        private Category GetCurrentCategory()
        {
            int.TryParse(Request.QueryString["ID"], out int id);
            return GetBuisnessService<ICategoryService>().GetCategory(id);
        }

        public bool IsGameNotStartedYet()
        {
            return GetBuisnessService<IGamePropertyService>().IsGameNotStartedYet();
        }

        public bool IsGameRunning()
        {
            return !GetBuisnessService<IGamePropertyService>().IsGameStopped();
        }

        protected bool CheckIfTheUserIsLogged()
        {
            return User.Identity.IsAuthenticated;
        }

        public string BuildPosterUrl(string path)
        {
            return "https://image.tmdb.org/t/p/w92" + path;
        }

        #region CreateAndInitGridViews

        private void CreateAndFillUserVotesDataTable(Category currentCategory)
        {
            var moviesFromCategory = currentCategory.Nominations.Select(n => n.Movie).ToList();

            if (!IsPostBack)
            {
                InitUserVotesGridViewColumns(moviesFromCategory);
            }

            var dataTable = CreateUserMoviesDataTable(moviesFromCategory);
            dataTable = FillVotesDataTable(dataTable, currentCategory.Nominations.Select(n => n.Movie.Title).ToList(), currentCategory);
            DataView sortedView = DefaultUserVotesTableSort(dataTable, UserColumnName, UserVotesGridViewSortDirection);
            UserVotesGridView.DataSource = sortedView;
        }

        private void CreateAndFillUserWatchedDataTable(Category currentCategory)
        {
            var moviesFromCategory = currentCategory.Nominations.Select(n => n.Movie).ToList();

            if (!IsPostBack)
            {
                InitUserWatchedGridViewColumns(moviesFromCategory);
            }

            var dataTable = CreateUserMoviesDataTable(moviesFromCategory);
            dataTable = FillWatchedDataTable(dataTable, currentCategory.Nominations.Select(n => n.Movie.Title).ToList(), currentCategory);
            DataView sortedView = DefaultUserVotesTableSort(dataTable, UserColumnName, UserWatchedGridViewSortDirection);
            UserWatchedGridView.DataSource = sortedView;
        }

        private void InitUserVotesGridViewColumns(IList<Movie> movies)
        {
            movies = movies.OrderBy(m => m.Title).ToList();

            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            UserVotesGridView.Columns.Add(field);

            foreach (var movie in movies)
            {
                field = new BoundField();
                field.HeaderStyle.Width = Unit.Pixel(46);
                field.HeaderImageUrl = BuildPosterUrl(movie.PosterPath);
                field.DataField = movie.Title;
                field.HtmlEncode = false;
                UserVotesGridView.Columns.Add(field);
            }
        }

        private void InitUserWatchedGridViewColumns(IList<Movie> movies)
        {
            movies = movies.OrderBy(m => m.Title).ToList();

            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            UserWatchedGridView.Columns.Add(field);

            foreach (var movie in movies)
            {
                field = new BoundField();
                field.HeaderStyle.Width = Unit.Pixel(46);
                field.HeaderImageUrl = BuildPosterUrl(movie.PosterPath);
                field.DataField = movie.Title;
                field.HtmlEncode = false;
                UserWatchedGridView.Columns.Add(field);
            }
        }

        private DataTable CreateUserMoviesDataTable(IList<Movie> movies)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(UserColumnName, typeof(string));

            foreach (var movie in movies)
            {
                dataTable.Columns.Add(movie.Title);
            }

            return dataTable;
        }

        #endregion
      
        #region FillGridViews

        private DataTable FillVotesDataTable(DataTable dataTable, List<string> titles, Category currentCategory)
        {
            var betStatisticService = GetBuisnessService<IBetStatisticService>();
            var users = betStatisticService.GetData();

            foreach (var user in users)
            {
                var row = dataTable.NewRow();
                row[UserColumnName] = user.UserEmail;

                int scores = 0;

                foreach (var bet in user.UserBets.Where(b => b.CategoryTitle == currentCategory.CategoryTtle))
                {
                    row[bet.MovieTitle] = "<span class='glyphicon glyphicon-ok'></span>";
                    scores++;
                }

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private DataTable FillWatchedDataTable(DataTable dataTable, List<string> titles, Category currentCategory)
        {
            var watchedMoviesStatistic = GetBuisnessService<IWatcheMoviesStatisticService>();
            var users = watchedMoviesStatistic.GetData();

            foreach (var user in users)
            {
                var row = dataTable.NewRow();
                row[UserColumnName] = user.UserEmail;

                int scores = 0;

                foreach (var movie in user.MovieTitles.Where(m => currentCategory.Nominations.Any(n => n.Movie.Title.Contains(m))))
                {
                    row[movie] = "<span class='glyphicon glyphicon-ok'></span>";
                    scores++;
                }

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        #endregion

        #region SortingEventsAndMethods

        protected void MoviesScoresGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            MoviesScoresGridViewSortDirection = MoviesScoresGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            if (MoviesScoresGridViewSortDirection == SortDirection.Ascending)
            {
                MoviesScoresGridView.DataSource = e.SortExpression == "Movie" ?
                    currentCategory.Nominations.OrderBy(n => n.Movie.Title) : currentCategory.Nominations.OrderBy(n => n.Bets.Count);
            }
            else
            {
                MoviesScoresGridView.DataSource = e.SortExpression == "Movie" ?
                    currentCategory.Nominations.OrderByDescending(n => n.Movie.Title) : currentCategory.Nominations.OrderByDescending(n => n.Bets.Count);
            }

            MoviesScoresGridView.DataBind();

            NominationsRepeater.DataSource = currentCategory.Nominations;
            NominationsRepeater.DataBind();

            SetSortingArrows(MoviesScoresGridView, MoviesScoresGridViewSortDirection, e.SortExpression);
        }

        protected void UserVotesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            UserVotesGridViewSortDirection = UserVotesGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            CreateAndFillUserVotesDataTable(currentCategory);
            UserVotesGridView.DataBind();

            NominationsRepeater.DataSource = currentCategory.Nominations;
            NominationsRepeater.DataBind();

            SetSortingArrows(UserVotesGridView, UserVotesGridViewSortDirection, e.SortExpression);
        }

        protected void UserWatchedGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            UserWatchedGridViewSortDirection = UserWatchedGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            CreateAndFillUserWatchedDataTable(currentCategory);
            UserWatchedGridView.DataBind();

            NominationsRepeater.DataSource = currentCategory.Nominations;
            NominationsRepeater.DataBind();

            SetSortingArrows(UserWatchedGridView, UserWatchedGridViewSortDirection, e.SortExpression);
        }

        private void SetSortingArrows(GridView gridView, SortDirection gridViewSortDirection, string sortingExpression)
        {
            if (gridViewSortDirection == SortDirection.Ascending)
            {
                gridView.HeaderRow.Cells[GetColumnIndex(sortingExpression, gridView)].CssClass = "sortasc";
            }
            else
            {
                gridView.HeaderRow.Cells[GetColumnIndex(sortingExpression, gridView)].CssClass = "sortdesc";
            }
        }

        private int GetColumnIndex(string SortExpression, GridView gridViewToSearch)
        {
            int columnIndex = 0;

            foreach (DataControlField c in gridViewToSearch.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;

                columnIndex++;
            }

            return columnIndex;
        }

        private DataView DefaultUserVotesTableSort(DataTable dataTable, string sortExpresion, SortDirection gridViewSortDirection)
        {
            DataView dataView = new DataView(dataTable);

            if (gridViewSortDirection == SortDirection.Ascending)
            {
                dataView.Sort = sortExpresion + " ASC";
            }
            else
            {
                dataView.Sort = sortExpresion + " DESC";
            }

            return dataView;
        }

        #endregion

        protected void NominationsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsBetted")
            {
                if (IsGameRunning())
                {
                    var userId = User.Identity.Name;
                    var nominationId = int.Parse(e.CommandArgument.ToString());

                    var betService = GetBuisnessService<IBetService>();
                    betService.MakeBetEntity(userId, nominationId);

                    BindCategory();
                    DataBind();
                }
                else
                {
                    Response.Redirect("ShowCategories.aspx");
                }
            }
        }

        protected void NominationsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BetUpdate();
        }        

        protected string ChangeTextIfUserBettedOnThisNomination(ICollection<Bet> nominationBets)
        {
            string currentUserId = User.Identity.Name;

            if (nominationBets.Any(x => x.UserId == currentUserId))
            {
                return "<span class='check-button glyphicon glyphicon-check'></span>";
            }
            else
            {
                return "<span class='check-button glyphicon glyphicon-unchecked'></span>";
            }
        }

        protected string CheckIfWinnerImage(Nomination nomination)
        {
            return nomination.IsWinner && !IsGameRunning() ? "/images/Oscar_logo.png" : "";
        }

        private void BetUpdate()
        {
            var currentUsereId = User.Identity.Name;

            var categories = GetBuisnessService<ICategoryService>().GetAll();
            int categoryCount = categories.Count();

            var bets = categories.SelectMany(x => x.Nominations).SelectMany(x => x.Bets).Where(x => x.UserId == currentUsereId).ToList();

            int missedCategories = categoryCount - bets.Count;

            var winners = categories.SelectMany(c => c.Nominations).Where(x => x.IsWinner).ToList();
            bool winnersAreSet = (winners.Count == categoryCount);

            int counter = bets.Count(x => x.Nomination.IsWinner);

            if (CheckIfTheUserIsLogged() == true && IsGameRunning() == true)
            {
                if (missedCategories > 0)
                {
                    if (missedCategories == 1)
                    {
                        WarningLabel.Text = "Here you can bet in " + categoryCount + " different categories. " +
                            "You have " + (missedCategories) + " more category to bet.";
                    }
                    else
                    {
                        WarningLabel.Text = "Here you can bet in " + categoryCount + " different categories. " +
                            "You have " + (missedCategories) + " more categories to bet.";
                    }
                }
                else
                {
                    WarningLabel.CssClass = "goldBorder";
                    WarningLabel.Text = "Congratulations! You betted in all the " + categoryCount + " categories.";
                }
            }
            else
            {
                WarningLabel.CssClass = "hidden";
            }

            //////////////// Show right suggestions statistic label /////////////////////

            if (CheckIfTheUserIsLogged() == true && IsGameRunning() == false)
            {
                if (winnersAreSet)
                {
                    if (counter > 0)
                    {
                        if (counter == categoryCount)
                        {
                            WinnerLabel.Text = "Yayyyyyyyyy! You guessed right in all the categories!";
                            WinnerLabel.CssClass = "goldBorder";
                        }
                        else if (counter == 1)
                        {
                            WinnerLabel.Text = "Congratulations! You guessed right in " + counter + " category.";
                        }
                        else
                        {
                            WinnerLabel.Text = "Congratulations! You guessed right in " + counter + " categories.";
                        }
                    }
                    else
                    {
                        WinnerLabel.Text = "Sorry, you don't have right suggestions";
                    }
                }
                else
                {
                    WinnerLabel.Text = "The game is stopped, but we are waiting to know the winners.";
                }
            }
            else
            {
                WinnerLabel.CssClass = "hidden";
            }
        }
    }
}