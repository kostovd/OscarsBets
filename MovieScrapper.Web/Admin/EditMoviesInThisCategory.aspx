<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMoviesInThisCategory.aspx.cs" Inherits="MovieScrapper.Admin.EditMoviesInThisCategory" MasterPageFile="~/Site.Master"%>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <link href="../CommonPages/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <br />
    <hr />
 
        <div>
            <asp:Label ID="CategoryTitle" runat="server" style="text-transform: uppercase;"></asp:Label>           
        </div>
        <hr />       
        <asp:DataList ID="DataList1" runat="server" 
            RepeatColumns="5" 
            RepeatDirection="Horizontal" 
            OnItemCommand="DataList1_ItemCommand" 
            CellPadding="4" 
            ForeColor="#333333" 
            DataSourceID="ObjectDataSource1"
            ItemType="MovieScrapper.Entities.Movie">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <ItemTemplate>
                <div id="movieItem">  
                  <div id="title">
                      <div class="items">
                      <%# Item.Title %> (<%# DisplayYear((string)Item.ReleaseDate) %>)
                      </div>
                      <div class="items">
                      <asp:Button ID="DeleteButton" runat="server" CssClass="items" Height="30px" Text="Delete" Width="100px" 
                          CommandName="Delete" CommandArgument='<%# Item.Id %>'  OnClientClick="return confirm('Are you sure you want to delete this item?')"/>
                     </div>
                      <div class="items">
                      <asp:Button ID="ShowDetailsButton" runat="server" Text="Show details" Width="100px" 
                          CommandName="ShowDetails" CommandArgument='<%# Item.Id %>' CssClass="items" Height="30px" />
                        </div>                   
                  </div>  
                    &nbsp;<img id="poster" src="<%# BuildPosterUrl(Item.PosterPath) %>" class="auto-style2" /></div>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataList>
        <asp:ObjectDataSource ID="ObjectDataSource1" 
            runat="server" 
            SelectMethod="GetAllMoviesInCategory" 
            TypeName="MovieScrapper.Business.CategoryService">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="categoryId" Name="categoryId"/>
            </SelectParameters>
        </asp:ObjectDataSource>
        <hr />
    <asp:Button ID="BackToEditCategoriesButton" runat="server" Text="&#231;" OnClick="BackToEditCategoriesButton_Click" style="font-family:Wingdings" />
     &nbsp;
    <asp:Button ID="AddMovieToThiscategoryButton" runat="server" OnClick="AddMovieToThisCategoryButton_Click" Text="Add movie to this category" Width="282px" />
        <br />
</asp:Content>
