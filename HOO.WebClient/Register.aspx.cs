
namespace HOO.WebClient
{
    using System;
    using System.Web;
    using System.Web.UI;
    using HOO.SvcLib.Helpers;
    using ComLib;
    using Core.Model;

    public partial class Register : System.Web.UI.Page
	{
		protected void btnRegister_Click(object sender, EventArgs e)
		{
            IHOOService Channel = BackServiceHelper.ConnectToBackService();

            Player p = new Player();

            p.LeaderName = tbLeaderName.Text;
            p.Race = tbRace.Text;
            p.Motto = tbMotto.Text;
            p.Color = tbColor.Text;
            p.Username = tbUsername.Text;
            p.Password = tbPassword.Text;
            p.Email = tbEmail.Text;

            Session["Player"] = Channel.RegisterNewPlayer(p);
        }
	}
}

