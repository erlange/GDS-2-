<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="q.aspx.cs" Inherits="gds.SearchResultPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
		<form id="Form1" method="post" runat="server">

                <table id="Table1" style="width: 408px; height: 32px" cellspacing="1" cellpadding="1"
                    width="408" border="0">
                    <tr>
                        <td class="H9b" style="width: 130px" align="right">
                            <asp:Label ID="lblSearch" runat="server"></asp:Label>:
                        </td>
                        <td style="width: 70px">
                            <asp:TextBox ID="TextBox1" runat="server" Width="227px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnOK" runat="server" ImageUrl="images/go.jpg" 
                                onclick="btnOK_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:Label ID="lblResult" runat="server" CssClass="H11vb"></asp:Label><asp:DataGrid
                    ID="DataGrid1" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BackColor="White"
                    CellPadding="3" BorderWidth="1px" AllowPaging="True" AutoGenerateColumns="False"
                    Visible="False" Width="100%" onitemdatabound="DataGrid1_ItemDataBound" 
                    onpageindexchanged="DataGrid1_PageIndexChanged">
                    <FooterStyle HorizontalAlign="Center" ForeColor="#000066" BackColor="White"></FooterStyle>
                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                    <ItemStyle ForeColor="#000066"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699">
                    </HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:HyperLink ID="lblDesc" runat="server" CssClass="H11vb" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.var_id") %>'
                                    Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'></asp:HyperLink><asp:HyperLink
                                        ID="lblDescEn" runat="server" CssClass="H11vb" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.var_id") %>'
                                        Text='<%# DataBinder.Eval(Container, "DataItem.desc_en") %>'></asp:HyperLink><br>
                                <asp:Label ID="lblQ" runat="server" CssClass="H9" Text='<%# DataBinder.Eval(Container, "DataItem.q") %>'></asp:Label><asp:Label
                                    ID="lblQEn" runat="server" CssClass="H9" Text='<%# DataBinder.Eval(Container, "DataItem.q_en") %>'></asp:Label><br>
                                <asp:HyperLink ID="lblVarId" runat="server" CssClass="H11b" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.var_id") %>'
                                    Text='<%# DataBinder.Eval(Container, "DataItem.var_id") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Position="TopAndBottom" BackColor="White"
                        CssClass="H9b" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
                </form>
            </td>
        </tr>
    </table>
</asp:Content>
