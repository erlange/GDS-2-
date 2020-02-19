<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="gds.gallery" %>
<%@ Register TagPrefix="uc1" TagName="album" Src="album.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearch" Src="panelSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuLeft" Src="mnuLeft.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>GDS2 - Gallery</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="s.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="script.js"></script>
</head>
	<body ms_positioning="FlowLayout" onload="pre();">
		<form id="Form1" method="post" runat="server">
			<table class="pgCnt" cellspacing="0" cellpadding="0" width="780" align="center" border="0">
				<tbody>
					<tr>
						<td>
							<!-- Logo Menu Start --><uc1:mnutop id="MnuTop1" runat="server"></uc1:mnutop>
							<!-- Logo End --></td>
					</tr>
					<tr>
						<td>
							<!-- BEGIN ToolBar --><uc1:langbar id="LangBar1" runat="server"></uc1:langbar>
							<!-- END ToolBar -->
							<!-- BEGIN Panel Container -->
							<table id="pnlContainer" cellspacing="0" cellpadding="4" width="100%" border="0">
								<tr>
									<td valign="top" nowrap width="192" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"><!-- BEGIN Panel -->  <!-- END Panel -->
										<uc1:mnuleft id="MnuLeft1" runat="server"></uc1:mnuleft><br>
										<!-- BEGIN Panel --><uc1:panelSearch id="PanelSearch1" runat="server"></uc1:panelSearch>
										<!-- END Panel --></td>
									<td valign="top"><!-- BEGIN Panel -->
										<table class="pnl" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="pnlHdr">
													<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;&nbsp;Gallery
													</div>
												</td>
											</tr>
											<tr>
												<td class="pnlCnt">
													<uc1:album id="Album1" runat="server"></uc1:album></td>
											</tr>
											<tr>
												<td class="pnlCnt">&nbsp;</td>
											</tr>
										</table>
										<!-- END Panel --></td>
								</tr>
							</table>
							<!-- END Panel Container --></td>
					</tr>
					<tr>
						<td align="center"><uc1:panelcopyright id="PanelCopyright1" runat="server"></uc1:panelcopyright><uc1:mnubottom id="MnuBottom1" runat="server"></uc1:mnubottom></td>
					</tr>
				</tbody>
			</table>
			&nbsp;</form>
	</body>
</html>
