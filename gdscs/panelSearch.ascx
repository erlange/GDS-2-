<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelSearch.ascx.cs" Inherits="gds.panelSearch" %>
<table id=Table1 cellSpacing=1 cellPadding=1 align=center border=0>
  <tr>
    <td class=H9b colSpan=2><asp:label id=lblSearch runat="server"></asp:label></td></tr>
  <tr>
    <td vAlign=top><asp:textbox id=txtQ runat="server"></asp:textbox><asp:RequiredFieldValidator id="vldQ" runat="server" ErrorMessage="*" ControlToValidate="txtQ" display="Dynamic" cssclass="H11v"></asp:RequiredFieldValidator></td>
    <td vAlign=top><asp:imagebutton id=btnQ runat="server" imageurl="images/go.jpg" 
            onclick="btnQ_Click"></asp:imagebutton></td></tr></table>
