<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="MovieScrapper.Admin.Categories" MasterPageFile="~/Site.Master" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <div>
            <br />
            <hr />
            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333"
                OnItemCommand="DataList1_ItemCommand" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" RepeatDirection="Horizontal" RepeatColumns="5" BorderColor="Gray" CellSpacing="3">
                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#EFF3FB" />              
                <ItemTemplate>
                    
                    <asp:Label ID="CategoryTtleLabel" runat="server" style="text-transform: uppercase;" Text='<%# Eval("CategoryTtle") %>' />
                    <br />
                    <asp:Label ID="CategoryDescriptionLabel" runat="server" Text='<%# Eval("CategoryDescription") %>' />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="EditCategory" OnClick="EditCategoryButton_Click" Text="Edit category name or description" 
                        CommandName="EditCategory" CommandArgument='<%# Eval("Id") %>' Width="240px" />
                    <br />
                    <br />
                    <asp:Button ID="ShowMoviesInThisCategoryButton" runat="server" CommandName="EditMoviesInThisCategory"
                        CommandArgument='<%# Eval("Id") %>' Text="Edit movies in this category" Width="240px" />
                    <br />
                    <br />
                </ItemTemplate>
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [CategoryTtle], [CategoryDescription], [Id] FROM [MovieCategories]"></asp:SqlDataSource>
            <br />
        </div>
        <hr />
        <asp:Button ID="AddCategoryButton" runat="server" OnClick="AddCategoryButton_Click" Text="Add new category" Width="240px" />
</asp:Content>
