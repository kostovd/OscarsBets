<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCategories.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowCategories" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="MovieStyleSheet.css" rel="stylesheet" />
    <asp:Label ID="GreatingLabel" runat="server" CssClass="warning"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
    <asp:Label ID="WinnerLabel" runat="server" CssClass="greenBorder"></asp:Label>  
    <div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
            ItemType="MovieScrapper.Entities.Category">
            <ItemTemplate>
                <br />                
                <asp:Label ID="CategoryTtleLabel" class="categoryTtleLabel" runat="server" ToolTip="<%# Item.CategoryDescription %>" Text='<%# Item.CategoryTtle %>' />
                <hr />
                <asp:Repeater ID="Repeater2" runat="server"
                    ItemType="MovieScrapper.Entities.Movie" DataSource="<%# Item.Movies %>" OnItemCommand="Repeater2_ItemCommand">
                    <HeaderTemplate>
                        <div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div  class ="movieItem <%#  CheckIfWinner(DataBinder.Eval(Container.Parent.Parent, "DataItem.Winner.Id"), Item.Id)  %>"  >
                            <div class="title">
                                <div class="items " >                                   
                                    <img class="winnerLogo" src="<%# CheckIfWinnerImage(DataBinder.Eval(Container.Parent.Parent, "DataItem.Winner.Id"), Item.Id) %>" />
                                    <br />
                                    <a href="<%# BuildUrl(Item.Id) %>" title="<%# Item.Overview %>"><%# Item.Title %> (<%# DisplayYear(Item.ReleaseDate) %>)</a>                                   
                                    <hr />
                                    <a href="<%# BuildImdbUrl(Item.ImdbId) %>" target="_newtab" title="See the info in IMDB"><img class="imdb" src="/imdb.svg" /> </a>
                                    <asp:Button ID="MarkAsBettedButton"
                                        runat="server"
                                        CssClass="items checkButton"
                                        Text='<%# ChangeTextIfUserBettedOnThisMovie((ICollection<MovieScrapper.Entities.Bet>)DataBinder.Eval(Container.Parent.Parent, "DataItem.Bets"), Item.Id) %>'
                                        CommandName="MarkAsBetted"
                                        CommandArgument='<%# string.Format("{0}|{1}", Item.Id , DataBinder.Eval(Container.Parent.Parent, "DataItem.Id")) %>'
                                        title="Bet that the movie will be the winer in this category"
                                        enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning()%>"
                                        />
                                </div>
                            </div>
                            
                            <img class="poster" src="<%# BuildPosterUrl(Item.PosterPath) %>" />
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
            TypeName="MovieScrapper.Business.CategoryService" 
            OnSelected="ObjectDataSource1_Selected"></asp:ObjectDataSource>
    </div>
</asp:Content>
