
namespace HOO.WebClient
{
    using System;
    using System.Web;
    using System.Web.UI;
    using HOO.SvcLib.Helpers;
    using ComLib;

    public partial class Login : System.Web.UI.Page
	{
		protected void btnLogin_Click(object sender, EventArgs e)
		{
            IHOOService Channel = BackServiceHelper.ConnectToBackService();
			
			Session ["Player"] = Channel.AuthPlayer(Session.SessionID, tbUsername.Text, tbPassword.Text);
            Response.Redirect ("~/StarData.aspx");
		}
	}
}

