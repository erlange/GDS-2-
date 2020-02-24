<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="imgd.aspx.cs" Inherits="gds.ImagePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FormImg1" method="post" runat="server">
    <table class="pnl" id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">&nbsp;&nbsp;
                    <asp:Label ID="lblHeader" runat="server"></asp:Label></div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="gallery.aspx"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt" align="center">
                <asp:Label ID="lblTitle" runat="server" CssClass="H4" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>'></asp:Label><br>
                <asp:HyperLink ID="lblFilename" runat="server" Target="_blank"></asp:HyperLink><br>
                <asp:Label ID="lblSize" runat="server" CssClass="H9b"></asp:Label><br>
                <br>
                <asp:Label ID="lblClick" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
                </asp:Label><br>
                <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"></asp:ImageButton><br>
                <br>
                <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desc") %>'>
                </asp:Label><br>
                <br>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
