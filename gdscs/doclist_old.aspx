<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="doclist_old.aspx.cs" Inherits="gds.doclist_old" %>
<%@ Register TagPrefix="uc1" TagName="panelDocList" Src="panelDocList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearch" Src="panelSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuLeft" Src="mnuLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>GDS - Download</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="s.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="script.js"></script>
</head>
	<body ms_positioning="FlowLayout" onload="pre();">
		<form id="Form1" method="post" runat="server">
			<!-- BEGIN Template -->
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
									<td valign="top" nowrap width="192" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"><!-- BEGIN Panel --> <!-- END Panel -->
										<uc1:mnuleft id="MnuLeft1" runat="server"></uc1:mnuleft><br><uc1:panelSearch id="PanelSearch1" runat="server"></uc1:panelSearch>
										<br>
										<!-- BEGIN Panel -->
										<!-- END Panel -->
									</td>
									<td valign="top">
										<!-- BEGIN Panel -->
										<!-- END Panel -->
										<asp:placeholder id="PlaceHolder1" runat="server"></asp:placeholder>
									</td>
								</tr>
							</table>
							<!-- END Panel Container --></td>
					</tr>
					<tr>
						<td align="center"><uc1:panelcopyright id="PanelCopyright1" runat="server"></uc1:panelcopyright><uc1:mnubottom id="MnuBottom1" runat="server"></uc1:mnubottom></td>
					</tr>
				</tbody>
			</table>
			<!-- END Template -->
		</form>
	</body>
</html>
