<%@ Page Language="C#" Inherits="HOO.WebClient.Register" MasterPageFile="~/Main.master" EnableViewState="false" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">
<div class="row">
	<div class="col-md-4"><label for="tbUsername" >Username : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbUsername" runat="server" placeholder="Enter username..." CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-4"><label for="tbPassword" >Password : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbPassword" runat="server" placeholder="Enter password..." TextMode="Password" CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-4"><label for="tbPassword2" >Confirm Password : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbPassword2" runat="server" placeholder="Repeat password..." TextMode="Password" CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-4"><label for="tbEmail" >E-Mail : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbEmail" runat="server" placeholder="Enter E-Mail..." CssClass="form-control" /></div>
</div>
<hr />
<div class="row">
	<div class="col-md-4"><label for="tbLeaderName" >Leader Name : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbLeaderName" runat="server" placeholder="Enter Leader Name..." CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-4"><label for="tbRace" >Race Name : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbRace" runat="server" placeholder="Enter Race Name..." CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-4"><label for="tbMotto" >Your Motto : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbMotto" runat="server" placeholder="Enter Your Motto..." CssClass="form-control" /></div>
</div>
<br />
<div class="row">
	<div class="col-md-4"><label for="tbColor" >Player Color : </label></div>
	<div class="col-md-8"><asp:TextBox id="tbColor" runat="server" placeholder="Enter Color Code..." CssClass="form-control" /></div>
</div>
<hr />
<div class="row">
	<div class="col-md-12">
	<asp:Button id="btnRegister" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnRegister_Click"/>
	<a href="StarData.aspx" class="btn btn-info" >Back to Stars</a>
	</div>
</div>
</asp:Content>