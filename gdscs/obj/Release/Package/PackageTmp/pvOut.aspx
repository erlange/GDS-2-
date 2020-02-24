<%@ Page Title="" Language="C#" MasterPageFile="~/leftOpt.master" AutoEventWireup="true" CodeBehind="pvOut.aspx.cs" Inherits="gds.pvOut" %>

<%@ Register TagPrefix="uc1" TagName="treeDs" Src="components/treeDs.ascx" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="uc1" TagName="treeLocations" Src="components/treeLocations.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeVars" Src="components/treeVars.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeComparators" Src="components/treeComparators.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelResultToolBar" Src="panelResultToolBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr" height="28">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">
                    Pilihan Variabel
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <%  if (isValidRequest)
                    {  %>
                <form id="frmServer" runat="server">
                <uc1:panelResultToolBar id="PanelResultToolBar1" runat="server"></uc1:panelresulttoolbar>
                </form>
                <chart:webchartviewer id="WebChartViewer2" runat="server" designtimedragdrop="12"
                    enableviewstate="False"></chart:webchartviewer>
                <br>
                <chart:webchartviewer id="WebChartViewer1" runat="server" enableviewstate="False"></chart:webchartviewer>
                <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal><asp:PlaceHolder
                    ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                <br>
                <asp:DataGrid ID="DataGrid1" runat="server" EnableViewState="False" AutoGenerateColumns="False"
                    CellPadding="4" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="Purple"
                    CssClass="grid">
                    <FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
                    <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                    <AlternatingItemStyle HorizontalAlign="Right" BackColor="#EEECFF"></AlternatingItemStyle>
                    <ItemStyle HorizontalAlign="Right" ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#9966FF">
                    </HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="District">
                            <ItemStyle Font-Bold="True" BackColor="#CCCCFF"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Mean" HeaderText="Mean" DataFormatString="{0:#,###,###,##0.#}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="std" HeaderText="StdDev" DataFormatString="{0:#,###,###,##0.#}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="minimum" HeaderText="Min" DataFormatString="{0:#,###,###,##0.#}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="maximum" HeaderText="Max" DataFormatString="{0:#,###,###,##0.#}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CIl" HeaderText="CI(-)" DataFormatString="{0:#,###,###,##0.#}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CIh" HeaderText="CI(+)" DataFormatString="{0:#,###,###,##0.#}">
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages">
                    </PagerStyle>
                </asp:DataGrid>
                <%  } %>
                <form id="frmV" name="frmV" onsubmit="<%= submitString %>" action="<%= actionString %>"
                method="get">
                <a name="varsel"></a>
                <asp:Literal ID="LiteralToolTip" runat="server" EnableViewState="False"></asp:Literal>
                <table class="pnlCnt" id="Table1" style="overflow: scroll" cellspacing="1" cellpadding="1"
                    border="0">
                    <tr>
                        <td valign="top" align="center" bgcolor="#dfdfe7" colspan="2">
                            <uc1:treeDs id="TreeDs1" runat="server"></uc1:treeds>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="2">
                            <uc1:treeLocations id="TreeLocations1" runat="server" enableviewstate="False"></uc1:treelocations>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <uc1:treeVars id="TreeVars1" runat="server" enableviewstate="False"></uc1:treevars>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#dfdfe7" colspan="2">
                            <uc1:treeComparators id="TreeComparators1" runat="server" enableviewstate="False"></uc1:treecomparators>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#dfdfe7" colspan="2">
                            <input type="hidden" name="r">
                            <input type="hidden" name="d">
                            <input type="hidden" value="<%= iDs %>" name="ds">
                            <input type="hidden" value="<%= c %>" name="c">
                            <label for="ch">
                                <input id="ch" type="checkbox" checked value="1" name="ch">
                                <%= chString %>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#dfdfe7" colspan="2">
                            <input id="btnPrev" onclick="history.go(-1)" type="button" value="<< Back" name="button"
                                runat="server">
                            &nbsp;&nbsp;
                            <input id="btnNext" type="submit" value="Next >>" name="submit" runat="server">
                        </td>
                    </tr>
                </table>
                </form>
            </td>
        </tr>
    </table>
</asp:Content>
