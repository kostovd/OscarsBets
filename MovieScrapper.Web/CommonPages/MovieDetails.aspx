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

    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:Repeater ID="RptCast" runat="server" ItemType="MovieScrapper.Entities.MovieCredit">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="well well-sm">
                            <asp:CheckBox ID="CheckBox1" Visible="true" class="form-check-input" runat="server" Text="Nominated" />
                            <div>
                                <img style="margin-right: 15px;" src="<%# BuildProfileUrl(Item.ProfilePath) %>" />
                                <div style="display: inline-block">
                                    <p><%# Item.Name %></p>
                                    <p><%# Item.Role %></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="col-sm-6">
                <asp:Repeater ID="RptCrew" runat="server" ItemType="MovieScrapper.Entities.MovieCredit">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="well well-sm">
                            <asp:CheckBox ID="CheckBox1" Visible="true" class="form-check-input" runat="server" Text="Nominated" />
                            <div>
                                <img style="float: left; margin-right: 15px;" src="<%# BuildProfileUrl(Item.ProfilePath) %> " />
                                <blockquote class="blockquote">
                                    <p class="mb-0"><%# Item.Name %></p>
                                    <footer class="blockquote-footer"><%# Item.Role %></footer>
                                </blockquote>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <p>
        <asp:Button ID="AddMovieToCategoryButton" runat="server" Height="40px" Text="Add this movie to the selected category " Width="500px" OnClick="AddMovieToCategoryButton_Click" />
    </p>
</asp:Content>
