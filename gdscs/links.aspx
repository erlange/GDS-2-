<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="links.aspx.cs" Inherits="gds.links" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearch" Src="panelSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuLeft" Src="mnuLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Links</title>
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
									<td style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px" valign="top" nowrap width="192"><!-- BEGIN Panel -->
										<!-- END Panel --><uc1:mnuleft id="MnuLeft1" runat="server"></uc1:mnuleft><br><uc1:panelSearch id="PanelSearch1" runat="server"></uc1:panelSearch>
										<br>
										<!-- BEGIN Panel -->
										<!-- END Panel --></td>
									<td valign="top">
										<!-- BEGIN Panel -->
										<table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="pnlHdr">
													<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
														Links</div>
												</td>
											</tr>
											<tr>
												<td class="pnlCnt">
													<asp:hyperlink id="btnEditRecords" runat="server" cssclass="H11v" navigateurl="AdminEditLinks.aspx" tooltip="Edit content"><img src="imgedit/tbledit.gif" border="0" />Edit Data</asp:hyperlink>
													<asp:datalist id="DataList1" runat="server" bordercolor="#CC9966" 
                                                        borderstyle="None" backcolor="White" cellpadding="4" gridlines="Both" 
                                                        borderwidth="0px" onitemdatabound="DataList1_ItemDataBound">
														<selecteditemstyle font-bold="True" forecolor="#663399" backcolor="#FFCC66"></selecteditemstyle>
														<footerstyle forecolor="#330099" backcolor="#FFFFCC"></footerstyle>
														<itemstyle forecolor="#330099" cssclass="H11v" backcolor="White"></itemstyle>
														<itemtemplate>
															<asp:hyperlink id="lblTitle" runat="server" cssclass="H11vb" text='<%# DataBinder.Eval(Container, "DataItem.title") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
															</asp:hyperlink><!--
						<asp:label id="lblBR2" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label>
						--><br>
															<asp:hyperlink id="lblUrl" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.title") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
															</asp:hyperlink><br>
															<asp:label id="lblDesc" runat="server" cssclass="H9" text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
															</asp:label><br>
															<asp:label id="lblSubmitDate" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
															</asp:label>:
															<asp:label id="lblDate" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
															</asp:label>
														</itemtemplate>
														<headerstyle font-bold="True" forecolor="#FFFFCC" backcolor="#990000"></headerstyle>
													</asp:datalist></td>
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
