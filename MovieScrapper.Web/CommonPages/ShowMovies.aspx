<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowMovies.aspx.cs" 
    Inherits="MovieScrapper.Secured.MyMovies" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >

    <link href="MovieStyleSheet.css" rel="stylesheet" />
    <p>&nbsp;</p>
    <p>
        <asp:Panel runat="server" DefaultButton="SearchButton">
            <asp:Button ID="BackToEditMoviesInThisCategoryButton" runat="server" Text="&#231;" OnClick="BackToEditMoviesInThisCategoryButton_Click" style="font-family:Wingdings; background-color: none" />
            &nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            &nbsp;
            <asp:Button ID="SearchButton" runat="server" OnClick="Button1_Click" Text="Search" />   
            &nbsp;
            <br />   
        </asp:Panel>
    </p>
    <p>
        <asp:DataList ID="MoviesDataList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow" >
            <ItemTemplate>
              <div class="movieItem">  
                  <div id="title">
                      <%# Eval("Title") %> (<%# DisplayYear((string)Eval("ReleaseDate")) %>)
                      <br>
                      </br>
                      <a id="buildUrlWithId" runat="server" href=<%# BuildUrlWithId((int)Eval("Id")) %>>Details</a>
                  </div>  
                  <img class="poster" src=<%# BuildUrl((string)Eval("PosterPath")) %> />                                                  
              </div>
            </ItemTemplate>
        </asp:DataList>
    </p>
       
</asp:Content>
