<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCategories.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowCategories" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="MovieStyleSheet.css" rel="stylesheet" />
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="header"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <hr />
    <div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
            ItemType="MovieScrapper.Entities.Category">
            <ItemTemplate>
                <br />
                <br />
                <asp:Label ID="CategoryTtleLabel" runat="server" Style="text-transform: uppercase; font-weight: bold;" Text='<%# Item.CategoryTtle %>' />
                <br />
                <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Item.CategoryDescription %>' />
                <br />

                <asp:Repeater ID="Repeater2" runat="server"
                    ItemType="MovieScrapper.Entities.Movie" DataSource="<%# Item.Movies %>" OnItemCommand="Repeater2_ItemCommand">
                    <HeaderTemplate>
                        <div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div id="movieItem">
                            <div id="title">
                                <div class="items">
                                    <a href="<%# BuildUrl(Item.Id) %>"><%# Item.Title %> (<%# DisplayYear(Item.ReleaseDate) %>)</a>
                                    <asp:Button ID="MarkAsBettedButton"
                                        runat="server"
                                        CssClass="items checkButton"
                                        Text='<%# ChangeTextIfUserBettedOnThisMovie((ICollection<MovieScrapper.Entities.Bet>)DataBinder.Eval(Container.Parent.Parent, "DataItem.Bets"), Item.Id) %>'
                                        CommandName="MarkAsBetted"
                                        CommandArgument='<%# string.Format("{0}|{1}",Item.Id , DataBinder.Eval(Container.Parent.Parent, "DataItem.Id")) %>' />
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
            TypeName="MovieScrapper.Business.CategoryService"></asp:ObjectDataSource>
    </div>
</asp:Content>
