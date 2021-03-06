<%@ Page Language="C#" Inherits="HOO.WebClient.Default" MasterPageFile="~/Main.master" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">

<asp:GridView id="gvUniverses" runat="server" AutoGenerateColumns="false" OnRowCreated="gvUniverses_OnRowCreated" CssClass="table">
<Columns>
    <asp:BoundField DataField="Name" />
    <asp:BoundField DataField="Description" />
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltLink" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView>
</asp:Content>
