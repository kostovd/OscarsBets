<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="MovieScrapper.Admin.Calendar" MasterPageFile="~/Site.Master" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="AdminStyleSheet.css" rel="stylesheet" />    
    <h1>Enter start game date</h1>
        <div class="changeDate">                
                <asp:Calendar ID="StartGameCalendar" cssClass="calendar" runat="server" ></asp:Calendar>
                <asp:Button ID="ChangeStartGameDateButton" cssClass="saveButton" runat="server" OnClick="ChangeStartDateButton_Click" Text="Change start date"  />
                <asp:CustomValidator id="StartGameValidator" runat="server" Display="Dynamic" ErrorMessage="Please select a date"  OnServerValidate="StartGameValidator_ServerValidate"></asp:CustomValidator>
        </div>
    <h1>Enter stop game date</h1>
        <div class="changeDate">                
                <asp:Calendar ID="StopGameCalendar" cssClass="calendar" runat="server" ></asp:Calendar>
                <asp:Button ID="ChangeStopGameDateButton" cssClass="saveButton" runat="server" OnClick="ChangeStopDateButton_Click" Text="Change stop date"  />
                <asp:CustomValidator id="StopGameValidator" runat="server" Display="Dynamic" ErrorMessage="Please select a date"  OnServerValidate="StopGameValidator_ServerValidate"></asp:CustomValidator>
        </div>
   
        
</asp:Content>