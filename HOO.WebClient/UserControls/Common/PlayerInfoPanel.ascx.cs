namespace HOO.WebClient
{
	using System;
	using System.Web;
	using System.Web.UI;
	using HOO.Core.Model;

	public partial class PlayerInfoPanel : System.Web.UI.UserControl
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (Session ["Player"] != null) {
				pLoginRegister.Visible = false;
				pInfo.Visible = true;
				BindPlayerData ();
			} else {
				pLoginRegister.Visible = true;
				pInfo.Visible = false;
			}
		}

		private void BindPlayerData(){
			Player p = (Player)Session ["Player"];
			ltPlayerName.Text = String.Format ("<font style='color:{0}'>{1}</font>", p.Color, p.LeaderName);
			if (p.Attributes.ContainsAttribute (ObjectAttribute.NativeCredits, AttributeTypes.Resource)) {
				ltTotalCredits.Text = p.Attributes.ValueOf (ObjectAttribute.NativeCredits, AttributeTypes.Resource).ToString();
			}
			ltCreditsChange.Text = p.Attributes.ValueOf (ObjectAttribute.NativeCredits, AttributeTypes.ResourceFlatChange).ToString();
		}
	}
}

