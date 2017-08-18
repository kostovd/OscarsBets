<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesStatistic.aspx.cs" Inherits="MovieScrapper.CommonPages.MoviesStatistic" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="MovieStyleSheet.css" rel="stylesheet" /> 
    <br />
    <span>All users with the movies they have watched</span>
    <hr />
    <asp:GridView ID="GridView1" runat="server" 
        DataSourceID="ObjectDataSource1"
        ItemType="MovieScrapper.Data.ViewModelsRepository">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id"  />
             <asp:BoundField DataField="Title" HeaderText="Title"  />
            <%--<asp:BoundField DataField="Email" HeaderText="Email"  />--%>
                      
        </Columns>

    </asp:GridView>
    <asp:ObjectDataSource
            ID="ObjectDataSource1"
            runat="server"
            SelectMethod="GetData"
            TypeName="MovieScrapper.Business.StatisticService" >

    </asp:ObjectDataSource>

</asp:Content>
