<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="langBar.ascx.cs" Inherits="gds.langBar1" %>
<table cellspacing="0" cellpadding="0" width="100%">
	<tr style="height:25px;">
		<td class="langBar" valign=middle>
			<span id="spanLang" runat="server"><b><a id="btnIna" runat="server" class="langMnuDis">Bahasa 
						Indonesia</a></b> / <a id="btnEn" href="javascript:void(null)" class="langMnu" runat="server">
					English</a> </span>
		</td>
		<td align="right" class="langBar" valign=middle>
			<asp:label id="lblDate" runat="server" Visible=false></asp:label>&nbsp;
			<asp:label id="lblUser" runat="server" visible="False"></asp:label>&nbsp; <a id="btnLogout" href="AdminLogout.aspx" class="langMnu" runat="server">
				Log-out</a>&nbsp;
                <form action=q.aspx method=get style="margin:0px; padding:0px; display:inline">
                <input type="text" style="color:#ffffff; height:18px;border:1px solid white; background-color:#3675C9;"  name="q" id="txtQ" />
                <input type="image" src="images/search-21.png" alt="Search" title="Search" style="vertical-align:bottom;width:21px;height:21px;" />
                </form>
		</td>
	</tr>
</table>
