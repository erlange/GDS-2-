<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="s.aspx.cs" Inherits="gds.gds2_s" enableViewState="False" %>
<%@ Register TagPrefix="uc1" TagName="mnuTop" Src="mnuTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Analisis Interaktif GDS2</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="s.css">
		<script language="javascript" src="script.js"></script>
	</HEAD>
	<body onload="pre();">
		<form id="Form1" name="Form1" runat="server">
			<table class="pgCnt" cellspacing="0" cellpadding="0" width="780" align="center" border="0">
				<tr>
					<td>
						<!-- Logo Menu Start -->
						<uc1:mnutop id="MnuTop1" runat="server"></uc1:mnutop>
						<!-- Logo End --></td>
				</tr>
				<tr>
					<td>
						<uc1:langbar id="LangBar1" runat="server"></uc1:langbar>
						<!-- BEGIN ToolBar -->
						<!-- END ToolBar -->
						<!-- BEGIN Panel Container -->
						<table id="pnlContainer" cellspacing="0" cellpadding="4" width="100%" border="0">
							<tr>
								<td valign="top"><!-- BEGIN Panel -->
									<table width="100%" border="0" cellpadding="0" cellspacing="0" class="pnl">
										<tr>
											<td class="pnlHdr" colspan="3">
												<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
													<uc1:panelgenerictitle id="pTitle1" runat="server"></uc1:panelgenerictitle>
												</div>
											</td>
										</tr>
										<tr>
											<td width="33%" class="pnlCnt">
												<uc1:panelgenerictext id="pText1" runat="server"></uc1:panelgenerictext>
												<br>
												<br>
												<select class="lst" name="lstds" id="lstds" runat="server" style="WIDTH: 213px">
												</select>
												<asp:image id="imgDs1" runat="server" height="12px" width="12px" imageurl="images/help.gif"></asp:image>
												<br>
												<div class="pnlCntHdr">
													<div id="idMap" style="BORDER-RIGHT: black 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: black 1px solid; PADDING-LEFT: 10px; BACKGROUND: mistyrose; FILTER: Alpha(Opacity=85); LEFT: 0px; VISIBILITY: hidden; PADDING-BOTTOM: 3px; FONT: 11px Arial; OVERFLOW: visible; BORDER-LEFT: black 1px solid; WIDTH: 200px; PADDING-TOP: 3px; BORDER-BOTTOM: black 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: 35px; TEXT-ALIGN: left; z-Order: 4"></div>
													<p><img src="images/indomap.jpg" border="0" usemap="#indomap"> <MAP NAME="indomap">
															<AREA onmouseover="ciD('idMap','Papua');" 
																onmousemove="cm('idMap');" 
																onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="185,47,188,49" 
																HREF="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Papua');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="179,50,208,86" HREF="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Papua');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="162,43,184,63" HREF="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Maluku');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="144,49,147,52" HREF="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Maluku');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="159,51,161,53" HREF="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Maluku');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="144,57,150,60" HREF="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Maluku Utara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="156,25,160,30" HREF="javascript:cmmz('v.aspx?r=82&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Maluku Utara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="147,27,159,52" HREF="javascript:cmmz('v.aspx?r=82&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="134,80,145,83" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="115,81,132,85" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="131,85,139,91" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="128,92,131,94" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nusa Tenggara Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="97,81,111,86" HREF="javascript:cmmz('v.aspx?r=52&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nusa Tenggara Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="109,87,120,92" HREF="javascript:cmmz('v.aspx?r=52&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Bali');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="90,81,95,84" HREF="javascript:cmmz('v.aspx?r=51&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Jawa Timur','35');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="74,75,89,83" HREF="javascript:cmmz('v.aspx?r=35&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Daerah Istimewa Yogyakarta');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="70,79,72,81" HREF="javascript:cmmz('v.aspx?r=34&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Jawa Tengah');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="76,77,72,81,63,77,76,78" HREF="javascript:cmmz('v.aspx?r=33&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Jawa Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="53,72,63,79" HREF="javascript:cmmz('v.aspx?r=32&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Banten');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="49,71,54,76" HREF="javascript:cmmz('v.aspx?r=36&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sulawesi Tengah');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="122,38,119,41,115,42,120,48,132,48,127,50,120,55,112,48,116,38"
																HREF="javascript:cmmz('v.aspx?r=72&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sulawesi Utara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="131,34,141,41" HREF="javascript:cmmz('v.aspx?r=71&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Gorontalo');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="120,37,132,41" HREF="javascript:cmmz('v.aspx?r=75&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sulawesi Selatan');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="122,58,117,59,114,72,111,70,109,61,112,49" HREF="javascript:cmmz('v.aspx?r=73&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sulawesi Tenggara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="129,63,128,71,123,68,122,58" HREF="javascript:void(null);">
															<AREA onmouseover="ciD('idMap','Kalimatan Timur');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="106,25,107,31,108,40,105,45,97,52,90,35,101,22" HREF="javascript:cmmz('v.aspx?r=64&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Kalimantan Selatan');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="98,60,89,61,96,50" HREF="javascript:cmmz('v.aspx?r=63&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Kalimantan Tengah');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="93,42,94,54,85,60,72,57,74,49,87,40" HREF="javascript:cmmz('v.aspx?r=62&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Kalimantan Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="74,38,88,38,79,47,73,54,72,58,68,55,64,47,65,34" 
																HREF="javascript:cmmz('v.aspx?r=61&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Bangka Belitung');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="45,50,53,57" HREF="javascript:cmmz('v.aspx?r=19&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Bangka Belitung');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="57,54,60,57" HREF="javascript:cmmz('v.aspx?r=19&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Lampung');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="49,64,47,69,39,67,48,61" HREF="javascript:cmmz('v.aspx?r=18&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Bengkulu');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="38,64,37,66,28,54,38,63" HREF="javascript:cmmz('v.aspx?r=17&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Bengkulu');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="31,67,32,67,33,67,33,68,32,68,31,68" HREF="javascript:cmmz('v.aspx?r=17&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="27,45,27,54,20,40,26,42" HREF="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="21,55,16,47,22,57,17,48" HREF="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Barat');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="22,57,21,52,22,57,21,52" HREF="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Utara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="11,24,22,42" HREF="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Utara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="8,36,12,41" HREF="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Utara');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="13,43,15,44" HREF="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nanggroe Aceh Darussalam');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="13,22,10,31,0,18,12,21" HREF="javascript:cmmz('v.aspx?r=11&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Nanggroe Aceh Darussalam');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="5,32,1,28,5,32,1,28" HREF="javascript:cmmz('v.aspx?r=11&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Kepulauan Riau');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="RECT" COORDS="42,41,45,44" HREF="javascript:cmmz('v.aspx?r=21&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Lampung');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="49,64,47,69,39,67,48,61" HREF="javascript:cmmz('v.aspx?r=18&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Sumatera Selatan');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="47,54,47,62,36,63,34,53" HREF="javascript:cmmz('v.aspx?r=16&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Jambi');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="42,50,31,55,27,52,33,47" HREF="javascript:cmmz('v.aspx?r=15&amp;ds=',document.getElementById('lstds'));">
															<AREA onmouseover="ciD('idMap','Riau');" 
																onmousemove="cm('idMap');" onmouseout="ch('idMap');" 
																SHAPE="POLY" COORDS="38,44,31,47,22,36,26,32" HREF="javascript:cmmz('v.aspx?r=14&amp;ds=',document.getElementById('lstds'));">
														</MAP>
														<br>
														<br>
														<select class="lst" id="lstr" name="lstr" runat="server" style="WIDTH: 213px">
														</select>
														<asp:image id="imgProv" runat="server" height="12px" width="12px" imageurl="images/help.gif"></asp:image>
														<br>
														<input type="button" value="Lanjut >>" onclick="document.forms['frm1'].ds.value=lstds.value;document.forms['frm1'].r.value=lstr.value;document.forms['frm1'].submit()"
															id="btn1" name="Button2" runat="server"></p>
												</div>
											</td>
											<td class="pnlCnt" valign="top" width="33%" bgcolor="lavender">
												<uc1:panelgenerictext id="pText2" runat="server"></uc1:panelgenerictext><br>
												<br>
												<select class="lst" id="lstds2" runat="server" style="WIDTH: 213px; BACKGROUND-COLOR: #ffffff">
												</select>
												<asp:image id="imgDs2" runat="server" height="12px" width="12px" imageurl="images/help.gif"></asp:image>
												<br>
												<br>
												<input type="button" value="Lanjut >>" onclick="document.forms['frm2'].ds.value=lstds2.value;document.forms['frm2'].c.value=1;document.forms['frm2'].submit();"
													id="btn2" name="Button3" runat="server">
											</td>
											<td width="33%" valign="top" bordercolor="#ccccff" bgcolor="#ffffcc" class="pnlCnt">
												<uc1:panelgenerictext id="pText3" runat="server"></uc1:panelgenerictext>
												<br>
												<br>
												<select class="lst" id="lstds3" runat="server" style="WIDTH: 213px">
												</select>
												<asp:image id="imgDs3" runat="server" height="12px" width="12px" imageurl="images/help.gif"></asp:image>
												<br>
												<br>
												<input type="button" value="Lanjut >>" onclick="document.forms['frm3'].ds.value=lstds3.value;document.forms['frm3'].submit();"
													id="btn3" name="Button4" runat="server"> &nbsp;</td>
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
		</form>
		<form name="frm1" action="v.aspx" method="get">
			<input type="hidden" name="ds"> <input type="hidden" name="r">
		</form>
		<form name="frm2" action="tw.aspx" method="get">
			<input type="hidden" name="ds"> <input type="hidden" name="c">
		</form>
		<form name="frm3" action="sc.aspx" method="get">
			<input type="hidden" name="ds">
		</form>
		<div id="tip" style="BORDER-RIGHT: black 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: black 1px solid; PADDING-LEFT: 10px; BACKGROUND: #ffffcc; FILTER: Alpha(Opacity=85); LEFT: 0px; VISIBILITY: hidden; PADDING-BOTTOM: 3px; FONT: 11px Arial; OVERFLOW: visible; BORDER-LEFT: black 1px solid; WIDTH: 200px; BOTTOM: 0px; PADDING-TOP: 3px; BORDER-BOTTOM: black 1px solid; POSITION: absolute; HEIGHT: 35px; TEXT-ALIGN: left; z-Order: 12"></div>
	</body>
</HTML>
