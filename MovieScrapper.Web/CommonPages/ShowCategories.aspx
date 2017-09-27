<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCategories.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowCategories" MasterPageFile="~/Site.Master" %>
<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/LargePoster.js"></script>
    <asp:Label ID="GreatingLabel" runat="server" CssClass="warning" ></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning" ></asp:Label>
    <asp:Label ID="WinnerLabel" runat="server" CssClass="greenBorder"></asp:Label>  
    <div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
            ItemType="MovieScrapper.Entities.Category">
            <ItemTemplate>
                <br />                
                <asp:Label ID="CategoryTtleLabel" cssClass= "categoryTitle" runat="server" ToolTip="<%# Item.CategoryDescription %>" Text='<%# Item.CategoryTtle %>' />
                <hr />
                <asp:Repeater ID="Repeater2" runat="server"
                    ItemType="MovieScrapper.Entities.Movie" DataSource="<%# Item.Movies %>" OnItemCommand="Repeater2_ItemCommand">
                    <HeaderTemplate>
                        <div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class=" pattern">
                            <My:MovieControl ID="MovieControl1" runat="server" Item="<%# Item %>" />    
                                <div class="under-movie">
                                      <img class="winnerLogo" src="<%# CheckIfWinnerImage(DataBinder.Eval(Container.Parent.Parent, "DataItem.Winner.Id"), Item.Id) %>" />
                                      <asp:LinkButton ID="MarkAsBettedButton"
                                        runat="server"
                                        Text=""
                                        CommandName="MarkAsBetted"
                                        CommandArgument='<%# string.Format("{0}|{1}", Item.Id , DataBinder.Eval(Container.Parent.Parent, "DataItem.Id")) %>'                       
                                        enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning()%>"
                                        visible="<%#!IsGameNotStartedYet()%>">
                                          <%# ChangeTextIfUserBettedOnThisMovie((ICollection<MovieScrapper.Entities.Bet>)DataBinder.Eval(Container.Parent.Parent, "DataItem.Bets"), Item.Id) %>
                                      </asp:LinkButton>
                                        
                                    <span class="label" visible="<%#!IsGameNotStartedYet()%>">Bet for this movie!</span>                                       
                                   </div>
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
            TypeName="MovieScrapper.Business.Interfaces.ICategoryService" 
            OnObjectCreating="ObjectDataSource1_ObjectCreating"
            OnSelected="ObjectDataSource1_Selected"></asp:ObjectDataSource>
    </div>
</asp:Content>
