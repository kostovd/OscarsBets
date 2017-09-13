﻿using MovieScrapper.Business;
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
            var betStatisticServices = GetBuisnessService<IBetStatisticService>();
            var categories = betStatisticServices.GetCategories();
            var winners = betStatisticServices.GetWinners();
        
            GridView1.Columns.Clear();

            // Create
            var dt= CreateDataTable(categories, winners);

            // Fill
            dt= FillDataTable(dt);

            // Bind
            BindDataTableToGrid(dt);
        }

        // CreateDataTable()
        private DataTable CreateDataTable(string[] categories, List<Winners> winners)
        {
            DataTable dt = new DataTable();
            bool allWinnersAreSet;

                if (winners.Count == categories.Count())
                {
                    allWinnersAreSet = true;
                }
                else
                {
                    allWinnersAreSet = false;
                }

            dt.Columns.Add("Email", typeof(string));
            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = "Email";
            field.SortExpression = "Email";
            GridView1.Columns.Add(field);


            dt.Columns.Add("Scores", typeof(int));
            field = new BoundField();
            field.HeaderText = "Scores";
            field.DataField = "Scores";
            field.SortExpression = "Scores";
            if (GameIsRunning())
            {
                field.InsertVisible = false;
            }
            GridView1.Columns.Add(field);

            

            foreach (string category in categories)
            {
                dt.Columns.Add(category);

                field = new BoundField();
                if (GameIsRunning())
                {
                    field.HeaderText = "<span class='goldFont'>" + category + "</span>";
                }
                else
                {
                    if (allWinnersAreSet)
                    {
                        var winner = winners.Where(x => x.Category == category).Select(x => x.Winner).Single();
                        field.HeaderText = "<span class='goldFont'>" + category + "</span><br/><span style='color:rgb(237,192,116)'>" + winner + "</span>";
                    }
                    else
                    {
                        field.HeaderText = "<span class='goldFont'>" + category + "</span><br/><span style='font-size:10px;'> Waiting to know the winner </span>";
                    }
                }
                field.DataField = category;
                field.HtmlEncode = false;
                GridView1.Columns.Add(field);

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
                row["Email"] = user.UserEmail;

                int scores = 0;
                foreach (var bet in user.UserBets)
                {
                    //row[bet.CategoryTitle] = bet;
                    if (bet.IsRightGuess && !GameIsRunning())
                    {
                        row[bet.CategoryTitle] = bet.MovieTitle + "<span style='font-family:Wingdings;color:rgb(237,192,116); font-size:30px;'>&#67;</span>";
                        scores++;
                    }
                    else
                    {
                        row[bet.CategoryTitle] = bet.MovieTitle;
                    }
                }

                row["Scores"] = scores;

                dt.Rows.Add(row);
            }
            return dt;
        }

        // BindDataTableToGrid()
        protected void BindDataTableToGrid(DataTable dt)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }



        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {       

        }

        private bool GameIsRunning()
        {
            var gamePropertyService = GetBuisnessService<IGamePropertyService>();

            if (gamePropertyService.IsGameStopped() == false)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewInit();
            ((DataTable)GridView1.DataSource).DefaultView.Sort = e.SortExpression;
        }
    }
}