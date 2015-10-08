<%@ Page Language="C#" Inherits="HOO.WebClient.StarData" MasterPageFile="~/Main.master" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">
<table border="1">
<tr style="height:25px;">
	<td rowspan="2">
		<table border="0">
		<tr>
			<td>-<asp:Literal id="ltStarName" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td>-<asp:Literal id="ltStarClass" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td>----------------</td>
		</tr>
		<tr>
			<td>X: <asp:Literal id="ltX" runat="server"></asp:Literal></td>
		</tr>
		<tr>
			<td>Y: <asp:Literal id="ltY" runat="server"></asp:Literal></td>
		</tr>
		<tr>
			<td>Z: <asp:Literal id="ltZ" runat="server"></asp:Literal></td>
		</tr>
		</table>
	</td>
	<td>--Orbital Bodies--</td>
</tr>
<tr><td>List of Bodies here...</td></tr>
</table>
</asp:Content>