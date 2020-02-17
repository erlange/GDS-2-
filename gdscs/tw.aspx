<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tw.aspx.cs" Inherits="gds.tw" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelToolTip" Src="panelToolTip.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearchHtml" Src="panelSearchHtml.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeTwOut" Src="components/treeTwOut.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeDs" Src="components/treeDs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelAnalysis" Src="panelAnalysis.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Analisis Interaktif GDS2 - Variabel Ganda</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<!-- <META NAME="save" CONTENT="history"> -->
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="s.css">
		<script language="javascript" src="script.js"></script>
	</head>
	<body onload="pre();">
		<div id="TOC" class="saveHistory" onload="fnLoad()" onsave="fnSave()"></div>
		<table class="pgCnt" cellspacing="0" cellpadding="0" width="780" align="center" border="0">
			<tr>
				<td>
					<uc1:mnutop id="MnuTop1" runat="server"></uc1:mnutop>
				</td>
			</tr>
			<tr>
				<td>
					<uc1:langbar id="LangBar1" runat="server"></uc1:langbar>
					<!-- BEGIN Panel Container -->
					<table id="pnlContainer" cellspacing="0" cellpadding="4" width="100%" border="0">
						<tr>
							<td valign="top" nowrap width="192"><!-- BEGIN Panel --> <!-- END Panel -->
								<!--
								<table width="100%" border="0" cellpadding="0" cellspacing="0" class="pnl">
									<tr>
										<td height="28" class="pnlHdr">
											<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;Analisis 
												Interaktif Survei GDS 2
											</div>
										</td>
									</tr>
									<tr>
										<td width="50%" class="pnlCnt">
											<form name="frm1" action="v.aspx" method="get">
												<div class="pnlCntHdr">Analisis Dasar</div>
												<br>
												<select class="lst" id="lstds" name="lstds" runat="server" style="WIDTH: 213px">
												</select>
												&nbsp;
												<br>
												<div class="pnlCntHdr">
													<img src="images/indofullsm.jpg"><br>
													<br>
													<select class="lst" id="lstr" name="lstr" runat="server" style="WIDTH: 213px">
													</select><br>
													<input name="submit" type="submit" value="OK">
												</div>
											</form>
										</td>
									</tr>
									<tr>
										<td class="pnlCnt">
											<div class="pnlCntHdr">Analisis Lanjut</div>
											<form name="frm2" action="tw.aspx" method="get" onsubmit="return adv('frm2');">
												<select class="lst" id="lstds2" name="lstds2" runat="server" style="WIDTH: 213px">
												</select>
												<br>
												<br>
												<label for="c1"><input name="c" type="radio" id="c1" value="1" checked> Analisis 
													Variabel Ganda (<em>Scatter-Plot</em>)</label>
												<br>
												<label for="c2"><input name="c" type="radio" value="2" id="c2"> Analisis 
													Multi-Variabel (<em>Score-Maker</em>)</label>
												<br>
												<br>
												<input name="ds" type="hidden"> <input name="submit" type="submit" value="OK">
											</form>
											&nbsp;
										</td>
									</tr>
								</table>
								-->
								<uc1:panelanalysis id="PanelAnalysis1" runat="server"></uc1:panelanalysis>
								<br>
								<!-- BEGIN Panel --><uc1:panelsearchhtml id="PanelSearchHtml1" runat="server"></uc1:panelsearchhtml> 
								<!-- END Panel --></td>
							<td valign="top"><!-- BEGIN Panel -->
								<table width="100%" border="0" cellpadding="0" cellspacing="0" class="pnl">
									<tr>
										<td class="pnlHdr" height="28"><div class="pnlHdrDiv"><img src="images/dots.gif" width="8" height="8">&nbsp;
												<uc1:panelgenerictitle id="pTitleTw" runat="server"></uc1:panelgenerictitle>
											</div>
										</td>
									</tr>
									<tr>
										<td class="pnlCnt"><br>
											<uc1:treetwout id="TreeTwOut1" runat="server" enableviewstate="False"></uc1:treetwout>
										</td>
									</tr>
								</table>
								<!-- END Panel --></td>
						</tr>
					</table>
					<!-- END Panel Container --></td>
			</tr>
			<tr>
				<td>
					<uc1:panelcopyright id="PanelCopyright1" runat="server"></uc1:panelcopyright>
					<uc1:mnubottom id="MnuBottom1" runat="server"></uc1:mnubottom>
				</td>
			</tr>
		</table>
		<br>
		<br>
		<form name="frm1" action="v.aspx" method="get">
			<input type="hidden" name="ds"> <input type="hidden" name="r">
		</form>
		<form name="frm2" action="tw.aspx" method="get">
			<input type="hidden" name="ds"> <input type="hidden" name="c">
		</form>
		<form name="frm3" action="sc.aspx" method="get">
			<input type="hidden" name="ds">
		</form>
	</body>
</html>