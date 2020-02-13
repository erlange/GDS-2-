<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelHighlight.ascx.cs" Inherits="gds.panelHighlight" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<chart:webchartviewer id="chart1" runat="server" enableviewstate="False"></chart:webchartviewer>
<table id="Table1" cellspacing="1" cellpadding="1" border="0" width="100%">
	<tr>
		<td class="H11" colspan="2" style="HEIGHT: 23px">
			<asp:dropdownlist id="lstIsland" runat="server" cssclass="H11" width="100%"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td class="H11" style="WIDTH: 100%">
			<asp:dropdownlist id="lstVar" runat="server" cssclass="H11" width="100%"></asp:dropdownlist></td>
		<td>
			<asp:imagebutton id="btnOK" runat="server" imageurl="images/go.jpg" causesvalidation="False"></asp:imagebutton></td>
	</tr>
</table>
