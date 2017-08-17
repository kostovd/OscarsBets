<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WatchesMoviesStatistics.aspx.cs" Inherits="MovieScrapper.CommonPages.WatchesMoviesStatistics" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="MovieStyleSheet.css" rel="stylesheet" />    
    <asp:GridView ID="GridView1" runat="server"  DataSourceID="ObjectDataSource1" AutoGenerateColumns="false" ItemType="MovieScrapper.Business.ViewModels.WatchedMovies">
        <Columns>             
            <asp:BoundField DataField="Title" HeaderText="Movie"  />
                     
        </Columns>
        
    </asp:GridView>
    <asp:ObjectDataSource 
                ID="ObjectDataSource1" 
                runat="server" 
                SelectMethod="GetAllMovies" 
                TypeName="MovieScrapper.Business.CategoryService">
                  
     </asp:ObjectDataSource>
</asp:Content>