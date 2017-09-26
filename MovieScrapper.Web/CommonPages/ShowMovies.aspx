<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowMovies.aspx.cs" 
    Inherits="MovieScrapper.Secured.MyMovies" Async="true" %>
<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >

    <link href="../Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/LargePoster.js"></script>
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
                <div class=" pattern">
                    <My:MovieControl ID="MovieControl1" runat="server" Item="<%# ((MovieScrapper.Entities.Movie)Container.DataItem) %>" /> 
                </div>
            </ItemTemplate>
        </asp:DataList>
    </p>
       
</asp:Content>
