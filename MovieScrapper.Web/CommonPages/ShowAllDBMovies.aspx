<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAllDBMovies.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowAllDBMovies" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="../Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/LargePoster.js"></script>

    <asp:Label ID="GreatingLabel" runat="server" Text="" CssClass="warning-left"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning-left"></asp:Label>
    <br />
    <br />

    <asp:DropDownList 
        ID="DdlFilters"
        runat="server"  
        AutoPostBack="true"
        Height="40px" 
        Width="180px"
        CssClass="dropdown-filter">
        <asp:ListItem Selected="True" Value="0">By Name</asp:ListItem>
        <asp:ListItem Value="1">By Nominations</asp:ListItem>
        <asp:ListItem Value="2">By Proxiad Popularity</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />

    <asp:Repeater ID="Repeater1" runat="server"
        ItemType="MovieScrapper.Entities.Movie" DataSourceID="ObjectDataSource1" OnItemCommand="Repeater1_ItemCommand">
        <HeaderTemplate>
            <div>
        </HeaderTemplate>
        <ItemTemplate>
            <div class=" pattern">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <My:MovieControl ID="MovieControl1" runat="server" Item="<%# ((MovieScrapper.Entities.Movie)((IDataItemContainer)Container).DataItem) %>" />
                        <div class="under-movie">
                            <asp:LinkButton ID="MarkAsWatchedButton"
                                runat="server"
                                Text=""
                                ClientIDMode="AutoID"
                                CommandName="MarkAsWatchedOrUnwatched"
                                CommandArgument="<%#((MovieScrapper.Entities.Movie)((IDataItemContainer)Container).DataItem).Id%>"
                                Enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning() %>"
                                Visible="<%#!IsGameNotStartedYet()%>">
                                <%# ChangeTextIfUserWatchedThisMovie(((MovieScrapper.Entities.Movie)((IDataItemContainer)Container).DataItem).UsersWatchedThisMovie) %>
                            </asp:LinkButton>
                            <span class="label leftLabel" visible="<%#!IsGameNotStartedYet()%>">Mark as watched</span>
                            <span class="label rightLabel"><%# GetNominaionsInfo(((MovieScrapper.Entities.Movie)((IDataItemContainer)Container).DataItem)) %></span>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource
        ID="ObjectDataSource1"
        runat="server"
        SelectMethod="GetAllMoviesByCriteria"
        OnSelected="ObjectDataSource1_Selected"
        TypeName="MovieScrapper.Business.Interfaces.IMovieService"
        OnObjectCreating="ObjectDataSource1_ObjectCreating">
        <SelectParameters>
            <asp:ControlParameter ControlID="DdlFilters" DefaultValue="0" Name="filter" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
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
</asp:Content>
