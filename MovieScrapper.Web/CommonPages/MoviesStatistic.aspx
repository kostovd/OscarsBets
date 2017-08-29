<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesStatistic.aspx.cs" Inherits="MovieScrapper.CommonPages.MoviesStatistic" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="StatisticsStyleSheet.css" rel="stylesheet" /> 
    <br />   
    <hr />
    <asp:GridView ID="GridView1" CssClass="grid" runat="server" AutoGenerateColumns = "false" OnRowDataBound = "OnRowDataBound"></asp:GridView>
    
</asp:Content>
