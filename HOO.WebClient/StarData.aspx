<%@ Page Language="C#" Inherits="HOO.WebClient.StarData" MasterPageFile="~/Main.master" EnableViewState="false" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">
<table border="1">
<tr style="height:25px;">
	<td rowspan="2" width="250px">
		<table border="0" width="100%">
		<tr>
			<td colspan="3">-<asp:Literal id="ltUniverse" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td>Tick-<asp:Literal id="ltUniverseTick" runat="server"></asp:Literal>-</td>
			<td>Turn-<asp:Literal id="ltUniverseTurn" runat="server"></asp:Literal>-</td>
			<td>Period-<asp:Literal id="ltUniversePeriod" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3">----------------</td>
		</tr>
		<tr>
			<td colspan="3">-<asp:Literal id="ltGalaxy" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3">----------------</td>
		</tr>
		<tr>
			<td colspan="3">-<asp:Literal id="ltStarName" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3">-<asp:Literal id="ltStarClass" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3">----------------</td>
		</tr>
		<tr>
			<td>X: <asp:Literal id="ltX" runat="server"></asp:Literal></td>
			<td>Y: <asp:Literal id="ltY" runat="server"></asp:Literal></td>
			<td>Z: <asp:Literal id="ltZ" runat="server"></asp:Literal></td>
		</tr>
		</table>
	</td>
	<td width="450px">--Orbital Bodies--</td>
</tr>
<tr><td><asp:GridView id="gvOrbits" runat="server" AutoGenerateColumns="false" ShowHeader="true" OnRowCreated="gvOrbits_OnRowCreated" BorderWidth="0" Width="100%">
<Columns>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltOrbitNo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltName" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltSize" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltClass" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltBaseAttrs" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView></td></tr>
<tr><td colspan="2">
<asp:GridView id="gvNearestStars" runat="server" AutoGenerateColumns="false" ShowHeader="true" OnRowCreated="gvNearestStars_OnRowCreated" BorderWidth="0" Width="100%">
<Columns>
	<asp:TemplateField HeaderText="Name">
		<ItemTemplate>
			<asp:Literal id="ltName2" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Class">
		<ItemTemplate>
			<asp:Literal id="ltClass2" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
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
<tr><td colspan="2"><asp:Button id="btnNextStar" runat="server" Text="Show Random Star" OnClick="btnNextStar_Click" /><asp:Button id="btnTurn" runat="server" OnClick="btnTurn_Click" Text="End Turn" /></td></tr>
</table>
<br />
<asp:Literal id="ltLoadTime" runat="server" />
</asp:Content>