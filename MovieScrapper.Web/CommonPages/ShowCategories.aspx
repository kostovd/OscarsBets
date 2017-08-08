<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCategories.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowCategories" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="MovieStyleSheet.css" rel="stylesheet" />
        <div>
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
                ItemType="MovieScrapper.Entities.MovieCategory">
                <ItemTemplate>
                    <br />
                    <br />
                    <asp:Label ID="CategoryTtleLabel"  runat="server" style="text-transform: uppercase;font-weight:bold;" Text='<%# Item.CategoryTtle %>' />
                    <br />
                    <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Item.CategoryDescription %>' />
                    <br />

                    <asp:Repeater ID="Repeater2" runat="server"
                        ItemType="MovieScrapper.Entities.Movie" DataSource="<%# Item.Movies %>">
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
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <asp:ObjectDataSource 
                ID="ObjectDataSource1" 
                runat="server" 
                SelectMethod="GetAll"
                TypeName="MovieScrapper.Business.CategoryService">
            </asp:ObjectDataSource>
        </div>
</asp:Content>
