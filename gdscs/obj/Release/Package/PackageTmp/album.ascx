<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="album.ascx.cs" Inherits="gds.album" %>
<asp:datalist id="DataList1" backcolor="#CCCCFF" repeatdirection="Horizontal" cellspacing="2"
	cellpadding="2" horizontalalign="Center" repeatcolumns="3" runat="server" 
    ondeletecommand="DataList1_DeleteCommand" 
    onitemdatabound="DataList1_ItemDataBound">
	<alternatingitemstyle horizontalalign="Center" verticalalign="Middle"></alternatingitemstyle>
	<itemstyle horizontalalign="Center" verticalalign="Middle" backcolor="#CCCCFF"></itemstyle>
	<itemtemplate>
		<table id="Table5" style="FILTER: progid:DXImageTransform.Microsoft.dropshadow(OffX=2, OffY=2, Color='gray', Positive='true')"
			height="100%" cellspacing="0" cellpadding="5" width="100%" bgcolor="#ccccff" border="0">
			<tr>
				<td class="H9t" valign="top" align="center" width="90" bgcolor="#ffffff" height="90">
					<asp:hyperlink id="btnEdit" runat="server" Visible="False" tooltip="Edit description of this picture" navigateurl='<%# DataBinder.Eval(Container, "DataItem.documentid", "AdminEditImg.aspx?id={0}") %>'>
													<img src="imgedit/edit.gif" border="0" />Edit
												</asp:hyperlink>&nbsp;
					<asp:linkbutton id="btnDelete" runat="server" tooltip="Delete Image" visible="False" commandname="Delete"
						causesvalidation="False"><img src="images/delete.gif" border="0" />Delete</asp:linkbutton><br>
					<asp:hyperlink id="lblImg" runat="server" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.documentid", "imgd.aspx?id={0}") %>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.titlealt") %>'>
						<asp:Image runat="server" ID="img1" ImageUrl='<%# DataBinder.Eval(Container, &quot;DataItem.documentid&quot;, &quot;ShowImg.aspx?id={0}&amp;t=1&quot;) %>'>
						</asp:image>
					</asp:hyperlink></td>
			</tr>
			<tr>
				<td valign="top" align="center" bgcolor="#ffffff">
					<asp:hyperlink id="lblUrl" runat="server" cssclass="H9tb" text='<%# DataBinder.Eval(Container, "DataItem.url") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.documentid", "imgd.aspx?id={0}") %>'>
					</asp:hyperlink><br>
					<asp:label id="lblTitle" runat="server" cssclass="H10" text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
					</asp:label></td>
			</tr>
		</table>
	</itemtemplate>
</asp:datalist>
