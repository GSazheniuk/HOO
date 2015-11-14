<%@ Control Language="C#" Inherits="HOO.WebClient.PlayerInfoPanel" %>
<asp:Panel id="pControl" runat="server">
	<asp:Panel id="pInfo" runat="server">
	<table class="table table-condensed">
	<tr>
		<td colspan="2"><asp:Literal id="ltPlayerName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td colspan="2"><asp:Literal id="ltMotto" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td>Native Credits :</td>
		<td><asp:Literal id="ltTotalCredits" runat="server"></asp:Literal>(<asp:Literal id="ltCreditsChange" runat="server"></asp:Literal>)</td>
	</tr>
	</table>
	</asp:Panel>
	<asp:Panel id="pLoginRegister" runat="server">
	<a href="Login.aspx" >Login</a> / <a href="Register.aspx" >Register</a>
	</asp:Panel>
</asp:Panel>