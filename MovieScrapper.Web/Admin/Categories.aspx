<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="MovieScrapper.Admin.Categories" MasterPageFile="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="MovieStyleSheet.css" rel="stylesheet" />
        <div>
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
                ItemType="MovieScrapper.Entities.MovieCategory" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    
                        <br />
                        <asp:Label ID="CategoryTtleLabel"  runat="server" style="text-transform: uppercase;font-weight:bold;" Text='<%# Item.CategoryTtle %>' />
                        <br />
                        <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Item.CategoryDescription %>' />   
                        <br />
                        <asp:Button runat="server" ID="EditCategory"  Text="Edit category name or description" CommandName="EditCategory" CommandArgument='<%# Item.Id %>' Width="240px" />
                        <br />
                        <br />
                        <asp:Button runat="server" ID="ShowMoviesInThisCategoryButton" CommandName="ShowMoviesInThisCategory" CommandArgument='<%# Item.Id %>' Text="Show movies in this category" Width="240px" />
                        <hr />
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <hr />
            <asp:Button ID="AddCategoryButton" runat="server" OnClick="AddCategoryButton_Click" Text="Add new category" Width="240px" />
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll"
                TypeName="MovieScrapper.Business.CategoryService"></asp:ObjectDataSource>

        </div>
</asp:Content>
