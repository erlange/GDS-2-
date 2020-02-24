<%@ Page Title="" Language="C#" MasterPageFile="~/left.master" AutoEventWireup="true"
    CodeBehind="gallery.aspx.cs" Inherits="gds.GalleryPage" %>

<%@ Register TagPrefix="uc1" TagName="album" Src="album.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="pnl" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="pnlHdr">
                <div class="pnlHdrDiv">
                    <img height="8" src="images/dots.gif" width="8">
                    &nbsp;&nbsp;Gallery
                </div>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                <uc1:album ID="Album1" runat="server"></uc1:album>
            </td>
        </tr>
        <tr>
            <td class="pnlCnt">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
