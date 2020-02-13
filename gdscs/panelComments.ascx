<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelComments.ascx.cs" Inherits="gds.panelComments" %>
<table class="pnl" cellspacing="0" cellpadding="0" width="100%" bgcolor="white" border="0">
	<tbody>
		<tr>
			<td class="pnlHdr">
				<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;<asp:label id="lblTitle" runat="server"></asp:label>
				</div>
			</td>
			<td class="pnlHdr" align="right"><asp:hyperlink id="btnEdit" runat="server" tooltip="Edit content" cssclass="pnlBtn">
						<img src="imgedit/edit.gif" border="0" />Edit</asp:hyperlink></td>
		</tr>
		<tr>
			<td class="pnlCnt" colspan="2"><asp:hyperlink id="btnEditRecords" runat="server" tooltip="Edit content" cssclass="H11v" navigateurl="AdminEditCommentData.aspx"><img src="imgedit/tbledit.gif" border="0" />Edit Comments</asp:hyperlink>
                <asp:datalist id="DataList1" runat="server" bordercolor="#CC9966" 
                    borderstyle="None" backcolor="White"
					cellpadding="4" gridlines="Both" borderwidth="0px" 
                    onitemdatabound="DataList1_ItemDataBound">
					<selecteditemstyle font-bold="True" forecolor="#663399" backcolor="#FFCC66"></selecteditemstyle>
					<footerstyle forecolor="#330099" backcolor="#FFFFCC"></footerstyle>
					<itemstyle forecolor="#330099" cssclass="H11v" backcolor="White"></itemstyle>
					<itemtemplate>
						<asp:label id="lblDate" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.submitdate") %>'>
						</asp:label><br>
						<asp:label id="lblFrom" runat="server" cssclass="H11b"></asp:label>&nbsp;
						<asp:hyperlink id="lblEmail" runat="server" cssclass="H11vb" navigateurl='<%# "mailto:" + DataBinder.Eval(Container, "DataItem.email") %>' text='<%# DataBinder.Eval(Container, "DataItem.sender") %>'>
						</asp:hyperlink>
						<asp:label id="lblBR1" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label>
						<!--
						<asp:label id="lblBR2" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label>
						-->
						<asp:label id="lblComment" runat="server" cssclass="H11v" text='<%# DataBinder.Eval(Container, "DataItem.comment") %>'>
						</asp:label>
					</itemtemplate>
					<headerstyle font-bold="True" forecolor="#FFFFCC" backcolor="#990000"></headerstyle>
				</asp:datalist></td>
		</tr>
		<tr>
			<td class="pnlCnt" colspan="2"><asp:label id="lblContent" runat="server"></asp:label></td>
		</tr>
	</tbody>
</table>
