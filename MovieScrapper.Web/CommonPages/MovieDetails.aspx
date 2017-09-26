<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="MovieScrapper.MovieDetails" Async="true" MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../Content/MovieDetail.css" rel="stylesheet" type="text/css" />       
        <asp:DetailsView ID="DetailsView1" runat="server" Height="157px"  Width="279px" AutoGenerateRows="false" BorderStyle="None" GridLines="None" >
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
                            <a id="backlLink" runat="server" href="<%# BuildBackUrl() %>"><span style="font-family:Wingdings">&#231;</span></a> </br>
                        </br>
                    </div>
                    <img class="poster" src="<%# BuildPosterUrl((string)Eval("PosterPath")) %> "/>
                      <br />
                      <br />
                      <br />                      
                   </div>
                </ItemTemplate>
            </asp:TemplateField>            

        </Fields>  
        </asp:DetailsView>
        <p>
            <asp:Button ID="AddMovieToCategoryButton" runat="server" Height="40px" Text="Add this movie to the selected category "  Width="500px" OnClick="AddMovieToCategoryButton_Click" />
        </p>
        
</asp:Content>
