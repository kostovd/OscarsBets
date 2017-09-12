<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBMovieDetails.aspx.cs" Inherits="MovieScrapper.CommonPages.DBMovieDetails" MasterPageFile="~/Site.Master" %>
        <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <link href="MovieDetail.css" rel="stylesheet" type="text/css" />   
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="false" BorderStyle="None" GridLines="None" DataSourceID="ObjectDataSource1"
                ItemType="MovieScrapper.Entities.Movie">
            <Fields>            
            <asp:TemplateField>
                <ItemTemplate>
                  <div class="movieItem"> 
                    <div id="info">                        
                            <div id="title"><%# Item.Title %> (<%# Item.ReleaseDate %>)</div>                       
                            </br>
                            <div id="overview"><%# Item.Overview %></div> 
                            <hr />                           
                            <a href="<%# BuildImdbUrl(Item.ImdbId) %>" target="_newtab" title="See the info in IMDB"><img class="imdb" src="/imdb.svg" /> </a>
                            <br />
                            <a class="backlLink" title="Back" runat="server" href="<%# BuildBackUrl() %>"><span style="font-family:Wingdings">&#231;</span></a>  
                            <br />
                                                   
                    </div>
                    <img class="poster"  src="<%# BuildPosterUrl((string)Item.PosterPath) %>"/>
                      <br />
                      <br />                     
                   </div>
                </ItemTemplate>
            </asp:TemplateField>            
        </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="ObjectDataSource1" 
            runat="server" 
            SelectMethod="GetMovie" 
            TypeName="MovieScrapper.Business.Interfaces.IMovieService" 
            OnObjectCreating="ObjectDataSource1_ObjectCreating">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="id" Name="id"/>
            </SelectParameters>
        </asp:ObjectDataSource>
</asp:Content>

