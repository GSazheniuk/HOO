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
		private Star s;
		protected void gvNearestStars_OnRowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				Star s2 = (Star)e.Row.DataItem;
				double dist = Math.Sqrt (Math.Pow (s.Coordinates.X - s2.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - s2.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - s2.Coordinates.Z, 2));

				Literal ltX = (Literal)e.Row.FindControl("ltXCoo");
				ltX.Text = s2.Coordinates.X.ToString();
				Literal ltY = (Literal)e.Row.FindControl("ltYCoo");
				ltY.Text = s2.Coordinates.Y.ToString();
				Literal ltZ = (Literal)e.Row.FindControl("ltZCoo");
				ltZ.Text = s2.Coordinates.Z.ToString();
				Literal ltD = (Literal)e.Row.FindControl("ltDistance");
				ltD.Text = dist.ToString();
			}
		}

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			int gid = 0;

			if (Request ["gid"] != null && int.TryParse (Request ["gid"], out gid)) {
				DBCommandResult res = dh.GetAllStars(gid);
				if (res.ResultCode == 0) {
					List<Star> stars = (List<Star>)res.Tag;
					s = stars.ToArray()[MrRandom.rnd.Next(stars.Count)];
					ltStarName.Text = s.StarSystemName;
					ltStarClass.Text = s.ClassName;
					ltX.Text = s.Coordinates.X.ToString ();
					ltY.Text = s.Coordinates.Y.ToString ();
					ltZ.Text = s.Coordinates.Z.ToString ();

					gvNearestStars.DataSource = stars.Where(p=>p.Id != s.Id).OrderBy (p => Math.Sqrt (Math.Pow (s.Coordinates.X - p.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - p.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - p.Coordinates.Z, 2))).Take(5);
					gvNearestStars.DataBind ();
					s = new Star ();
				}
			}
		}

		protected void Page_Load (object sender, EventArgs e)
		{
/*			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			int gid = 0;

			if (Request ["gid"] != null && int.TryParse (Request ["gid"], out gid)) {
				DBCommandResult res = dh.GetAllStars(gid);
				if (res.ResultCode == 0) {
					List<Star> stars = (List<Star>)res.Tag;
					s = stars.ToArray()[MrRandom.rnd.Next(stars.Count)];
					ltStarName.Text = s.StarSystemName;
					ltStarClass.Text = s.ClassName;
					ltX.Text = s.Coordinates.X.ToString ();
					ltY.Text = s.Coordinates.Y.ToString ();
					ltZ.Text = s.Coordinates.Z.ToString ();

					gvNearestStars.DataSource = stars.Where(p=>p.Id != s.Id).OrderBy (p => Math.Sqrt (Math.Pow (s.Coordinates.X - p.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - p.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - p.Coordinates.Z, 2))).Take(5);
					gvNearestStars.DataBind ();
					s = new Star ();
				}
			}*/
		}
	}
}

