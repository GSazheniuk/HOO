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
	using HOO.Core.Configuration;
	using HOO.SvcLib.Helpers;

	public partial class StarData : System.Web.UI.Page
	{
		private Star s;
		private UniverseHelper _uh;
		private StarHelper _sh;
//		private MySqlDBHelper dh;
//		private int uId = 0;

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

		protected void gvOrbits_OnRowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				Literal ltOrbitNo = (Literal)e.Row.FindControl("ltOrbitNo");
				Literal ltName = (Literal)e.Row.FindControl("ltName");
				Literal ltSize = (Literal)e.Row.FindControl("ltSize");
				Literal ltClass = (Literal)e.Row.FindControl("ltClass");

				ltOrbitNo.Text = e.Row.RowIndex.ToString ();
				if (e.Row.DataItem == null) {
					ltName.Text = "--Empty--";
					ltSize.Text = "";
					ltClass.Text = "";
				} else {
					ltName.Text = e.Row.DataItem.GetType().ToString();
					ltSize.Text = "";
					ltClass.Text = "";

					if (e.Row.DataItem is Planet) {
						ltName.Text = ((Planet)e.Row.DataItem).PlanetFriendlyName;
						ltSize.Text = ((Planet)e.Row.DataItem).Size.ToString();
						ltClass.Text = ((Planet)e.Row.DataItem).Type.ToString();
					}

					if (e.Row.DataItem is GasGiant) {
						ltName.Text = "Gas Giant";
						ltSize.Text = ((GasGiant)e.Row.DataItem).Size.ToString();
						ltClass.Text = ((GasGiant)e.Row.DataItem).Class.ToString();
					}

					if (e.Row.DataItem is AsteroidBelt) {
						ltName.Text = "Asteroid Belt";
						ltSize.Text = ((AsteroidBelt)e.Row.DataItem).Density.ToString();
						ltClass.Text = ((AsteroidBelt)e.Row.DataItem).Type.ToString();
					}
				}
			}
		}

		protected void btnTurn_Click(object sender, EventArgs e)
		{
//			DBCommandResult res = dh.EndTurn (7);
//			if (res.ResultCode == 0) {
//				Galaxy g = new Galaxy ();
//			}
		}

		protected override void OnInit (EventArgs e)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			base.OnInit (e);
			int gid = 0;

			if (Request ["gid"] != null && int.TryParse (Request ["gid"], out gid)) {
				_uh = new UniverseHelper ();
				_uh.Universe.Id = 7;
				_uh.Load();

				if (_uh.Universe.Galaxies != null) {
					var g = _uh.Universe.Galaxies.Single (x => x.Id == gid);
					List<Star> stars = g.Stars;
					s = stars.ToArray()[MrRandom.rnd.Next(stars.Count)];
					ltStarName.Text = s.StarSystemName;
					ltStarClass.Text = s.ClassName;
					ltX.Text = s.Coordinates.X.ToString ();
					ltY.Text = s.Coordinates.Y.ToString ();
					ltZ.Text = s.Coordinates.Z.ToString ();

					gvNearestStars.DataSource = stars.Where(p=>p.Id != s.Id).OrderBy (p => Math.Sqrt (Math.Pow (s.Coordinates.X - p.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - p.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - p.Coordinates.Z, 2))).Take(5);
					gvNearestStars.DataBind ();
					_sh = new StarHelper (s);
					_sh.RefreshOrbitalBodies ();
					StarOrbitalBody[] sobs = new StarOrbitalBody[10];
					foreach (StarOrbitalBody sob in _sh.Star.OrbitalBodies) {
						sobs [sob.OrbitNo] = sob;
					}
					gvOrbits.DataSource = sobs;
					gvOrbits.DataBind ();
				}
			}
			sw.Stop ();
			TimeSpan ts = sw.Elapsed;
			ltLoadTime.Text = String.Format ("loaded in {0}ms", ts);
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

