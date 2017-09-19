<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="MovieScrapper.TestPage" MasterPageFile="~/Site.Master" %>
<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="CommonPages/MovieStyleSheet.css" rel="stylesheet" />    
    <asp:Label ID="GreatingLabel" runat="server" Text="" CssClass="warning-left"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning-left" ></asp:Label>
    <br /> 
    
    <asp:Repeater ID="Repeater1" runat="server"
                        ItemType="MovieScrapper.Entities.Movie" DataSourceID="ObjectDataSource1">
                        <HeaderTemplate>
                            <div>
                        </HeaderTemplate>
                        <ItemTemplate>                       
                            <My:MovieControl ID="MovieControl1" runat="server" Item="<%# Item %>" />
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource 
                ID="ObjectDataSource1" 
                runat="server" 
                SelectMethod="GetAllMovies"
                OnSelected="ObjectDataSource1_Selected"
                TypeName="MovieScrapper.Business.Interfaces.IMovieService" 
                OnObjectCreating="ObjectDataSource1_ObjectCreating">
            </asp:ObjectDataSource>
    </asp:Content>
