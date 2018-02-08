<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="MovieScrapper.MovieDetails" Async="true" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/MovieDetail.css" rel="stylesheet" type="text/css" />
    <asp:DetailsView ID="DetailsView1" runat="server" Height="157px" Width="279px" AutoGenerateRows="false" BorderStyle="None" GridLines="None">
        <Fields>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="movieItem">
                        <div id="info">
                            <div id="title"><%# Eval("Title") %> (<%# DisplayYear((string)Eval("ReleaseDate")) %>)</div>
                            </br></br>
                        <div id="overview">
                            <%# Eval("Overview") %>
                        </div>
                            <br></br>
                            <a id="backlLink" runat="server" href="<%# BuildBackUrl() %>"><span class='glyphicon glyphicon-backward'></span></a>
                            </br></br>
                        </div>
                        <img class="poster" src="<%# BuildPosterUrl((string)Eval("PosterPath")) %> " />
                        <br />
                        <br />
                        <br />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

        </Fields>
    </asp:DetailsView>
    <asp:Repeater ID="RptCredits" runat="server" ItemType="MovieScrapper.Entities.MovieCredit">
        <HeaderTemplate>
            <div class="container">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-4">
                    <div class="well">
                        <img src="<%# BuildProfileUrl(Item.ProfilePath) %> " />
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="well">
                                <%# Item.Name %>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="well">
                                    <%# Item.Role %>
                                </div>
                            </div>
                    </div>
                </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <p>
        <asp:Button ID="AddMovieToCategoryButton" runat="server" Height="40px" Text="Add this movie to the selected category " Width="500px" OnClick="AddMovieToCategoryButton_Click" />
    </p>
</asp:Content>
