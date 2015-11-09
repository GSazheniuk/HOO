
namespace HOO.WebClient
{
	using System;
	using System.Web;
	using System.Web.UI;
	using HOO.SvcLib.Helpers;

	public partial class Login : System.Web.UI.Page
	{
		protected void btnLogin_Click(object sender, EventArgs e)
		{
			PlayerHelper ph = new PlayerHelper ();
			ph.AuthUser (tbUsername.Text, tbPassword.Text);
			Session ["Player"] = ph.Player;
			Response.Redirect ("~/StarData.aspx");
		}
	}
}

