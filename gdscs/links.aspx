<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="links.aspx.cs" Inherits="gds.LinksPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp; Links</div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <asp:HyperLink ID="btnEditRecords" runat="server" CssClass="H11v" NavigateUrl="AdminEditLinks.aspx"
                    ToolTip="Edit content"><img src="imgedit/tbledit.gif" border="0" />Edit Data</asp:HyperLink>
                <asp:DataList ID="DataList1" runat="server" BorderColor="#CC9966" BorderStyle="None"
                    BackColor="White" CellPadding="4" GridLines="Both" BorderWidth="0px" OnItemDataBound="DataList1_ItemDataBound">
                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                    <ItemStyle ForeColor="#330099" CssClass="H11v" BackColor="White"></ItemStyle>
                    <ItemTemplate>
                        <asp:HyperLink ID="lblTitle" runat="server" CssClass="H11vb" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'
                            NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
                        </asp:HyperLink><!--
						<asp:label id="lblBR2" runat="server" cssclass="H11b" enableviewstate="False">
							<br />
						</asp:label>
						--><br>
                        <asp:HyperLink ID="lblUrl" runat="server" CssClass="H11b" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'
                            NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
                        </asp:HyperLink><br>
                        <asp:Label ID="lblDesc" runat="server" CssClass="H9" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
                        </asp:Label><br>
                        <asp:Label ID="lblSubmitDate" runat="server" CssClass="H11b" Text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
                        </asp:Label>:
                        <asp:Label ID="lblDate" runat="server" CssClass="H11b" Text='<%# DataBinder.Eval(Container, "DataItem.submitDate") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
                </asp:DataList>
            </td>
        </tr>
    </table>
</asp:Content>
