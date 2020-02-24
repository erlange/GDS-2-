<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelGallery.ascx.cs" Inherits="gds.panelGallery" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="album" Src="album.ascx" %>
<table class="pnl" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
	<tbody>
		<tr>
			<td class="pnlHdr">
				<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;&nbsp;
					<uc1:panelgenerictitle id="pTitleGallery" runat="server"></uc1:panelgenerictitle>
				</div>
			</td>
		</tr>
		<tr>
			<td class="pnlCnt">
				<uc1:album id="Album1" runat="server"></uc1:album></td>
		</tr>
		<tr>
			<td class="pnlCnt" align="right"><asp:hyperlink cssclass="H11b" id="lblFooter" runat="server" navigateurl="gallery.aspx"></asp:hyperlink></td>
		</tr>
	</tbody>
</table>
