<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="gds.DefaultPage" %>

<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGallery" Src="panelGallery.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelPpt" Src="panelPpt.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelComments" Src="panelComments.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelHighlight" Src="panelHighlight.ascx" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <!-- BEGIN Panel -->
    <table cellspacing="0" cellpadding="2" border="0">
        <tr valign="top">
            <td valign="top" align="center">
                <!-- BEGIN Center -->
                <table class="pnl" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="pnlHdr">
                            <div class="pnlHdrDiv">
                                <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                <uc1:panelGenericTitle ID="pTitleWelcome" runat="server"></uc1:panelGenericTitle>
                            </div>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="pnlCnt" valign="top">
                            <uc1:panelGenericText ID="pTextWelcome" runat="server"></uc1:panelGenericText>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="pnl" id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="pnlHdr">
                            <div class="pnlHdrDiv">
                                <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                <uc1:panelGenericTitle ID="pTitle1" runat="server"></uc1:panelGenericTitle>
                            </div>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="pnlCnt" valign="top">
                            <uc1:panelGenericText ID="pText1" runat="server"></uc1:panelGenericText>
                            <br>
                            <br>
                            <select class="lst" name="lstds" id="lstds" runat="server" style="width: 213px">
                            </select>
                            <asp:Image ID="imgDs1" runat="server" Height="12px" Width="12px" ImageUrl="images/help.gif">
                            </asp:Image>
                            <br>
                            <div class="pnlCntHdr">
                                <div id="idMap" style="border-right: black 1px solid; padding-right: 2px; border-top: black 1px solid;
                                    padding-left: 10px; background: mistyrose; filter: Alpha(Opacity=85); left: 0px;
                                    visibility: hidden; padding-bottom: 3px; font: 11px Arial; overflow: visible;
                                    border-left: black 1px solid; width: 200px; padding-top: 3px; border-bottom: black 1px solid;
                                    position: absolute; top: 0px; height: 35px; text-align: left; z-order: 4">
                                </div>
                                <p>
                                    <img src="images/indomap.jpg" border="0" usemap="#indomap">
                                    <map name="indomap">
                                        <area onmouseover="ciD('idMap','Papua');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="185,47,188,49" href="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Papua');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="179,50,208,86" href="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Papua');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="162,43,184,63" href="javascript:cmmz('v.aspx?r=94&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Maluku');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="144,49,147,52" href="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Maluku');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="159,51,161,53" href="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Maluku');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="144,57,150,60" href="javascript:cmmz('v.aspx?r=81&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Maluku Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="156,25,160,30" href="javascript:cmmz('v.aspx?r=82&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Maluku Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="147,27,159,52" href="javascript:cmmz('v.aspx?r=82&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="134,80,145,83" href="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="115,81,132,85" href="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="131,85,139,91" href="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nusa Tenggara Timur');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="128,92,131,94" href="javascript:cmmz('v.aspx?r=53&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nusa Tenggara Barat');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="97,81,111,86" href="javascript:cmmz('v.aspx?r=52&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nusa Tenggara Barat');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="109,87,120,92" href="javascript:cmmz('v.aspx?r=52&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Bali');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="90,81,95,84" href="javascript:cmmz('v.aspx?r=51&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Jawa Timur','35');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="74,75,89,83" href="javascript:cmmz('v.aspx?r=35&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Daerah Istimewa Yogyakarta');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="RECT" coords="70,79,72,81" href="javascript:cmmz('v.aspx?r=34&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Jawa Tengah');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="76,77,72,81,63,77,76,78" href="javascript:cmmz('v.aspx?r=33&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Jawa Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="53,72,63,79" href="javascript:cmmz('v.aspx?r=32&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Banten');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="49,71,54,76" href="javascript:cmmz('v.aspx?r=36&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sulawesi Tengah');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="122,38,119,41,115,42,120,48,132,48,127,50,120,55,112,48,116,38"
                                            href="javascript:cmmz('v.aspx?r=72&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sulawesi Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="131,34,141,41" href="javascript:cmmz('v.aspx?r=71&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Gorontalo');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="120,37,132,41" href="javascript:cmmz('v.aspx?r=75&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sulawesi Selatan');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="122,58,117,59,114,72,111,70,109,61,112,49" href="javascript:cmmz('v.aspx?r=73&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sulawesi Tenggara');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="POLY" coords="129,63,128,71,123,68,122,58" href="javascript:void(null);">
                                        <area onmouseover="ciD('idMap','Kalimatan Timur');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="106,25,107,31,108,40,105,45,97,52,90,35,101,22" href="javascript:cmmz('v.aspx?r=64&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Kalimantan Selatan');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="POLY" coords="98,60,89,61,96,50" href="javascript:cmmz('v.aspx?r=63&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Kalimantan Tengah');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="POLY" coords="93,42,94,54,85,60,72,57,74,49,87,40"
                                            href="javascript:cmmz('v.aspx?r=62&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Kalimantan Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="74,38,88,38,79,47,73,54,72,58,68,55,64,47,65,34" href="javascript:cmmz('v.aspx?r=61&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Bangka Belitung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="45,50,53,57" href="javascript:cmmz('v.aspx?r=19&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Bangka Belitung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="57,54,60,57" href="javascript:cmmz('v.aspx?r=19&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Lampung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="49,64,47,69,39,67,48,61" href="javascript:cmmz('v.aspx?r=18&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Bengkulu');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="38,64,37,66,28,54,38,63" href="javascript:cmmz('v.aspx?r=17&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Bengkulu');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="31,67,32,67,33,67,33,68,32,68,31,68" href="javascript:cmmz('v.aspx?r=17&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="27,45,27,54,20,40,26,42" href="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="21,55,16,47,22,57,17,48" href="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Barat');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="22,57,21,52,22,57,21,52" href="javascript:cmmz('v.aspx?r=13&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="11,24,22,42" href="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="8,36,12,41" href="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Utara');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="13,43,15,44" href="javascript:cmmz('v.aspx?r=12&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nanggroe Aceh Darussalam');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="POLY" coords="13,22,10,31,0,18,12,21" href="javascript:cmmz('v.aspx?r=11&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Nanggroe Aceh Darussalam');" onmousemove="cm('idMap');"
                                            onmouseout="ch('idMap');" shape="POLY" coords="5,32,1,28,5,32,1,28" href="javascript:cmmz('v.aspx?r=11&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Kepulauan Riau');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="RECT" coords="42,41,45,44" href="javascript:cmmz('v.aspx?r=21&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Lampung');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="49,64,47,69,39,67,48,61" href="javascript:cmmz('v.aspx?r=18&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Sumatera Selatan');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="47,54,47,62,36,63,34,53" href="javascript:cmmz('v.aspx?r=16&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Jambi');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="42,50,31,55,27,52,33,47" href="javascript:cmmz('v.aspx?r=15&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                        <area onmouseover="ciD('idMap','Riau');" onmousemove="cm('idMap');" onmouseout="ch('idMap');"
                                            shape="POLY" coords="38,44,31,47,22,36,26,32" href="javascript:cmmz('v.aspx?r=14&amp;ds=',document.getElementById('<%= lstds.ClientID %>'));">
                                    </map>
                                    <br>
                                    <br>
                                    <select class="lst" id="lstr" name="lstr" runat="server" style="width: 213px">
                                    </select>
                                    <asp:Image ID="imgProv" runat="server" Height="12px" Width="12px" ImageUrl="images/help.gif">
                                    </asp:Image>
                                    <br>
                                    <input type="button" value="Lanjut >>" onclick="document.forms['frm1'].ds.value=lstds.value;document.forms['frm1'].r.value=lstr.value;document.forms['frm1'].submit()"
                                        id="btn1" name="Button2" runat="server"></p>
                            </div>
                        </td>
                    </tr>
                </table>
                <br>
                <table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="pnlHdr">
                            <div class="pnlHdrDiv">
                                <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                <uc1:panelGenericTitle ID="pTitleSurveyData" runat="server"></uc1:panelGenericTitle>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pnlCnt">
                            <uc1:panelGenericText ID="pTextSurveyData" runat="server"></uc1:panelGenericText>
                            <br>
                            <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="center">
                                        <a href="g1_s.aspx">
                                            <img onmouseup="FP_swapImg(0,0,/*id*/'img1',/*url*/'images/buttonC.jpg')" onmousedown="FP_swapImg(1,0,/*id*/'img1',/*url*/'images/buttonD.jpg')"
                                                id="img1" onmouseover="FP_swapImg(1,0,/*id*/'img1',/*url*/'images/buttonC.jpg')"
                                                onmouseout="FP_swapImg(0,0,/*id*/'img1',/*url*/'images/buttonB.jpg')" height="25"
                                                alt="GDS 1+" src="images/buttonB.jpg" width="91" border="0" fp-style="fp-btn: Glass Rectangle 1; fp-font-style: Bold; fp-font-color-normal: #1F558C; fp-font-color-hover: #000066; fp-proportional: 0"
                                                fp-title="GDS 1+"></a>
                                    </td>
                                    <td align="center">
                                        <a href="s.aspx">
                                            <img onmouseup="FP_swapImg(0,0,/*id*/'img2',/*url*/'images/buttonF.jpg')" onmousedown="FP_swapImg(1,0,/*id*/'img2',/*url*/'images/button10.jpg')"
                                                id="img2" onmouseover="FP_swapImg(1,0,/*id*/'img2',/*url*/'images/buttonF.jpg')"
                                                onmouseout="FP_swapImg(0,0,/*id*/'img2',/*url*/'images/buttonE.jpg')" height="25"
                                                alt="GDS 2" src="images/buttonE.jpg" width="91" border="0" fp-style="fp-btn: Glass Rectangle 1; fp-font-style: Bold; fp-font-color-normal: #1F558C; fp-font-color-hover: #000066; fp-proportional: 0"
                                                fp-title="GDS 2"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br>
                <uc1:panelGallery ID="PanelGallery1" runat="server"></uc1:panelGallery>
                <!-- END Center -->
            </td>
            <td>
                <!-- BEGIN right -->
                <table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="pnlHdr">
                            <div class="pnlHdrDiv">
                                <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                <uc1:panelGenericTitle ID="pTitleHighlight" runat="server"></uc1:panelGenericTitle>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pnlCnt">
                            <uc1:panelHighlight ID="PanelHighlight1" runat="server"></uc1:panelHighlight>
                        </td>
                    </tr>
                </table>
                <br>
                <uc1:panelPpt ID="PanelPpt1" runat="server"></uc1:panelPpt>
                <br>
                <br>
                <uc1:panelComments ID="PanelComments1" runat="server"></uc1:panelComments>
                <br>
                <br>
                <!-- END right -->
            </td>
        </tr>
    </table>
    <!-- END Panel -->
</asp:Content>
