<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelGenericTitle.ascx.cs" Inherits="gds.panelGenericTitle" %>
<asp:hyperlink id="btnEdit" runat="server" tooltip="Edit content" cssclass="pnlBtn">
	<img src="imgedit/edit.gif" border="0" />
	<asp:label id="lblEdit" runat="server" cssclass="pnlBtn">Edit Title</asp:label>
</asp:hyperlink>
<asp:label id="lblGenericTitle" runat="server"></asp:label>
