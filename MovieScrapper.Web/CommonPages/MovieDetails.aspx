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
                            <div>
                                <img style="margin-right: 15px;" src="<%# BuildProfileUrl(Item.ProfilePath) %>" />
                                <div style="display: inline-block">
                                    <p class="h5"><%# Item.Name %></p>
                                    <p class="h5 text-muted"><%# Item.Role %></p>
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
                            <div>
                                <img style="margin-right: 15px;" src="<%# BuildProfileUrl(Item.ProfilePath) %>" />
                                <div style="display: inline-block">
                                    <p class="h5"><%# Item.Name %></p>
                                    <p class="h5 text-muted"><%# Item.Role %></p>
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
