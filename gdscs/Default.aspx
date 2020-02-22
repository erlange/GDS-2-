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
        <tr valign=top >
            <td valign=top align=center>
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
    <br>
    <table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                    <uc1:panelGenericTitle id="pTitleSurveyData" runat="server"></uc1:panelGenericTitle>
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <uc1:panelGenericText id="pTextSurveyData" runat="server"></uc1:panelGenericText>
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
    <uc1:panelGallery id="PanelGallery1" runat="server"></uc1:panelgallery>
                <!-- END Center -->
            </td>
            <td>
            <!-- BEGIN right -->
                        <table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="pnlHdr">
                                        <div class="pnlHdrDiv">
                                            <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                                            <uc1:panelGenericTitle id="pTitleHighlight" runat="server"></uc1:panelGenericTitle>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pnlCnt">
                                        <uc1:panelHighlight id="PanelHighlight1" runat="server"></uc1:panelHighlight>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <uc1:panelPpt id="PanelPpt1" runat="server"></uc1:panelPpt>
                            <br>
                            <br>
                            <uc1:panelComments id="PanelComments1" runat="server"></uc1:panelComments>
                            <br>
                            <br>
            <!-- END right -->
            </td>
        </tr>
    </table>

    <!-- END Panel -->
</asp:Content>
