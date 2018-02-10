<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="MovieScrapper.MovieDetails" Async="true" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/MovieDetail.css" rel="stylesheet" type="text/css" />
    <asp:DetailsView ID="DetailsView1" runat="server" Height="157px" Width="279px" AutoGenerateRows="false" BorderStyle="None" GridLines="None">
        <Fields>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="movieItem">
                        <div id="info">
                            <a target="_newtab" href="<%# BuildMovieUrl((int)Eval("Id")) %>">
                                <div id="title"><%# Eval("Title") %> (<%# DisplayYear((string)Eval("ReleaseDate")) %>)</div>
                            </a>
                            </br></br>
                        <div id="overview">
                            <%# Eval("Overview") %>
                        </div>
                            <br></br>
                            <a id="backlLink" runat="server" href="<%# BuildBackUrl() %>"><span class='glyphicon glyphicon-backward'></span></a>
                            </br></br>
                        </div>
                        <img class="poster" src="<%# BuildPosterUrl((string)Eval("PosterPath")) %>" />
                        <br />
                        <br />
                        <br />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

        </Fields>
    </asp:DetailsView>

    <asp:Panel ID="PnlAddMovieButton" runat="server">
        <asp:Button ID="AddMovieToCategoryButton" runat="server" Height="40px" Text="Add this movie to the selected category " Width="500px" OnClick="AddMovieToCategoryButton_Click" />
    </asp:Panel>

    <div class="container">
        <div class="row">
            <div class="col-sm-4">
                <asp:Repeater ID="RptCast" runat="server" ItemType="MovieScrapper.Entities.MovieCredit">
                    <HeaderTemplate>
                        <h2>Cast</h2>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="well well-sm">
                            <asp:CheckBox ID="CbNominated" Visible="<%# IsCheckBoxNominationVisible %>" runat="server" Text="Nominated" />
                            <asp:HiddenField ID="HfCreditId" Value="<%# Item.Id %>" runat="server" />
                            <div class="person_profile">
                                <div runat="server" class="glyphicon glyphicon-user profile_image no_image" visible="<%# !HasProfileImage(Item.ProfilePath) %>" />
                                <img runat="server" class="profile_image" visible="<%# HasProfileImage(Item.ProfilePath) %>" src="<%# BuildProfileImageUrl(Item.ProfilePath) %>" />
                                <div class="person_info">
                                    <p class="h5">
                                        <a target="_newtab" href="<%# BuildPersonUrl(Item.PersonId) %>"><%# Item.Name %></a>
                                        <br />
                                        <span class="text-muted"><%# Item.Role %></span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="col-sm-4">
                <asp:Repeater ID="RptCrew" runat="server" ItemType="MovieScrapper.Entities.MovieCredit">
                    <HeaderTemplate>
                        <h2>Crew</h2>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="well well-sm">
                            <asp:CheckBox ID="CbNominated" Visible="<%# IsCheckBoxNominationVisible %>" runat="server" Text="Nominated" />
                            <asp:HiddenField ID="HfCreditId" Value="<%# Item.Id %>" runat="server" />
                            <div class="person_profile">
                                <div runat="server" class="glyphicon glyphicon-user profile_image no_image" visible="<%# !HasProfileImage(Item.ProfilePath) %>" />
                                <img runat="server" class="profile_image" visible="<%# HasProfileImage(Item.ProfilePath) %>" src="<%# BuildProfileImageUrl(Item.ProfilePath) %>" />
                                <div class="person_info">
                                    <p class="h5">
                                        <a target="_newtab" href="<%# BuildPersonUrl(Item.PersonId) %>"><%# Item.Name %></a>
                                        <br />
                                        <span class="text-muted"><%# Item.Role %></span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
