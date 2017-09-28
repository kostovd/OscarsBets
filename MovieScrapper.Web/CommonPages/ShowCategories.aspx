<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCategories.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowCategories" MasterPageFile="~/Site.Master" ClientIdMode = "AutoId"%>

<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/LargePoster.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= this.ResolveUrl("~/images/DoubleRing.gif")%>" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
     <style>
            .loading-panel {
                background: rgba(0, 0, 0, 0.2) none repeat scroll 0 0;
                position: relative;
                width: 100%;
            }

            .loading-container {
                background: rgba(49, 133, 156, 0.4) none repeat scroll 0 0;
                color: #fff;
                font-size: 90px;
                height: 100%;
                left: 0;
                padding-top: 15%;
                position: fixed;
                text-align: center;
                top: 0;
                width: 100%;
                z-index: 999999;
            }
        </style>
    <asp:Label ID="GreatingLabel" runat="server" CssClass="warning"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
    <asp:Label ID="WinnerLabel" runat="server" CssClass="greenBorder"></asp:Label>
    <div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
            ItemType="MovieScrapper.Entities.Category">
            <ItemTemplate>
                <br />
                <asp:Label ID="CategoryTtleLabel" CssClass="categoryTitle" runat="server" ToolTip="<%# Item.CategoryDescription %>" Text='<%# Item.CategoryTtle %>' />
                <hr />
                <asp:UpdatePanel ID="UpdatePanel2"  runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="Repeater2" runat="server"
                            ItemType="MovieScrapper.Entities.Movie" DataSource="<%# ((MovieScrapper.Entities.Category)((IDataItemContainer)Container).DataItem).Movies %>" OnItemCommand="Repeater2_ItemCommand">
                            <HeaderTemplate>
                                <div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class=" pattern">

                                    <My:MovieControl ID="MovieControl1" runat="server" Item="<%# ((MovieScrapper.Entities.Movie)((IDataItemContainer)Container).DataItem) %>" />
                                    <div class="under-movie">
                                        <img class="winnerLogo" src="<%# CheckIfWinnerImage((int?)DataBinder.Eval(Container.Parent.Parent.Parent.Parent, "DataItem.Winner.Id"), Item.Id) %>" />
                                        <asp:LinkButton ID="MarkAsBettedButton"
                                            runat="server"
                                            Text=""
                                            CommandName="MarkAsBetted"
                                            CommandArgument='<%# string.Format("{0}|{1}", Item.Id , DataBinder.Eval(Container.Parent.Parent.Parent.Parent, "DataItem.Id")) %>'
                                            Enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning()%>"
                                            Visible="<%#!IsGameNotStartedYet()%>">
                                          <%# ChangeTextIfUserBettedOnThisMovie((ICollection<MovieScrapper.Entities.Bet>)DataBinder.Eval(Container.Parent.Parent.Parent.Parent, "DataItem.Bets"), Item.Id) %>
                                        </asp:LinkButton>

                                        <span class="label" visible="<%#!IsGameNotStartedYet()%>">Bet for this movie!</span>
                                    </div>

                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>

                    </ContentTemplate>
                </asp:UpdatePanel>
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
