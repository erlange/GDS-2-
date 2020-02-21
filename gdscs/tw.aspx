<%@ Page Title="" Language="C#" MasterPageFile="~/leftOpt.master" AutoEventWireup="true" CodeBehind="tw.aspx.cs" Inherits="gds.ScatterPage" %>
<%@ Register TagPrefix="uc1" TagName="treeTwOut" Src="components/treeTwOut.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="pnl">
        <tr>
            <td class="pnlHdr" height="28">
                <div class="pnlHdrDiv">
                    <img src="images/dots.gif" width="8" height="8">&nbsp;
                    <uc1:panelgenerictitle id="pTitleTw" runat="server">
                    </uc1:panelgenerictitle>
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <br>
                <uc1:treeTwOut ID="TreeTwOut1" runat="server" EnableViewState="False"></uc1:treeTwOut>
            </td>
        </tr>
    </table>
</asp:Content>
