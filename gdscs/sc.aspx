<%@ Page Title="" Language="C#" MasterPageFile="~/leftOpt.master" AutoEventWireup="true" CodeBehind="sc.aspx.cs" Inherits="gds.ScoreVarPage" %>
<%@ Register TagPrefix="uc1" TagName="treeDs" Src="components/treeDs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeScore" Src="components/treeScore.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeScoreOut" Src="components/treeScoreOut.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="pnl">
        <tr>
            <td class="pnlHdr" height="28">
                <div class="pnlHdrDiv">
                    <img src="images/dots.gif" width="8" height="8">&nbsp;
                    <uc1:panelGenericTitle id="pTitleSc" runat="server">
                    </uc1:panelGenericTitle>
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <uc1:treeScoreOut ID="TreeScoreOut1" runat="server" EnableViewState="False"></uc1:treeScoreOut>
                <a name="varsel"></a>
                <form name="frmVar" method="get">
                <table class="pnlCnt" bgcolor="#dfdfe7">
                    <tr>
                        <td align="center">
                            <b>Dataset:</b>
                            <uc1:treeDs id="TreeDs1" runat="server" enableviewstate="False">
                            </uc1:treeDs>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="white">
                            <uc1:treeScore ID="TreeScore1" runat="server" EnableViewState="False"></uc1:treeScore>
                        </td>
                    </tr>
                </table>
                <br>
                </form>
            </td>
        </tr>
    </table>
</asp:Content>
