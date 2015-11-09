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
//		private UniverseHelper _uh;
		private StarHelper _sh;
		private Universe ActiveUniverse;
//		private MySqlDBHelper dh;
//		private int uId = 0;
		private int _gid;
		private string[] _starColors = {"#3366FF", "#6699FF", "#99CCFF", "#66FFFF", "#FFFF66", "#FFCC00", "#FF9900"};

		protected void gvNearestStars_OnRowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				Star s2 = (Star)e.Row.DataItem;
				double dist = Math.Sqrt (Math.Pow (s.Coordinates.X - s2.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - s2.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - s2.Coordinates.Z, 2));

				Literal ltName = (Literal)e.Row.FindControl("ltName2");
				ltName.Text = String.Format("<font style='color:{0}'>{1}</font>", _starColors[(int)s2.Class], s2.StarSystemName);
				Literal ltClass = (Literal)e.Row.FindControl("ltClass2");
				ltClass.Text = s2.ClassName;
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
				Literal ltBaseAttrs = (Literal)e.Row.FindControl("ltBaseAttrs");

				ltOrbitNo.Text = e.Row.RowIndex.ToString ();
				if (e.Row.DataItem == null) {
					ltName.Text = "--Empty--";
					ltSize.Text = "";
					ltClass.Text = "";
					ltBaseAttrs.Text = "";
				} else {
					ltName.Text = e.Row.DataItem.GetType().ToString();
					ltSize.Text = "";
					ltClass.Text = "";
					ltBaseAttrs.Text = "";

					if (e.Row.DataItem is Planet) {
						var p = (Planet)e.Row.DataItem;
						ltName.Text = p.PlanetFriendlyName;
						ltSize.Text = p.Size.ToString();
						ltClass.Text = p.Type.ToString();
						ltBaseAttrs.Text = String.Format ("(<font style='color:DarkGrey'>{0}</font>, <font style='color:Orange'>{1}</font>, <font style='color:Green'>{2}</font>, <font style='color:LightBlue'>{3}</font>)"
						                                  , p.Attributes.BasePopulation, p.Attributes.BaseProduction
						                                  , p.Attributes.BaseFarming, p.Attributes.BaseResearch);
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
			UniverseHelper uh = new UniverseHelper ();
			uh.Universe = ActiveUniverse;
			uh.Tick ();
//			Star st = uh.Universe.Galaxies [0].Stars.First (s => s.OrbitalBodies.Exists(ob => !ob.IsSaved));
			LoadStarData ();
//			DBCommandResult res = dh.EndTurn (7);
//			if (res.ResultCode == 0) {
//				Galaxy g = new Galaxy ();
//			}
		}

		protected void btnNextStar_Click(object sender, EventArgs e)
		{
			LoadStarData ();
			//			DBCommandResult res = dh.EndTurn (7);
			//			if (res.ResultCode == 0) {
			//				Galaxy g = new Galaxy ();
			//			}
		}

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

			if (Session ["Universe"] != null) {
				this.ActiveUniverse = (Universe)Session ["Universe"];
				this._gid = this.ActiveUniverse.Galaxies [0].OBID;
			}
			if (Request ["gid"] != null && int.TryParse (Request ["gid"], out _gid)) {
/*				_uh = new UniverseHelper ();
				_uh.Universe.Id = 7;
				_uh.Load();*/
			}
		}

		private void LoadStarData(){
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			if (this.ActiveUniverse != null && this.ActiveUniverse.Galaxies != null) {
				var g = ActiveUniverse.Galaxies.Single (x => x.OBID == _gid);
				List<Star> stars = g.Stars;
				s = stars.ToArray () [MrRandom.rnd.Next (stars.Count)];
				ltStarName.Text = String.Format("<font style='color:{0}'>{1}</font>", _starColors[(int)s.Class], s.StarSystemName);
				ltStarClass.Text = s.ClassName;
				ltX.Text = s.Coordinates.X.ToString ();
				ltY.Text = s.Coordinates.Y.ToString ();
				ltZ.Text = s.Coordinates.Z.ToString ();

				ltUniverse.Text = ActiveUniverse.Name;
				ltUniverseTick.Text = ActiveUniverse.CurrentTick.ToString ();
				ltUniverseTurn.Text = ActiveUniverse.CurrentTurn.ToString ();
				ltUniversePeriod.Text = ActiveUniverse.CurrentPeriod.ToString ();
				ltGalaxy.Text = g.Name;

				gvNearestStars.DataSource = stars.Where (p => p.OBID != s.OBID).OrderBy (p => Math.Sqrt (Math.Pow (s.Coordinates.X - p.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - p.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - p.Coordinates.Z, 2))).Take (5);
				gvNearestStars.DataBind ();

				if (!s.IsLoaded) {
					_sh = new StarHelper (s);
					_sh.RefreshOrbitalBodies ();
					StarOrbitalBodyHelper sobh = new StarOrbitalBodyHelper ();
					foreach (StarOrbitalBody sob in s.OrbitalBodies) {
						sobh.OrbitalBody = sob;
						sobh.LoadOrbitalBody ();
					}
				}

				StarOrbitalBody[] sobs = new StarOrbitalBody[10];
				foreach (StarOrbitalBody sob in s.OrbitalBodies) {
					sobs [sob.OrbitNo] = sob;
				}
				gvOrbits.DataSource = sobs;
				gvOrbits.DataBind ();
			}
			sw.Stop ();
			TimeSpan ts = sw.Elapsed;
			ltLoadTime.Text = String.Format ("loaded in {0}ms", ts);
		}

		protected void Page_Load (object sender, EventArgs e)
		{
			if (!IsPostBack) {
				LoadStarData ();
			}

			if (Session ["Player"] != null) {
				btnRegister.Visible = btnLogin.Visible = false;
			}
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

