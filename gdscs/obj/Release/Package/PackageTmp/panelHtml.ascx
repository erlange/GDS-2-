<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelHtml.ascx.cs" Inherits="gds.panelHtml" %>
<table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
	<tbody>
		<tr>
			<td class="pnlHdr">
				<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;<asp:label id="lblTitle" runat="server"></asp:label>
				</div>
			</td>
			<td class="pnlHdr" align="right">
				<asp:hyperlink id="btnEdit" runat="server" tooltip="Edit content" cssclass="pnlBtn">
						<img src="imgedit/edit.gif" border="0" />Edit</asp:hyperlink>
			</td>
		</tr>
		<tr>
			<td class="pnlCnt" colspan="2">
				<asp:label id="lblContent" runat="server"></asp:label>
			</td>
		</tr>
	</tbody>
</table>
