<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BetsStatistics.aspx.cs" Inherits="MovieScrapper.CommonPages.BetsStatistics" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="StatisticsStyleSheet.css" rel="stylesheet" />    
    
    <asp:Label ID="Label1" runat="server"></asp:Label>
   
    <asp:GridView ID="GridView1" CssClass="grid" runat="server" AutoGenerateColumns = "false" OnRowDataBound = "OnRowDataBound"></asp:GridView>
    
</asp:Content>
