<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMoviesInCategory.aspx.cs" Inherits="MovieScrapper.CommonPages.ShowMoviesInCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MovieStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DataList ID="DataList1" runat="server">
                <ItemTemplate>
                <div id="movieItem">  
                  <div id="title">
                      <%# Eval("Title") %> (<%# DisplayYear((string)Eval("ReleaseDate")) %>)
                      <br>
                      </br>                      
                  </div>  
                  <img id="poster" src=<%# BuildUrl((string)Eval("PosterPath")) %> />                                 
                  
              </div>
            </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
