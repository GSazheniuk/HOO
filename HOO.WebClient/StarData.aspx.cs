namespace HOO.WebClient
{
	using System;
	using System.Collections.Generic;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Linq;
	using HOO.Core.Model.Universe;
	using HOO.Core.Model.Configuration;
	using HOO.Core;
	using HOO.DB;
	using HOO.Core.Configuration;

	public partial class StarData : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			int gid = 0;

			if (Request ["gid"] != null && int.TryParse (Request ["gid"], out gid)) {
				DBCommandResult res = dh.GetAllStars(gid);
				if (res.ResultCode == 0) {
					List<Star> stars = (List<Star>)res.Tag;
					Star s = stars.ToArray()[MrRandom.rnd.Next(stars.Count)];
					ltStarName.Text = s.StarSystemName;
					ltStarClass.Text = s.ClassName;
					ltX.Text = s.Coordinates.X.ToString ();
					ltY.Text = s.Coordinates.Y.ToString ();
					ltZ.Text = s.Coordinates.Z.ToString ();
				}
			}
		}
	}
}

