<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mnuTop.ascx.cs" Inherits="gds.mnuTop" %>
<table id="logo" cellspacing="0" cellpadding="0" width="100%" bgcolor="#abaebf" border="0">
	<tbody>
		<tr>
			<td bgcolor="#abaebf"><asp:repeater id="Repeater1" runat="server" 
                    onitemdatabound="Repeater1_ItemDataBound">
					<headertemplate>
						<table cellspacing="0" cellpadding="0" bgcolor="#abaebf" border="0">
							<tr>
					</headertemplate>
					<itemtemplate>
						<td id="tdl" runat="server" 
			width='<%# DataBinder.Eval(Container, "DataItem.leftwidth") %>' 
			background='<%# DataBinder.Eval(Container, "DataItem.leftbgurl") %>' 
			bgcolor='<%# DataBinder.Eval(Container, "DataItem.leftbgcolor") %>' 
			height='<%# DataBinder.Eval(Container, "DataItem.height") %>' >
						</td>
						<td id="tdc" runat="server" 
			width='<%# DataBinder.Eval(Container, "DataItem.width") %>' 
			class='<%# DataBinder.Eval(Container, "DataItem.class") %>' 
			style='<%# DataBinder.Eval(Container, "DataItem.style") %>' 
			bgcolor='<%# DataBinder.Eval(Container, "DataItem.bgcolor") %>' 
			height='<%# DataBinder.Eval(Container, "DataItem.height") %>' >
							<a runat="server" id="lblTitle"  
				class='<%# DataBinder.Eval(Container, "DataItem.linkclass") %>' 
				style='<%# DataBinder.Eval(Container, "DataItem.linkstyle") %>' 
				href='<%# DataBinder.Eval(Container, "DataItem.href") %>' 
				title='<%# DataBinder.Eval(Container, "DataItem.linktooltip") %>' 
				></a>
						</td>
						<td id="tdr" runat="server" 
		width='<%# DataBinder.Eval(Container, "DataItem.rightwidth") %>' 
		background='<%# DataBinder.Eval(Container, "DataItem.rightbgurl") %>' 
		bgcolor='<%# DataBinder.Eval(Container, "DataItem.rightbgcolor") %>' 
		height='<%# DataBinder.Eval(Container, "DataItem.height") %>' >
							<asp:label id="lblId" runat="server" visible="False">
								<%# DataBinder.Eval(Container, "DataItem.id") %>
							</asp:label>
						</td>
					</itemtemplate>
					<footertemplate>
		</tr>
</table>
</footertemplate> </asp:repeater></TD></TR>
<tr>
	<td id="tdLogo" width="775" style="BACKGROUND-ATTACHMENT: fixed" bgcolor="white">
		<a href="default.aspx"><asp:image id="Image1" runat="server" ></asp:image></a></td>
</tr>
</TBODY></TABLE>
