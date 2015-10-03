<%@ Page Language="C#" Inherits="HOO.WebClient.Default" MasterPageFile="~/Main.master" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">

<asp:GridView id="gvUniverses" runat="server" AutoGenerateColumns="false" OnRowCreated="gvUniverses_OnRowCreated">
<Columns>
	<asp:BoundField DataField="Name" HeaderText="Name" />
	<asp:BoundField DataField="Descrip" HeaderText="Description" />
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltLink" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView>
</asp:Content>
