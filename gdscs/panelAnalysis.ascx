<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelAnalysis.ascx.cs" Inherits="gds.panelAnalysis" %>
<table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td class="pnlHdr" height="28">
			<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;&nbsp;
				<asp:label id="lblHeader" runat="server"></asp:label>
			</div>
		</td>
		<td class="pnlHdr" height="28"></td>
	</tr>
	<tr>
		<td class="pnlCnt" width="50%" bgcolor="#ffffcc" colspan="2"><div class="pnlCntHdr">
				<asp:label id="lblBasic" runat="server"></asp:label>&nbsp; &nbsp;
				<asp:image id="imgHelpBasic" runat="server" imageurl="images/help.gif" visible="False"></asp:image>
				<br>
				<select class="lst" id="lstds" style="WIDTH: 213px; BACKGROUND-COLOR: white" name="lstds"
					runat="server">
				</select>
				&nbsp;
				<br>
			</div>
			<div id="idMap" style="BORDER-RIGHT: black 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: black 1px solid; PADDING-LEFT: 10px; BACKGROUND: mistyrose; FILTER: Alpha(Opacity=85); LEFT: 0px; VISIBILITY: hidden; PADDING-BOTTOM: 3px; FONT: 11px Arial; OVERFLOW: visible; BORDER-LEFT: black 1px solid; WIDTH: 200px; PADDING-TOP: 3px; BORDER-BOTTOM: black 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: 35px; TEXT-ALIGN: left; z-Order: 4"></div>
			<div class="pnlCntHdr"><img src="images/indomap.jpg" usemap="#indomap">&nbsp; <MAP NAME="indomap">
					<AREA onmouseover="ciD('idMap','Papua');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="185,47,188,49" HREF="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Papua');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="179,50,208,86" HREF="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('<%= lstds.ClientID%>'));">
					<AREA onmouseover="ciD('idMap','Papua');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="162,43,184,63" HREF="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Maluku');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="144,49,147,52" HREF="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Maluku');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="159,51,161,53" HREF="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Maluku');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="144,57,150,60" HREF="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Maluku Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="156,25,160,30" HREF="javascript:cmmz('v.aspx?r=82&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Maluku Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="147,27,159,52" HREF="javascript:cmmz('v.aspx?r=82&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="134,80,145,83" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="115,81,132,85" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="131,85,139,91" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="128,92,131,94" HREF="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nusa Tenggara Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="97,81,111,86" HREF="javascript:cmmz('v.aspx?r=52&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nusa Tenggara Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="109,87,120,92" HREF="javascript:cmmz('v.aspx?r=52&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Bali');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="90,81,95,84" HREF="javascript:cmmz('v.aspx?r=51&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Jawa Timur','35');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="74,75,89,83" HREF="javascript:cmmz('v.aspx?r=35&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Daerah Istimewa Yogyakarta');" onmousemove="cm('idMap');"
						onmouseout="ch('idMap');" SHAPE="RECT" COORDS="70,79,72,81" HREF="javascript:cmmz('v.aspx?r=34&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Jawa Tengah');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="76,77,72,81,63,77,76,78" HREF="javascript:cmmz('v.aspx?r=33&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Jawa Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="53,72,63,79" HREF="javascript:cmmz('v.aspx?r=32&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Banten');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="49,71,54,76" HREF="javascript:cmmz('v.aspx?r=36&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sulawesi Tengah');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="122,38,119,41,115,42,120,48,132,48,127,50,120,55,112,48,116,38"
						HREF="javascript:cmmz('v.aspx?r=72&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sulawesi Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="131,34,141,41" HREF="javascript:cmmz('v.aspx?r=71&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Gorontalo');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="120,37,132,41" HREF="javascript:cmmz('v.aspx?r=75&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sulawesi Selatan');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="122,58,117,59,114,72,111,70,109,61,112,49" HREF="javascript:cmmz('v.aspx?r=73&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sulawesi Tenggara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="129,63,128,71,123,68,122,58" HREF="javascript:void(null);">
					<AREA onmouseover="ciD('idMap','Kalimatan Timur');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="106,25,107,31,108,40,105,45,97,52,90,35,101,22" HREF="javascript:cmmz('v.aspx?r=64&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Kalimantan Selatan');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="98,60,89,61,96,50" HREF="javascript:cmmz('v.aspx?r=63&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Kalimantan Tengah');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="93,42,94,54,85,60,72,57,74,49,87,40" HREF="javascript:cmmz('v.aspx?r=62&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Kalimantan Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="74,38,88,38,79,47,73,54,72,58,68,55,64,47,65,34" HREF="javascript:cmmz('v.aspx?r=61&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Bangka Belitung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="45,50,53,57" HREF="javascript:cmmz('v.aspx?r=19&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Bangka Belitung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="57,54,60,57" HREF="javascript:cmmz('v.aspx?r=19&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Lampung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="49,64,47,69,39,67,48,61" HREF="javascript:cmmz('v.aspx?r=18&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Bengkulu');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="38,64,37,66,28,54,38,63" HREF="javascript:cmmz('v.aspx?r=17&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Bengkulu');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="31,67,32,67,33,67,33,68,32,68,31,68" HREF="javascript:cmmz('v.aspx?r=17&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="27,45,27,54,20,40,26,42" HREF="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="21,55,16,47,22,57,17,48" HREF="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="22,57,21,52,22,57,21,52" HREF="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="11,24,22,42" HREF="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="8,36,12,41" HREF="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="13,43,15,44" HREF="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nanggroe Aceh Darussalam');" onmousemove="cm('idMap');"
						onmouseout="ch('idMap');" SHAPE="POLY" COORDS="13,22,10,31,0,18,12,21" HREF="javascript:cmmz('v.aspx?r=11&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Nanggroe Aceh Darussalam');" onmousemove="cm('idMap');"
						onmouseout="ch('idMap');" SHAPE="POLY" COORDS="5,32,1,28,5,32,1,28" HREF="javascript:cmmz('v.aspx?r=11&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Kepulauan Riau');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="RECT" COORDS="42,41,45,44" HREF="javascript:cmmz('v.aspx?r=21&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Lampung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="49,64,47,69,39,67,48,61" HREF="javascript:cmmz('v.aspx?r=18&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Sumatera Selatan');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="47,54,47,62,36,63,34,53" HREF="javascript:cmmz('v.aspx?r=16&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Jambi');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="42,50,31,55,27,52,33,47" HREF="javascript:cmmz('v.aspx?r=15&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
					<AREA onmouseover="ciD('idMap','Riau');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
						SHAPE="POLY" COORDS="38,44,31,47,22,36,26,32" HREF="javascript:cmmz('v.aspx?r=14&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
				</MAP>
			</div>
		</td>
	</tr>
	<tr>
		<td class="pnlCnt" width="80%" bgcolor="#ffffcc"><select class="lst" id="lstr" style="BACKGROUND-COLOR: white" name="lstr" runat="server"></select></td>
		<td class="pnlCnt" align="right" width="20%" bgcolor="#ffffcc"><input id="btn1" onclick="document.forms['frm1'].ds.value=lstds.value;document.forms['frm1'].r.value=lstr.value;document.forms['frm1'].submit();"
				type="image" src="images/go.jpg" value="OK" name="Image1" runat="server"></td>
	</tr>
	<tr>
		<td class="pnlCnt" align="right" width="80%" bgcolor="#ffffcc"></td>
		<td class="pnlCnt" align="right" width="20%" bgcolor="#ffffcc"></td>
	</tr>
	<tr>
		<td class="pnlCnt" bgcolor="lavender" colspan="2">
			<div class="pnlCntHdr"><br>
				<asp:label id="lblAdv" runat="server"></asp:label>&nbsp;&nbsp;
				<asp:image id="imgHelpAdv" runat="server" imageurl="images/help.gif" visible="False"></asp:image>&nbsp;<br>
				<br>
			</div>
			<select class="lst" id="lstds2" style="WIDTH: 213px; BACKGROUND-COLOR: white" name="lstds2"
				runat="server">
			</select>
			<br>
		</td>
	</tr>
	<tr>
		<td class="pnlCnt" width="80%" bgcolor="#e6e6fa"><label for="c1"><input id="c1" type="radio" value="1" name="c" checked>
				<asp:label id="lblDual" runat="server"></asp:label>&nbsp;</label>
			<br>
			<label for="c2"><input id="c2" type="radio" value="2" name="c">
				<asp:label id="lblMulti" runat="server"></asp:label>&nbsp;</label>&nbsp;
		</td>
		<td class="pnlCnt" align="right" width="20%" bgcolor="#e6e6fa"><input id="btn2" onclick="document.forms['frm2'].ds.value=lstds2.value;if(c[0].checked) {document.forms['frm2'].c.value=1;} else {document.forms['frm2'].c.value=2;}document.forms['frm2'].submit();"
				type="image" src="images/go.jpg" value="OK" name="submit" runat="server"></td>
	</tr>
</table>
