<%@ Control Language="cs" AutoEventWireup="true" Inherits="gds.langBar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<!-- BEGIN ToolBar -->
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td height="28" background="images/graddk.jpg">
				&nbsp;&nbsp;Language:
				<select name="en" class="lst">
					<option value="0" selected>Bahasa Indonesia</option>
					<option value="1">English</option>
				</select><asp:Button ID="Button1" runat="server" 
                    Text="Button" />
            </td>
		</tr>
	</table>
<!-- END ToolBar -->
