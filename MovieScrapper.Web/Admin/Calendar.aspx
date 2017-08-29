<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="MovieScrapper.Admin.Calendar" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="AdminStyleSheet.css" rel="stylesheet" />      
            <div class="changeDate">                
                <asp:Calendar ID="Calendar1" cssClass="calendar" runat="server" ></asp:Calendar>
                <asp:Button ID="ChangeStopGameDateButton" cssClass="saveButton" runat="server" OnClick="ChangeDateButton_Click" Text="Save"  />
                <asp:CustomValidator id="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="Please select a date"  OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
            </div>
        
</asp:Content>