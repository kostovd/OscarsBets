<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAllDBMovies.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowAllDBMovies" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="../Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/LargePoster.js"></script>
    <asp:Label ID="GreatingLabel" runat="server" Text="" CssClass="warning-left"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning-left"></asp:Label>
    <br />
    <br />
    <asp:Repeater ID="Repeater1" runat="server"
        ItemType="MovieScrapper.Entities.Movie" DataSourceID="ObjectDataSource1" OnItemCommand="Repeater1_ItemCommand">
        <HeaderTemplate>
            <div>
        </HeaderTemplate>
        <ItemTemplate>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
                    <div class=" pattern">
                        <My:MovieControl ID="MovieControl1" runat="server" Item="<%# Item %>" />
                        <div class="under-movie">
                            <asp:LinkButton ID="MarkAsWatchedButton"
                                runat="server"
                                Text=""
                                CommandName="MarkAsWatchedOrUnwatched"
                                CommandArgument='<%# Item.Id %>'
                                Enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning() %>"
                                Visible="<%#!IsGameNotStartedYet()%>">
                                <%# ChangeTextIfUserWatchedThisMovie(Item.UsersWatchedThisMovie) %>
                            </asp:LinkButton>
                            <span class="label" visible="<%#!IsGameNotStartedYet()%>">Mark as watched</span>
                        </div>
                    </div>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
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
        OnObjectCreating="ObjectDataSource1_ObjectCreating"></asp:ObjectDataSource>
</asp:Content>
