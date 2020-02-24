<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelGenericText.ascx.cs" Inherits="gds.panelGenericText" %>
<asp:label id="lblBr" runat="server">
	<asp:hyperlink id="btnEdit" runat="server" tooltip="Edit content" cssclass="H11b">
			<img src="imgedit/edit.gif" border="0" />Edit Text
	</asp:hyperlink>
	<br />
</asp:label>
<asp:label id="lblGenericText" runat="server"></asp:label>
