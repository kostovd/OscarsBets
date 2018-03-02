<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="MovieScrapper.Admin.Calendar" MasterPageFile="~/Site.Master" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../Content/AdminStyleSheet.css" rel="stylesheet" type="text/css" /> 
    <div class="calendar">
        <h3 class="calendarTitle">Enter start game date</h3>
        <asp:Calendar ID="StartGameCalendar" cssClass="calendar" runat="server" ></asp:Calendar>              
     </div>

    <div class="calendar">
        <h3 class="calendarTitle">Enter stop game date</h3>                      
        <asp:Calendar ID="StopGameCalendar" cssClass="calendar" runat="server" ></asp:Calendar>           
     </div>
    <hr />
   <asp:Button ID="Button1" cssClass="saveButton" runat="server" OnClick="ChangeDateButton_Click" Text="Save"  />
   <asp:CustomValidator id="CustomValidator1" runat="server" Display="Dynamic" 
       ErrorMessage="<span class='errorMessage'>Please select a date. The start date must be before the end date.</span>"  
       OnServerValidate="StopGameValidator_ServerValidate"></asp:CustomValidator>
        
</asp:Content>