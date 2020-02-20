<%@ Page Title="" Language="C#" MasterPageFile="~/leftOpt.master" AutoEventWireup="true"
    CodeBehind="v.aspx.cs" Inherits="gds.VarSelectPage" %>

<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericTitle" Src="panelGenericTitle.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelSearchHtml" Src="panelSearchHtml.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeComparators" Src="components/treeComparators.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeVars" Src="components/treeVars2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeLocations" Src="components/treeLocations.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeDs" Src="components/treeDs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelAnalysis" Src="panelAnalysis.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="saveHistory" id="TOC" onload="fnLoad()" onsave="fnSave()">
        <table class="pnl" id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="pnlHdr" height="28">
                    <div class="pnlHdrDiv">
                        <img height="8" src="images/dots.gif" width="8">&nbsp;
                        <uc1:panelGenericTitle ID="pTitleV" runat="server"></uc1:panelGenericTitle>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="pnlCnt">
                    <form id="frmV" name="frmV" onsubmit="<%= submitString %>" action="<%= actionString %>"
                    method="get">
                    <table class="pnlCnt" id="Table1" style="text-align: left" cellspacing="1" cellpadding="1"
                        width="440" align="center" border="0">
                        <tr>
                            <td valign="top">
                                <br>
                                <uc1:panelGenericText ID="pTextV" runat="server"></uc1:panelGenericText>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 29px" valign="middle" align="center" bgcolor="#dfdfe7">
                                <asp:Label ID="lblTreeDs" runat="server" CssClass="h9b"></asp:Label>&nbsp;
                                <uc1:treeDs ID="TreeDs1" runat="server"></uc1:treeDs>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" bgcolor="#dfdfe7">
                                <uc1:treeLocations ID="TreeLocations1" runat="server" EnableViewState="False"></uc1:treeLocations>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#dfdfe7">
                                <uc1:treeVars ID="TreeVars1" runat="server" EnableViewState="False"></uc1:treeVars>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#dfdfe7">
                                <uc1:treeComparators ID="TreeComparators1" runat="server" EnableViewState="False">
                                </uc1:treeComparators>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#dfdfe7">
                                <label for="ch">
                                    <input id="ch" type="checkbox" checked value="1" name="ch"><%= chString %></label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#dfdfe7">
                                <input type="hidden" name="r">
                                <input type="hidden" name="d">
                                <input type="hidden" value="<%= iDs %>" name="ds">
                                <input type="hidden" value="<%= c %>" name="c">
                                <input id="btnPrev" onclick="history.go(-1)" type="button" value="Back" name="Button2"
                                    runat="server">&nbsp;&nbsp;<input id="btnNext" type="submit" value="Next" name="Submit1"
                                        runat="server">
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
    <%
      
    %>
</asp:Content>
