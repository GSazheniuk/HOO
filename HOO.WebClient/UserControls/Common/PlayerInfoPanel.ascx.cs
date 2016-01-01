namespace HOO.WebClient
{
	using System;
	using System.Web;
	using System.Web.UI;
	using HOO.Core.Model;
    using System.Linq;

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
			OnlinePlayer p = (OnlinePlayer)Session ["Player"];
			ltPlayerName.Text = String.Format ("<font style='color:{0}'>{1}</font>", p.Color, p.LeaderName);
            var nativeCredits = p.Attributes.FirstOrDefault(x => x.Attribute == ObjectAttribute.NativeCredits && x.AttributeType == AttributeType.Resource);
            var ncIncome = p.Attributes.FirstOrDefault(x => x.Attribute == ObjectAttribute.NativeCredits && x.AttributeType == AttributeType.ResourceFlatChange);

            if (nativeCredits != null)
            {
                ltTotalCredits.Text = nativeCredits.Value.ToString();
            }

            if (ncIncome != null)
            {
                ltCreditsChange.Text = ncIncome.Value.ToString();
            }
        }
	}
}

