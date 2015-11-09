<%@ Page Language="C#" Inherits="HOO.WebClient.Login" MasterPageFile="~/Main.master" EnableViewState="false" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">
<div class="row">
<br />
</div>
<div class="row">
	<div class="col-md-offset-3 col-md-2"><label for="tbUsername" >Username : </label></div>
	<div class="col-md-4"><asp:TextBox id="tbUsername" runat="server" placeholder="Enter username..." CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-offset-3 col-md-2"><label for="tbPassword" >Password : </label></div>
	<div class="col-md-4"><asp:TextBox id="tbPassword" runat="server" placeholder="Enter password..." TextMode="Password" CssClass="form-control" /></div>
</div>
<hr />
<div class="row">
	<div class="col-md-12">
	<asp:Button id="btnLogin" runat="server" CssClass="btn btn-success" Text="Login" OnClick="btnLogin_Click"/>
	<a href="StarData.aspx" class="btn btn-info" >Back to Stars</a>
	</div>
</div>
</asp:Content>