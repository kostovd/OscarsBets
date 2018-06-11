using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieScrapper.CommonPages
{
    public partial class ShowCategory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCategory();
            DataBind();

            var gamePropertyService = GetBuisnessService<IGamePropertyService>();
            if (!User.Identity.IsAuthenticated)
            {
                GreatingLabel.Text = "You must be logged in to bet!";
            }
            else
            {
                GreatingLabel.CssClass = "hidden";
            }

            if (gamePropertyService.IsGameNotStartedYet())
            {
                WarningLabel.CssClass = WarningLabel.CssClass.Replace("warning", "");
                GreatingLabel.CssClass = "hidden";
                WarningLabel.CssClass = "hidden";
            }
        }

        public bool IsGameRunning()
        {
            var gamePropertyService = GetBuisnessService<IGamePropertyService>();
            return !gamePropertyService.IsGameStopped();
        }

        private void BindCategory()
        {
            int.TryParse(Request.QueryString["ID"], out int id);
            Category currentCategory = GetBuisnessService<ICategoryService>().GetCategory(id);
            Repeater2.DataSource = currentCategory.Nominations;

            CategoryTtleLabel.Text = currentCategory.CategoryTtle;
            CategoryTtleLabel.ToolTip = currentCategory.CategoryDescription;

            GridView1.DataSource = currentCategory.Nominations;
        }

        public bool IsGameNotStartedYet()
        {
            var gamePropertyService = GetBuisnessService<IGamePropertyService>();

            return gamePropertyService.IsGameNotStartedYet();
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    Response.Redirect("ShowCategories.aspx");
                }
            }
        }

        protected bool CheckIfTheUserIsLogged()
        {
            return User.Identity.IsAuthenticated;
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
            return nomination.IsWinner && !IsGameRunning() ?
                    "/images/Oscar_logo.png" :
                    "";
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

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BetUpdate();
        }       
    }
}