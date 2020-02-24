<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="treeTwOut.ascx.cs" Inherits="gds.treeTwOut" %>
<%@ Register TagPrefix="uc1" TagName="treeComparators" Src="treeComparators.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeVars" Src="treeVars.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeLocations" Src="treeLocations.ascx" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="../panelGenericText.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelResultToolBar" Src="../panelResultToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="treeDs" Src="treeDs.ascx" %>
<!-- BEGIN OF Tree -->
<%
			
            int c = Convert.ToInt32( Request.Params["c"]!=null? (Request.Params["c"].ToString() == "1"? 1: 0):0);
            int l = Convert.ToInt32((bEn? 1: 0));
    string submitString;
    string actionString;
    string chString;
    if (isSingleVar)
    {
        submitString = string.Format("return dist3('frmV',{0});", l);
        actionString = "pvOut.aspx";
    }
    else
    {
        submitString = string.Format("return mz('frmV',{0},{1})", c, l);
        actionString = "tw.aspx";
    }

    if (bEn)
    {
        chString = "Show chart";
    }
    else
    {
        chString = "Tampilkan dengan chart";
    }

		%>
<!-- BEGIN OF Tree  -->
<!-- BEGIN OF Result -->
<%  if (isValidRequest) { %>
<form id="Form1" runat="server">
	<p><uc1:panelresulttoolbar id="PanelResultToolBar1" runat="server"></uc1:panelresulttoolbar></p>
	<table id="Table1" cellspacing="1" cellpadding="1" border="1">
		<tbody>
			<tr>
				<td colspan="3"></td>
			</tr>
			<tr>
				<td colspan="3"></td>
			</tr>
			<tr valign="top">
				<td valign="top" width="209">
					<table cellspacing="0" cellpadding="0" border="0">
						<tr>
							<td><asp:datagrid id="DataGrid1" runat="server" enableviewstate="False" width="139px" cellpadding="3"
									backcolor="White" borderwidth="1px" borderstyle="Solid" bordercolor="Thistle" autogeneratecolumns="False"
									cssclass="pnlCnt">
									<footerstyle forecolor="#4A3C8C" backcolor="#B5C7DE"></footerstyle>
									<selecteditemstyle font-bold="True" forecolor="#F7F7F7" backcolor="#738A9C"></selecteditemstyle>
									<alternatingitemstyle backcolor="#F7F7F7"></alternatingitemstyle>
									<itemstyle forecolor="#4A3C8C" backcolor="#E7E7FF"></itemstyle>
									<headerstyle font-bold="True" forecolor="DarkBlue" backcolor="#EEEEFF"></headerstyle>
									<pagerstyle horizontalalign="Right" forecolor="Blue" backcolor="#E7E7FF" mode="NumericPages"></pagerstyle>
								</asp:datagrid><asp:literal id="literalFooter" runat="server"></asp:literal></td>
						</tr>
					</table>
					<asp:literal id="Literal2" runat="server"></asp:literal></td>
				<td valign="top" colspan="2"><chart:webchartviewer id="WebChartViewer1" runat="server" enableviewstate="False" visible="False"></chart:webchartviewer><br>
					<br>
					<chart:webchartviewer id="Webchartviewer2" runat="server" enableviewstate="False" visible="False"></chart:webchartviewer></td>
			</tr>
			<tr>
				<td width="208" colspan="3">
					<p></p>
				</td>
			</tr>
			<tr>
				<td width="208" colspan="3"></td>
			</tr>
		</tbody>
	</table>
</form>
<%} %>
<!-- END OF Result --><a name="varsel"></a>
<form id="frmV" name="frmV" onsubmit="return mz('frmV',1,0)" action="tw.aspx" method="get">
	<uc1:panelgenerictext id="pTextTw" runat="server"></uc1:panelgenerictext>
	<table class="pnlCnt" id="Table2" style="OVERFLOW: scroll" cellspacing="1" cellpadding="1"
		border="0">
		<tbody>
			<tr>
				<td bgcolor="#dfdfe7" colspan="2" align="center"><b>Dataset: </b>
					<uc1:treeds id="TreeDs1" runat="server"></uc1:treeds></td>
			</tr>
			<tr>
				<td style="HEIGHT: 23px" colspan="2"><uc1:treevars id="TreeVars1" runat="server"></uc1:treevars></td>
			</tr>
			<tr>
				<td colspan="2"></td>
			</tr>
			<tr>
				<td align="center" bgcolor="#dfdfe7" colspan="2"><input type="hidden" name="r"> <input type="hidden" name="d">
					<input type=hidden value="<%= iDs %>" name=ds 
      > <input type="hidden" value="1" name="c"> <label for="ch"><input id="ch" type="checkbox" checked value="1" name="ch"><%= chString %>
					</label>
				</td>
			</tr>
			<tr>
				<td align="center" bgcolor="#dfdfe7" colspan="2"><input id="btnPrev" onclick="history.go(-1)" type="button" value="<< Back" name="btnPrev"
						runat="server">&nbsp;&nbsp;<input id="btnNext" type="submit" value="Next >>" name="btnNext" runat="server">
				</td>
			</tr>
		</tbody>
	</table>
</form>
