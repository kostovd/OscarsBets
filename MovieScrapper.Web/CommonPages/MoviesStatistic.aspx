<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesStatistic.aspx.cs" Inherits="MovieScrapper.CommonPages.MoviesStatistic" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="../Content/StatisticsStyleSheet.css" rel="stylesheet" type="text/css" /> 
    <ul class="nav nav-tabs">
        <li><a href="BetsStatistics.aspx">Predictions</a></li>
        <li class="active"><a href="MoviesStatistic.aspx">Watched movies</a></li>
    </ul>
    <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="None" runat="server" AutoGenerateColumns = "false" AllowSorting="true" OnSorting="GridView1_Sorting" >
    </asp:GridView>  
</asp:Content>