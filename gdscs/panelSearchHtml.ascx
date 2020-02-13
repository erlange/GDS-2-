<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelSearchHtml.ascx.cs" Inherits="gds.panelSearchHtml" %>
<form name="frmQ" action="q.aspx" method="get" onsubmit="if(document.forms['frmQ'].q.value.length==0)return false;">
<table id=Table1 cellSpacing=1 cellPadding=1 align=center border=0>
  <tr>
    <td class=H9b colSpan=2>
    <asp:label id=lblSearch runat="server"></asp:label>
    </TD></TR>
  <tr>
    <td vAlign=top>
    <input type="text" name="q" id="txtQ" />
    </TD>
    <td vAlign=top>
    <input type="image" src="images/go.jpg" />
    </td>
    </tr>
    </table>
</form>