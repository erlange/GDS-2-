<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imgd_old.aspx.cs" Inherits="gds.imgd_old" %>
<%@ Register TagPrefix="uc1" TagName="mnuLeft" Src="mnuLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Template2Cols</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="s.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="script.js"></script>
	</head>
	<body onload="pre();" ms_positioning="FlowLayout">
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
									<td valign="top" nowrap width="192"><!-- BEGIN Panel --> <!-- END Panel --><uc1:mnuleft id="MnuLeft1" runat="server"></uc1:mnuleft><br>
										<span class="pnlCntHdr">Pencarian:</span> <input type="text" name="textfield"><img src="images/go.jpg" align="top">
										<br>
										<!-- BEGIN Panel -->
										<!-- END Panel --></td>
									<td valign="top">
										<!-- BEGIN Panel -->
										<table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="pnlHdr">
													<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
														<asp:label id="lblHeader" runat="server"></asp:label></div>
												</td>
											</tr>
											<tr>
												<td class="pnlCnt">
													<asp:hyperlink id="HyperLink1" runat="server" navigateurl="gallery.aspx"></asp:hyperlink></td>
											</tr>
											<tr>
												<td class="pnlCnt" align="center"><asp:label id=lblTitle runat="server" CssClass="H4" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'></asp:label><br>
													<asp:hyperlink id="lblFilename" runat="server" target="_blank"></asp:hyperlink><br>
													<asp:label id="lblSize" runat="server" cssclass="H9b"></asp:label><br>
													<br>
													<asp:label id="lblClick" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
													</asp:label><br>
													<asp:imagebutton id="ImageButton1" runat="server" onclick="ImageButton1_Click"></asp:imagebutton><br>
													<br>
													<asp:label id=lblDesc runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
													</asp:label><br>
													<br>
												</td>
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
			<!-- END Template --></form>
	</body>
</html>
