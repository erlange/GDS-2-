<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="def2.aspx.cs" Inherits="gds.def2" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGallery" Src="panelGallery.ascx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <!-- BEGIN Panel -->
    <table class="pnl" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                    <uc1:panelGenericTitle id="pTitleWelcome" runat="server"></uc1:panelGenericTitle>
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <uc1:panelGenericText id="pTextWelcome" runat="server"></uc1:panelGenericText>
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
    <br>
    <br>
    <br>
    <!-- END Panel -->
</asp:Content>
