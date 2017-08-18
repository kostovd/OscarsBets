<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WatchesMoviesStatistics.aspx.cs" Inherits="MovieScrapper.CommonPages.WatchesMoviesStatistics" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="MovieStyleSheet.css" rel="stylesheet" /> 
    <br />
    <span>All users which the movies they have watched- SQL Query- Test</span>
    <hr />
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <%--<asp:BoundField DataField="Watched_UserId" HeaderText="Watched_UserId" SortExpression="Watched_UserId" />--%>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Movies.Id, Movies.Title, WatchedMovies.Watched_UserId, AspNetUsers.Email FROM Movies LEFT OUTER JOIN WatchedMovies ON Movies.Id = WatchedMovies.Movie_Id LEFT OUTER JOIN AspNetUsers ON WatchedMovies.Watched_UserId = AspNetUsers.Id"></asp:SqlDataSource>
</asp:Content>