<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelResultToolBar.ascx.cs" Inherits="gds.panelResultToolBar" %>
<a href="#varsel"><img border="0" src="images/tree.gif"><asp:label id="lblVarSel" runat="server">Pilih Variabel Lain</asp:label></a>&nbsp;&nbsp;
<asp:linkbutton id="btnExcel" runat="server" causesvalidation="False" 
    tooltip="Download Excel File" onclick="btnExcel_Click"><img src="imgedit/xls.gif" border="0" />Download Excel</asp:linkbutton>&nbsp;&nbsp;
<a id="btnPrint2" runat="server" href="javascript:void(null);" onmouseover="window.status='Print'" onmouseout="window.status=''"><img border="0" src="imgedit/print.gif"><asp:label id="Label1" runat="server">Print</asp:label></a>&nbsp;&nbsp;
