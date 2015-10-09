<%@ Page Language="C#" Inherits="HOO.WebClient.StarData" MasterPageFile="~/Main.master" EnableViewState="false" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">
<table border="1">
<tr style="height:25px;">
	<td rowspan="2">
		<table border="0" width="100%">
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
<tr><td colspan="2">
<asp:GridView id="gvNearestStars" runat="server" AutoGenerateColumns="false" ShowHeader="true" OnRowCreated="gvNearestStars_OnRowCreated" BorderWidth="0">
<Columns>
	<asp:BoundField DataField="StarSystemName" HeaderText="Name" />
	<asp:BoundField DataField="ClassName" HeaderText="Class" />
	<asp:TemplateField HeaderText="X">
		<ItemTemplate>
			<asp:Literal id="ltXCoo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Y">
		<ItemTemplate>
			<asp:Literal id="ltYCoo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Z">
		<ItemTemplate>
			<asp:Literal id="ltZCoo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Distance">
		<ItemTemplate>
			<asp:Literal id="ltDistance" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView>
</td></tr>
<tr><td colspan="2"><input type="submit" value="Show Random Star" /></td></tr>
</table>
</asp:Content>