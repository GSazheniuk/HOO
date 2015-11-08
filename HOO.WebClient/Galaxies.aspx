<%@ Page Language="C#" Inherits="HOO.WebClient.Galaxies"  MasterPageFile="~/Main.master" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">

<asp:GridView id="gvGalaxies" runat="server" AutoGenerateColumns="false" OnRowCreated="gvGalaxies_OnRowCreated" CssClass="table">
<Columns>
	<asp:BoundField DataField="Name" HeaderText="Name" />
	<asp:BoundField DataField="DimensionX" HeaderText="R(X)" />
	<asp:BoundField DataField="DimensionY" HeaderText="R(Y)" />
	<asp:BoundField DataField="DimensionZ" HeaderText="R(Z)" />
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltLink" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView>

</asp:Content>