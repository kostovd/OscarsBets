<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAllDBMovies.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowAllDBMovies" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="MovieStyleSheet.css" rel="stylesheet" />
    <asp:Repeater ID="Repeater1" runat="server"
                        ItemType="MovieScrapper.Entities.Movie" DataSourceID="ObjectDataSource1">
                        <HeaderTemplate>
                            <div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="movieItem">
                                <div id="title">
                                    <div class="items">
                                      
                                       <a href="<%# BuildUrl(Item.Id) %>"><%# Item.Title %> (<%# DisplayYear(Item.ReleaseDate) %>)</a>
                                    </div>
                                </div>
                                <img id="poster" src="<%# BuildPosterUrl(Item.PosterPath) %>" class="auto-style2" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
            <asp:ObjectDataSource 
                ID="ObjectDataSource1" 
                runat="server" 
                SelectMethod="GetAllMovies"
                TypeName="MovieScrapper.Business.CategoryService">
            </asp:ObjectDataSource>
</asp:Content>