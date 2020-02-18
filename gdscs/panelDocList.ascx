<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelDocList.ascx.cs" Inherits="gds.panelDocList" %>
<table class="pnl" cellspacing="0" cellpadding="0" width="100%" bgcolor="white" border="0">
	<tbody>
		<tr>
			<td class="pnlHdr">
				<div class="pnlHdrDiv"><img height="8" src="images/dots.gif" width="8"> &nbsp;<asp:label id="lblTitle" runat="server"></asp:label>
				</div>
			</td>
			<td class="pnlHdr" align="right"></td>
		</tr>
		<tr>
			<td class="pnlCnt" colspan="2">
				<asp:label id="lblNoData" runat="server"></asp:label>
				<asp:hyperlink id="btnEditRecords" runat="server" tooltip="Edit content" cssclass="H11v" navigateurl="doc.aspx?id=10"><img src="imgedit/tbledit.gif" border="0" />Edit Data</asp:hyperlink><br>
				<asp:datalist id="DataList1" runat="server" bordercolor="#CC9966" 
                    borderstyle="None" cellpadding="4"
					gridlines="Both" borderwidth="0px" onitemdatabound="DataList1_ItemDataBound">
					<selecteditemstyle font-bold="True" forecolor="#663399" backcolor="#FFCC66"></selecteditemstyle>
					<footerstyle forecolor="#330099" backcolor="#FFFFCC"></footerstyle>
					<itemstyle forecolor="#330099" cssclass="H11v"></itemstyle>
					<itemtemplate>
						<asp:label id="lblDocTitle" runat="server" cssclass="H11vb" text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
						</asp:label>:
						<asp:hyperlink id="lblUrl" runat="server" cssclass="H11vb" text='<%# DataBinder.Eval(Container, "DataItem.title") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
						</asp:hyperlink><!--
						<asp:label id="lblBR2" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label>
						--><br>
						<asp:label id="lblFilenameTitle" runat="server" cssclass="H11b">Filename</asp:label>:
						<asp:hyperlink id="lblFilename" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.title") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
						</asp:hyperlink>&nbsp;
						<asp:image id="Image1" runat="server" imageurl="imgedit/file.gif" width="16px" height="16px"></asp:image>(
						<asp:label id="lblSize" runat="server" cssclass="H11b"></asp:label>)<br>
						<asp:label id="lblDesc" runat="server" cssclass="H9" text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
						</asp:label><br>
						<asp:label id="lblSubmitDate" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
						</asp:label>:
						<asp:label id="Label2" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
						</asp:label>
					</itemtemplate>
					<separatortemplate>
						<hr width="100%" size="1">
					</separatortemplate>
					<headerstyle font-bold="True" forecolor="#FFFFCC" backcolor="#990000"></headerstyle>
				</asp:datalist></td>
		</tr>
		<tr>
			<td class="pnlCnt" colspan="2" align="right"></td>
		</tr>
	</tbody>
</table>
