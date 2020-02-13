<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="langBar.ascx.cs" Inherits="gds.langBar1" %>
<table cellspacing="0" cellpadding="0" width="100%">
	<tr>
		<td height="20" class="langBar">
			<span id="spanLang" runat="server"><b><a id="btnIna" runat="server" class="langMnuDis">Bahasa 
						Indonesia</a></b> / <a id="btnEn" href="javascript:void(null)" class="langMnu" runat="server">
					English</a> </span>
		</td>
		<td align="right" class="langBar">
			<asp:label id="lblDate" runat="server"></asp:label>&nbsp;
			<asp:label id="lblUser" runat="server" visible="False"></asp:label>&nbsp; <a id="btnLogout" href="AdminLogout.aspx" class="langMnu" runat="server">
				Log-out</a>&nbsp;
		</td>
	</tr>
</table>
