<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="contacts.aspx.cs" Inherits="gds.ContactsPage" %>
<%@ Register TagPrefix="uc1" TagName="panelHtml" Src="panelHtml.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:panelHtml ID="PanelHtml1" runat="server"></uc1:panelHtml>
</asp:Content>
