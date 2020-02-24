<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelPpt.ascx.cs" Inherits="gds.panelPpt" %>
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
			<td class="pnlCnt" colspan="2"><asp:hyperlink id="btnEditRecords" runat="server" tooltip="Edit content" cssclass="H11v" navigateurl="doc.aspx?id=10"><img src="imgedit/tbledit.gif" border="0" />Edit Data</asp:hyperlink>
                <asp:datalist id="DataList1" runat="server" bordercolor="#CC9966" 
                    borderstyle="None" cellpadding="4"
					gridlines="Both" borderwidth="0px" onitemdatabound="DataList1_ItemDataBound">
					<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
					<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
					<ItemStyle ForeColor="#330099" CssClass="H11v"></ItemStyle>
					<ItemTemplate>
						<asp:label id="lblDate" runat="server" cssclass="H11b" text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
						</asp:label><br>
						<asp:hyperlink id="lblUrl" runat="server" cssclass="H11vb" text='<%# DataBinder.Eval(Container, "DataItem.title") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
						</asp:hyperlink>
						<asp:label id="lblBR1" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label><!--
						<asp:label id="lblBR2" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label>
						-->
						<asp:label id="lblDesc" runat="server" cssclass="H11v" text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
						</asp:label>
					</ItemTemplate>
					<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
				</asp:datalist></td>
		</tr>
		<tr>
			<td class="pnlCnt" colspan="2" align="right">
				<asp:hyperlink id="lblGoToDoc" runat="server" navigateurl="doc.aspx?id=10">Pustaka Paper dan Presentasi ...</asp:hyperlink></td>
		</tr>
	</tbody>
</table>
