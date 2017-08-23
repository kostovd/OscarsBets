<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WatchesMoviesStatistics.aspx.cs" Inherits="MovieScrapper.CommonPages.WatchesMoviesStatistics" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="MovieStyleSheet.css" rel="stylesheet" /> 
    <br />
    <span>All users which the movies they have watched- SQL Query- Test</span>
    <hr />
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Email" HeaderText="Email"  />
            <asp:BoundField DataField="Category" HeaderText=" Category"/>           
            <asp:BoundField DataField="Title" HeaderText="Title"  />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT	Bets.Id, AspNetUsers.Email, Categories.CategoryTtle as Category, Movies.Title FROM Bets INNER JOIN Movies ON Bets.Movie_Id = Movies.Id INNER JOIN AspNetUsers ON Bets.UserId = AspNetUsers.Id JOIN Categories ON Bets.Category_Id = Categories.Id"></asp:SqlDataSource>
</asp:Content>