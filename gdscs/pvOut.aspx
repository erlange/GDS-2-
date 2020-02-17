<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pvOut.aspx.cs" Inherits="gds.pvOut" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelAnalysis" Src="panelAnalysis.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeDs" Src="components/treeDs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearchHtml" Src="panelSearchHtml.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelResultToolBar" Src="panelResultToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="uc1" TagName="treeLocations" Src="components/treeLocations.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeVars" Src="components/treeVars.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeComparators" Src="components/treeComparators.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Analisis Interaktif GDS2 - Analisis Dasar</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<!-- <META NAME="save" CONTENT="history"> -->
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="s.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="script.js"></script>
	</HEAD>
	<body onload="pre();">
		<div class="saveHistory" id="TOC" onload="fnLoad()" onsave="fnSave()"></div>
		<%
			
            int c = Convert.ToInt32( Request.Params["c"]!=null? (Request.Params["c"].ToString() == "1"? 1: 0):0);
            int l = Convert.ToInt32((bEn? 1: 0));
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
		<table class="pgCnt" cellspacing="0" cellpadding="0" width="780" align="center" border="0">
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
							--><uc1:panelanalysis id="PanelAnalysis1" runat="server"></uc1:panelanalysis>
								<br>
								<!-- BEGIN Panel --><uc1:panelsearchhtml id="PanelSearchHtml1" runat="server"></uc1:panelsearchhtml>
								<!-- END Panel --></td>
							<td valign="top"><!-- BEGIN Panel -->
								<table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td class="pnlHdr" height="28">
											<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> Pilihan 
												Variabel
											</div>
										</td>
									</tr>
									<tr>
										<td class="pnlCnt">
											<%  if(isValidRequest){  %>
											<form id="frmServer" runat="server">
												<uc1:panelresulttoolbar id="PanelResultToolBar1" runat="server"></uc1:panelresulttoolbar></form>
											<chart:webchartviewer id="WebChartViewer2" runat="server" designtimedragdrop="12" enableviewstate="False"></chart:webchartviewer><br>
											<chart:webchartviewer id="WebChartViewer1" runat="server" enableviewstate="False"></chart:webchartviewer><asp:literal id="Literal1" runat="server" enableviewstate="False"></asp:literal><asp:placeholder id="PlaceHolder1" runat="server"></asp:placeholder><br>
											<asp:datagrid id="DataGrid1" runat="server" enableviewstate="False" autogeneratecolumns="False"
												cellpadding="4" backcolor="White" borderwidth="1px" borderstyle="None" bordercolor="Purple"
												cssclass="grid">
												<footerstyle forecolor="#003399" backcolor="#99CCCC"></footerstyle>
												<selecteditemstyle font-bold="True" forecolor="#CCFF99" backcolor="#009999"></selecteditemstyle>
												<alternatingitemstyle horizontalalign="Right" backcolor="#EEECFF"></alternatingitemstyle>
												<itemstyle horizontalalign="Right" forecolor="Black" backcolor="White"></itemstyle>
												<headerstyle font-bold="True" horizontalalign="Center" verticalalign="Middle" backcolor="#9966FF"></headerstyle>
												<columns>
													<asp:boundcolumn datafield="District">
														<itemstyle font-bold="True" backcolor="#CCCCFF"></itemstyle>
													</asp:boundcolumn>
													<asp:boundcolumn datafield="Mean" headertext="Mean" dataformatstring="{0:#,###,###,##0.#}"></asp:boundcolumn>
													<asp:boundcolumn datafield="std" headertext="StdDev" dataformatstring="{0:#,###,###,##0.#}"></asp:boundcolumn>
													<asp:boundcolumn datafield="minimum" headertext="Min" dataformatstring="{0:#,###,###,##0.#}"></asp:boundcolumn>
													<asp:boundcolumn datafield="maximum" headertext="Max" dataformatstring="{0:#,###,###,##0.#}"></asp:boundcolumn>
													<asp:boundcolumn datafield="CIl" headertext="CI(-)" dataformatstring="{0:#,###,###,##0.#}"></asp:boundcolumn>
													<asp:boundcolumn datafield="CIh" headertext="CI(+)" dataformatstring="{0:#,###,###,##0.#}"></asp:boundcolumn>
												</columns>
												<pagerstyle horizontalalign="Left" forecolor="#003399" backcolor="#99CCCC" mode="NumericPages"></pagerstyle>
											</asp:datagrid>
											<%  } %>
											<form id=frmV name=frmV onsubmit="<%= submitString %>" 
                  action="<%= actionString %>" method=get 
                  >
												<a name="varsel"></a>
												<asp:literal id="LiteralToolTip" runat="server" enableviewstate="False"></asp:literal>
												<table class="pnlCnt" id="Table1" style="OVERFLOW: scroll" cellspacing="1" cellpadding="1"
													border="0">
													<tr>
														<td valign="top" align="center" bgcolor="#dfdfe7" colspan="2"><uc1:treeds id="TreeDs1" runat="server"></uc1:treeds></td>
													</tr>
													<tr>
														<td valign="top" colspan="2"><uc1:treelocations id="TreeLocations1" runat="server" enableviewstate="False"></uc1:treelocations></td>
													</tr>
													<tr>
														<td colspan="2"><uc1:treevars id="TreeVars1" runat="server" enableviewstate="False"></uc1:treevars></td>
													</tr>
													<tr>
														<td bgcolor="#dfdfe7" colspan="2"><uc1:treecomparators id="TreeComparators1" runat="server" enableviewstate="False"></uc1:treecomparators></td>
													</tr>
													<tr>
														<td align="center" bgcolor="#dfdfe7" colspan="2"><input type="hidden" name="r"> <input type="hidden" name="d">
															<input type=hidden 
                        value="<%= iDs %>" name=ds> <input 
                        type=hidden value="<%= c %>" name=c 
                        > <label for="ch"><input id="ch" type="checkbox" checked value="1" name="ch">
																<%= chString %>
															</label>
														</td>
													</tr>
													<tr>
														<td align="center" bgcolor="#dfdfe7" colspan="2"><input id="btnPrev" onclick="history.go(-1)" type="button" value="<< Back" name="button"
																runat="server"> &nbsp;&nbsp; <input id="btnNext" type="submit" value="Next >>" name="submit" runat="server">
														</td>
													</tr>
												</table>
											</form>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td><uc1:panelcopyright id="PanelCopyright1" runat="server"></uc1:panelcopyright><uc1:mnubottom id="MnuBottom1" runat="server"></uc1:mnubottom></td>
			</tr>
		</table>
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
</HTML>
