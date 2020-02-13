<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="err.aspx.cs" Inherits="gds.err" %>
<%@ Register TagPrefix="uc1" TagName="mnuLeft" Src="mnuLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Governance and Decentralization Survey Indonesia</title>
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
									<td valign="top" nowrap width="192"><!-- BEGIN Panel --> <!-- END Panel -->
										<uc1:mnuleft id="MnuLeft1" runat="server"></uc1:mnuleft><br>
										<span class="pnlCntHdr">Pencarian:</span> <input type="text" name="textfield"><img src="images/go.jpg" align="top">
										<br>
										<!-- BEGIN Panel -->
										<!-- END Panel -->
									</td>
									<td valign="top">
										<!-- BEGIN Panel -->
										<table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="pnlHdr" height="28">
													<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
														ERROR</div></td></tr>
											<tr>
												<td class="pnlCnt" height="28"><br><br><br>
													<% if(bEn) %>
													Sorry, but unfortunately the server cannot process your request right now.
													<br><a href="javascript:history.go(-1);">Click here</a> to
													go to the previous page and try again.
													<% else %>
													Maaf, sementara server tidak dapat memroses permintaan Anda.
													<br><a href="javascript:history.go(-1);">Klik di sini</a>
													untuk kembali ke halaman sebelumnya dan mencoba lagi
													<%  %>
													<br><br><br>
												</td></tr></table>
										<!-- END Panel -->
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
