<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="v.aspx.cs" Inherits="gds.gds2_v" %>

<%@ Register TagPrefix="uc1" TagName="treeComparators" Src="components/treeComparators.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeVars" Src="components/treeVars2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeLocations" Src="components/treeLocations.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeDs" Src="components/treeDs.ascx" %>

<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearchHtml" Src="panelSearchHtml.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelToolTip" Src="panelToolTip.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelAnalysis" Src="panelAnalysis.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Analisis Interaktif GDS2 - Pilihan Variabel</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<!-- <META NAME="save" CONTENT="history"> -->
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="s.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="script.js"></script>
	</head>
	<body onload="pre();">
		<div class="saveHistory" id="TOC" onload="fnLoad()" onsave="fnSave()"></div>
		<%
            int c = Request.Params["c"] == "1" ? 1 : 0;
            int l = bEn? 1: 0;
            string submitString;
            string actionString;
            string chString;
            if (isSingleVar)
            {
                submitString = string.Format("return dist3('frmV',{0});", l);
                actionString = "pvOut.aspx";
            }
            else
            {
                submitString = string.Format("return mz('frmV',{0},{1})", c, l);
                actionString = "tw.aspx";
            }

            if (bEn)
            {
                chString = "Show chart";
            }
            else
            {
                chString = "Tampilkan dengan chart";
            }
      
      	%>
		<table class="pgCnt" id="Table2" cellspacing="0" cellpadding="0" width="780" align="center" border="0">
			<tr>
				<td><uc1:mnutop id="MnuTop1" runat="server"></uc1:mnutop></td>
			</tr>
			<tr>
				<td><uc1:langbar id="LangBar1" runat="server"></uc1:langbar>
					<!-- BEGIN Panel Container -->
					<table id="pnlContainer" cellspacing="0" cellpadding="4" width="100%" border="0">
						<tr>
							<td valign="top" nowrap width="192"><!-- BEGIN Panel --> <!-- END Panel -->
								<!-- 
								<table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td class="pnlHdr" height="28">
											<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;&nbsp;Mode 
												Analisis GDS 2
											</div>
										</td>
									</tr>
									<tr>
										<td class="pnlCnt" width="50%" bgcolor="#ffffcc">
											<div class="pnlCntHdr">Analisis Dasar</div>
											<br>
											<select class="lst" id="lstds" style="WIDTH: 213px; BACKGROUND-COLOR: white" name="lstds"
												runat="server">
											</select>
											&nbsp;
											<br>
											<div class="pnlCntHdr"><img src="images/indofullsm.jpg"><br>
												<br>
												<select class="lst" id="lstr" style="WIDTH: 213px; BACKGROUND-COLOR: white" name="lstr"
													runat="server">
												</select>
											</div>
										</td>
									</tr>
									<tr>
										<td class="pnlCnt" align="right" width="50%" bgcolor="#ffffcc"><input onclick="document.forms['frm1'].ds.value=lstds.value;document.forms['frm1'].r.value=lstr.value;document.forms['frm1'].submit();"
												type="button" value="OK"></td>
									</tr>
									<tr>
										<td class="pnlCnt" bgcolor="lavender">
											<div class="pnlCntHdr"><br>
												Analisis Lanjut<br>
												<br>
											</div>
											<select class="lst" id="lstds2" style="WIDTH: 213px; BACKGROUND-COLOR: white" name="lstds2"
												runat="server">
											</select>
											<br>
											<label for="c1"><input id="c1" type="radio" checked value="1" name="c"> Analisis 
												Variabel Ganda&nbsp;</label>
											<br>
											<label for="c2"><input id="c2" type="radio" value="2" name="c"> Analisis 
												Multi-Variabel&nbsp;</label>&nbsp;
										</td>
									</tr>
									<tr>
										<td class="pnlCnt" align="right" bgcolor="#e6e6fa"><input onclick="document.forms['frm2'].ds.value=lstds2.value;if(c[0].checked) {document.forms['frm2'].c.value=1;} else {document.forms['frm2'].c.value=2;}document.forms['frm2'].submit();"
												type="image" value="OK" name="submit"></td>
									</tr>
								</table>
								--><uc1:panelanalysis id="PanelAnalysis1" runat="server"></uc1:panelanalysis><br>
								<!-- BEGIN Panel --><uc1:panelsearchhtml id="PanelSearchHtml1" runat="server"></uc1:panelsearchhtml> 
								<!-- END Panel --></td>
							<td valign="top"><!-- BEGIN Panel -->
								<table class="pnl" id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td class="pnlHdr" height="28">
											<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8">&nbsp;
												<uc1:panelgenerictitle id="pTitleV" runat="server"></uc1:panelgenerictitle></div>
										</td>
									</tr>
									<tr>
										<td class="pnlCnt">
											<form id="frmV" name="frmV" onsubmit="<%= submitString %>" 
                  action="<%= actionString %>" method="get" >
												<table class="pnlCnt" id="Table1" style="TEXT-ALIGN: left" cellspacing="1" cellpadding="1" width="440" align="center" border="0">
													<tr>
														<td valign="top" colspan="2">
															<br>
															<uc1:panelgenerictext id="pTextV" runat="server"></uc1:panelgenerictext><br>
														</td>
													</tr>
													<tr>
														<td style="HEIGHT: 29px" valign="middle" align="center" bgcolor="#dfdfe7" colspan="2">
															<asp:label id="lblTreeDs" runat="server" cssclass="h9b"></asp:label>&nbsp;
															<uc1:treeds id="TreeDs1" runat="server"></uc1:treeds></td>
													</tr>
													<tr>
														<td valign="top" colspan="2" bgcolor="#dfdfe7"><uc1:treelocations id="TreeLocations1" runat="server" enableviewstate="False"></uc1:treelocations></td>
													</tr>
													<tr>
														<td colspan="2" bgcolor="#dfdfe7"><uc1:treevars id="TreeVars1" runat="server" enableviewstate="False"></uc1:treevars></td>
													</tr>
													<tr>
														<td colspan="2" bgcolor="#dfdfe7"><uc1:treecomparators id="TreeComparators1" runat="server" enableviewstate="False"></uc1:treecomparators></td>
													</tr>
													<tr>
														<td align="center" bgcolor="#dfdfe7" colspan="2"><input type="hidden" name="r"> <input type="hidden" name="d">
															<input type=hidden 
                        value="<%= iDs %>" name=ds> <input 
                        type=hidden value="<%= c %>" name=c 
                        > <label for="ch"><input id="ch" type="checkbox" checked value="1" name="ch"><%= chString %></label></td>
													</tr>
													<tr>
														<td align="center" bgcolor="#dfdfe7" colspan="2"><input id="btnPrev" onclick="history.go(-1)" type="button" value="Back" name="Button2" runat="server">&nbsp;&nbsp;<input id="btnNext" type="submit" value="Next" name="Submit1" runat="server">
														</td>
													</tr>
												</table>
											</form>
										</td>
									</tr>
								</table>
								<!-- END Panel --></td>
						</tr>
					</table>
					<!-- END Panel Container --></td>
			</tr>
			<tr>
				<td><uc1:panelcopyright id="PanelCopyright1" runat="server"></uc1:panelcopyright><uc1:mnubottom id="MnuBottom1" runat="server"></uc1:mnubottom></td>
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