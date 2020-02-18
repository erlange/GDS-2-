<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="treeScoreOut.ascx.cs" Inherits="gds.treeScoreOut" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="uc1" TagName="panelResultToolBar" Src="../panelResultToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="panelGenericText" Src="../panelGenericText.ascx" %>
<form id="frmSrv" runat="server">
	<uc1:panelgenerictext id="pTextSc" runat="server"></uc1:panelgenerictext><br>
	<asp:label id="lblError" runat="server" forecolor="Red"></asp:label>
	<table id="Table1" cellspacing="1" cellpadding="1" border="0">
		<tbody>
			<tr>
				<td colspan="2" valign="top" class="H11vb">
					<uc1:panelresulttoolbar id="PanelResultToolBar1" runat="server"></uc1:panelresulttoolbar></td>
				</TD>
			</tr>
			<tr>
				<td valign="top">
					<asp:datagrid id="DataGrid2" runat="server" enableviewstate="False" cssclass="pnlCnt" caption="test"
						autogeneratecolumns="False" gridlines="Horizontal" cellpadding="3" backcolor="White" borderwidth="1px"
						borderstyle="None" bordercolor="#E7E7FF">
						<footerstyle forecolor="#4A3C8C" backcolor="#B5C7DE"></footerstyle>
						<selecteditemstyle font-bold="True" forecolor="#F7F7F7" backcolor="#738A9C"></selecteditemstyle>
						<alternatingitemstyle backcolor="#F7F7F7"></alternatingitemstyle>
						<itemstyle forecolor="#4A3C8C" backcolor="#E7E7FF"></itemstyle>
						<headerstyle font-bold="True" horizontalalign="Center" backcolor="#EEEEFF"></headerstyle>
						<pagerstyle horizontalalign="Right" forecolor="#4A3C8C" backcolor="#E7E7FF" mode="NumericPages"></pagerstyle>
					</asp:datagrid>
				</td>
				<td valign="top">
					<% if (Request.QueryString["ch"]=="1") { %>
					<chart:webchartviewer id="WebChartViewer1" runat="server" enableviewstate="False"></chart:webchartviewer>
					<% } %>
					<br>
					<asp:literal id="Literal2" runat="server"></asp:literal>
				</td>
			</tr>
			<tr>
				<td colspan="2"><asp:literal id="Literal1" runat="server" enableviewstate="False"></asp:literal></td>
			</tr>
		</tbody>
	</table>
</form>
