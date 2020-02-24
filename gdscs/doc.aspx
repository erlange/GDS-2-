<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="doc.aspx.cs" Inherits="gds.PptPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="pnl" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">
                    &nbsp;
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
            <form id="Form1" method="post" runat="server">
                <asp:LinkButton ID="btnAdd" runat="server" ToolTip="Add New Documents" Visible="False"
                    CausesValidation="False"><img src="imgedit/add.gif" border="0" />Add Documents</asp:LinkButton>
                <asp:DataGrid ID="DataGrid1" runat="server" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="1px" BackColor="White" CellPadding="3" CssClass="H11v" AutoGenerateColumns="False"
                    AllowSorting="True" onitemdatabound="DataGrid1_ItemDataBound">
                    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                    <EditItemStyle VerticalAlign="Top"></EditItemStyle>
                    <AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
                    <ItemStyle ForeColor="#000066" VerticalAlign="Top"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink ID="btnEdit" runat="server" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.documentid","AdminEditDoc.aspx?id={0}") %>'
                                    ImageUrl="imgedit/edit.gif">Edit</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                    Text="Delete">
																		<img border="0" src="images/delete.gif" title="Delete this record" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False" HeaderText="DocumentId">
                            <ItemTemplate>
                                <asp:Label ID="lblDocumentId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.documentid") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.documentid") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No.">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# DataBinder.Eval(Container, "ItemIndex") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="title" HeaderText="Title">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblDocTitle" runat="server" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'
                                    Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
                                </asp:HyperLink>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="url" HeaderText="Filename">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblUrl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.url" ) %>'
                                    NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
                                </asp:HyperLink>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.url") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="desc" HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Size">
                            <ItemTemplate>
                                <asp:Label ID="lblSize" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.size") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Type">
                            <ItemTemplate>
                                <asp:Image ID="imgType" runat="server"></asp:Image>&nbsp;
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Visible" Visible=false>
                            <ItemTemplate>
                                <asp:Image ID="imgVisible" runat="server"></asp:Image>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                    </PagerStyle>
                </asp:DataGrid>
                </form>
            </td>
        </tr>
    </table>
</asp:Content>
