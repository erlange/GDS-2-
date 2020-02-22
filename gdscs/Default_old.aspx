<%@ Page CodeBehind="Default_old.aspx.cs" Language="cs" AutoEventWireup="true" Inherits="gds.gds2_default_old"
    EnableViewState="True" %>

<%@ Register TagPrefix="uc1" TagName="mnuAdmin" Src="mnuAdmin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearch" Src="panelSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelPpt" Src="panelPpt.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelHighlight" Src="panelHighlight.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelCopyright" Src="panelCopyright.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopMenu" Src="mnuTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuBottom" Src="mnuBottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="mnuLeft" Src="mnuLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGallery" Src="panelGallery.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="langBar" Src="langBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelComments" Src="panelComments.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panel" Src="panelHtml.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Governance and Decentralization Survey Indonesia Web Site</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="s.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="script.js"></script>
</head>
<body onload="pre();">
    <form id="Form1" name="Form1" runat="server">
    <table class="pgCnt" cellspacing="0" cellpadding="0" width="780" align="center" border="0">
        <tr>
            <td>
                <!-- Logo Menu Start -->
                <uc1:topmenu id="TopMenu1" runat="server"></uc1:topmenu>
                <!-- Logo End -->
            </td>
        </tr>
        <tr>
            <td>
                <!-- BEGIN ToolBar -->
                <uc1:langbar id="LangBar1" runat="server"></uc1:langbar>
                <!-- END ToolBar -->
                <!-- BEGIN Panel Container -->
                <table id="pnlContainer" cellspacing="0" cellpadding="4" width="100%" border="0">
                    <tr>
                        <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px"
                            valign="top" nowrap width="192">
                            <!-- BEGIN Panel -->
                            <!-- END Panel -->
                            <uc1:mnuleft id="MnuLeft1" runat="server"></uc1:mnuleft>
                            <br>
                            <!-- BEGIN Panel -->
                            <uc1:panelsearch id="PanelSearch1" runat="server"></uc1:panelsearch>
                            <!-- END Panel -->
                        </td>
                        <td valign="top" width="344">
                            <!-- BEGIN Panel -->
                            <table class="pnl" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="pnlHdr">
                                        <div class="pnlHdrDiv">
                                            <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                            <uc1:panelgenerictitle id="pTitleWelcome" runat="server"></uc1:panelgenerictitle>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pnlCnt">
                                        <uc1:panelgenerictext id="pTextWelcome" runat="server"></uc1:panelgenerictext>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="pnlHdr">
                                        <div class="pnlHdrDiv">
                                            <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                            <uc1:panelgenerictitle id="pTitleSurveyData" runat="server"></uc1:panelgenerictitle>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pnlCnt">
                                        <uc1:panelgenerictext id="pTextSurveyData" runat="server"></uc1:panelgenerictext>
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
                            <uc1:panelgallery id="PanelGallery1" runat="server"></uc1:panelgallery>
                            <br>
                            <br>
                            <br>
                            <!-- END Panel -->
                        </td>
                        <td valign="top" width="218">
                            <table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="pnlHdr">
                                        <div class="pnlHdrDiv">
                                            <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                            <uc1:panelgenerictitle id="pTitleHighlight" runat="server"></uc1:panelgenerictitle>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pnlCnt">
                                        <uc1:panelhighlight id="PanelHighlight1" runat="server"></uc1:panelhighlight>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <uc1:panelppt id="PanelPpt1" runat="server"></uc1:panelppt>
                            <br>
                            <br>
                            <uc1:panelcomments id="PanelComments1" runat="server"></uc1:panelcomments>
                            <br>
                            <br>
                        </td>
                    </tr>
                </table>
                <!-- END Panel Container -->
            </td>
        </tr>
        <tr>
            <td align="center">
                <uc1:panelcopyright id="PanelCopyright1" runat="server"></uc1:panelcopyright>
                <uc1:mnubottom id="MnuBottom1" runat="server"></uc1:mnubottom>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
