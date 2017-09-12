<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BetsStatistics.aspx.cs" Inherits="MovieScrapper.CommonPages.BetsStatistics" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="StatisticsStyleSheet.css" rel="stylesheet" />    
    
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <a class="link" href="MoviesStatistic.aspx">See also the statistic for watched movies</a>
    <hr />
    <asp:GridView ID="GridView1" CssClass="grid" runat="server" AutoGenerateColumns = "true" OnRowDataBound = "OnRowDataBound"></asp:GridView>
    
</asp:Content>
