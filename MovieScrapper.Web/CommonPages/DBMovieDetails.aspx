<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBMovieDetails.aspx.cs" Inherits="MovieScrapper.CommonPages.DBMovieDetails" MasterPageFile="~/Site.Master" %>
        <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <link href="MovieDetail.css" rel="stylesheet" type="text/css" />   
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="false" BorderStyle="None" GridLines="None" >
            <Fields>            
            <asp:TemplateField>
                <ItemTemplate>
                  <div id="movieItem"> 
                    <div id="info">
                            <div id="title"><%# Eval("Title") %> (<%# Eval("ReleaseDate") %>)

                            </div>                       
                            </br>
                            <div id="overview">
                                <%# Eval("Overview") %>
                            </div>                    
                            <br />
                        <a id="backlLink" runat="server" href="<%# BuildBackUrl() %>"><span style="font-family:Wingdings">&#231;</span> </a>                             
                        </br>
                    </div>
                    <img id="poster" src="<%# BuildPosterUrl((string)Eval("PosterPath")) %>"/>
                      <br />
                      <br />                     
                   </div>
                </ItemTemplate>
            </asp:TemplateField>            
        </Fields>
        </asp:DetailsView>
</asp:Content>

