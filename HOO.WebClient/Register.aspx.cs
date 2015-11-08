
namespace HOO.WebClient
{
	using System;
	using System.Web;
	using System.Web.UI;
	using HOO.SvcLib.Helpers;

	public partial class Register : System.Web.UI.Page
	{
		protected void btnRegister_Click(object sender, EventArgs e)
		{
			PlayerHelper ph = new PlayerHelper ();
			ph.Player.LeaderName = tbLeaderName.Text;
			ph.Player.Race = tbRace.Text;
			ph.Player.Motto = tbMotto.Text;
			ph.Player.Color = tbColor.Text;

			ph.Register (tbUsername.Text, tbPassword.Text, tbEmail.Text);
		}
	}
}

