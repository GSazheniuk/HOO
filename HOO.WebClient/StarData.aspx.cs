namespace HOO.WebClient
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using HOO.Core.Model.Universe;
    using HOO.Core.Model.Configuration;
    using HOO.Core.Model;
    using HOO.SvcLib.Helpers;
    using ComLib;
    using System.Linq;

    public partial class StarData : System.Web.UI.Page
	{
		private Star s;
//		private UniverseHelper _uh;
		private StarHelper _sh;
		private Universe ActiveUniverse;
		private Player ActivePlayer;
//		private MySqlDBHelper dh;
//		private int uId = 0;
		private long _gid;
		private string[] _starColors = {"#3366FF", "#6699FF", "#99CCFF", "#66FFFF", "#FFFF66", "#FFCC00", "#FF9900"};
        private IHOOService Channel;
        private Log.Logger log;

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

                ltScript.Text += String.Format("x{0} = {1}*10; y{0} = {2}*10; z{0} = {3}*10; m{0} = \"{4}\"; \r\n", e.Row.RowIndex + 2, s.Coordinates.X - s2.Coordinates.X
                    , s.Coordinates.Y - s2.Coordinates.Y, s.Coordinates.Z - s2.Coordinates.Z, s2.Class);
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

                        string attributes = "";
                        foreach (OAttribute oa in p.Attributes)
                        {
                            if (oa.AttributeType == AttributeType.Attribute)
                                switch (oa.Attribute)
                                {
                                    case ObjectAttribute.BasePopulation:
                                        attributes += String.Format("<font style='color:DarkGrey'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseFarming:
                                        attributes += String.Format("<font style='color:Green'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseProduction:
                                        attributes += String.Format("<font style='color:Orange'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseResearch:
                                        attributes += String.Format("<font style='color:LightBlue'>{0}</font>", oa.Value);
                                        break;
                                }
                        }
                        attributes = "(" + attributes + ")";
                        ltBaseAttrs.Text = attributes;
                        //ltBaseAttrs.Text = String.Format ("(<font style='color:DarkGrey'>{0}</font>, <font style='color:Orange'>{1}</font>, <font style='color:Green'>{2}</font>, <font style='color:LightBlue'>{3}</font>)"
                        //                                  , p.Attributes.ValueOf(HOO.Core.Model.ObjectAttribute.BasePopulation, HOO.Core.Model.AttributeType.Attribute)
                        //                                  , p.Attributes.ValueOf(HOO.Core.Model.ObjectAttribute.BaseProduction, HOO.Core.Model.AttributeType.Attribute)
                        //                                  , p.Attributes.ValueOf(HOO.Core.Model.ObjectAttribute.BaseFarming, HOO.Core.Model.AttributeType.Attribute)
                        //                                  , p.Attributes.ValueOf(HOO.Core.Model.ObjectAttribute.BaseResearch, HOO.Core.Model.AttributeType.Attribute));
                    }

					if (e.Row.DataItem is GasGiant) {
                        var g = (GasGiant)e.Row.DataItem;
                        ltName.Text = "Gas Giant";
						ltSize.Text = g.Size.ToString();
						ltClass.Text = g.Class.ToString();

                        string attributes = "";
                        foreach (OAttribute oa in g.Attributes)
                        {
                            if (oa.AttributeType == AttributeType.Attribute)
                                switch (oa.Attribute)
                                {
                                    case ObjectAttribute.BaseAmoniaExtraction:
                                        attributes += String.Format("<font style='color:DarkGreen'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseCarbonExtraction:
                                        attributes += String.Format("<font style='color:DarkGrey'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseMetalExtraction:
                                        attributes += String.Format("<font style='color:Grey'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseSilicateExtraction:
                                        attributes += String.Format("<font style='color:Silver'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseWaterExtraction:
                                        attributes += String.Format("<font style='color:Blue'>{0}</font>,", oa.Value);
                                        break;
                                }
                        }
                        attributes = "(" + (String.IsNullOrEmpty(attributes) ? "-" : attributes.Remove(attributes.Length - 1)) + ")";
                        ltBaseAttrs.Text = attributes;
                    }

                    if (e.Row.DataItem is AsteroidBelt) {
                        var a = (AsteroidBelt)e.Row.DataItem;
                        ltName.Text = "Asteroid Belt";
						ltSize.Text = a.Density.ToString();
						ltClass.Text = a.Type.ToString();

                        string attributes = "";
                        foreach (OAttribute oa in a.Attributes)
                        {
                            if (oa.AttributeType == AttributeType.Attribute)
                                switch (oa.Attribute)
                                {
                                    case ObjectAttribute.BaseBasaltMining:
                                        attributes += String.Format("<font style='color:DarkRed'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseCarbonMining:
                                        attributes += String.Format("<font style='color:DarkGrey'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseMetalMining:
                                        attributes += String.Format("<font style='color:Grey'>{0}</font>,", oa.Value);
                                        break;
                                    case ObjectAttribute.BaseSilicateMining:
                                        attributes += String.Format("<font style='color:Silver'>{0}</font>,", oa.Value);
                                        break;
                                }
                        }
                        attributes = "(" + (String.IsNullOrEmpty(attributes) ? "-" : attributes.Remove(attributes.Length - 1)) + ")";
                        ltBaseAttrs.Text = attributes;
                    }
                }
			}
		}

        protected void btnNewStar_Click(object sender, EventArgs e)
        {
            LoadStarData(true);
        }

		protected void btnTurn_Click(object sender, EventArgs e)
		{
			//UniverseHelper uh = new UniverseHelper ();
			//uh.Universe = ActiveUniverse;
			//uh.Tick ();
			//PlayerHelper ph = new PlayerHelper ();
			//ph.Player = ActivePlayer;
			//ph.Tick ();
//			Star st = uh.Universe.Galaxies [0].Stars.First (s => s.OrbitalBodies.Exists(ob => !ob.IsSaved));
			LoadStarData (false);
//			DBCommandResult res = dh.EndTurn (7);
//			if (res.ResultCode == 0) {
//				Galaxy g = new Galaxy ();
//			}
		}

		protected void btnNextStar_Click(object sender, EventArgs e)
		{
			LoadStarData (false);
		}

		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);

			if (Session ["Universe"] != null) {
				this.ActiveUniverse = (Universe)Session ["Universe"];
				this._gid = this.ActiveUniverse.Galaxies [0]._id;
			}

			if (Session ["Player"] != null) {
				this.ActivePlayer = (Player)Session ["Player"];
			}
			if (Request ["gid"] != null && long.TryParse (Request ["gid"], out _gid)) {
			}

            Channel = BackServiceHelper.ConnectToBackService();
            log = new Log.Logger("HOO.WebClient", this.GetType());
            ltScript.Text = "<script>\r\n";
        }

        private void LoadStarData(bool newStar){
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			if (this.ActiveUniverse != null && this.ActiveUniverse.Galaxies != null) {
                var g = ActiveUniverse.Galaxies.Single(x => x.OBID == _gid);
                if (newStar)
                {
                    s = Channel.GenerateNewStar(ActiveUniverse._id, this._gid);
                }
                else
                {
                    List<Star> stars = g.Stars;
                    s = stars.ToArray()[MrRandom.rnd.Next(stars.Count)];
                }
				ltStarName.Text = String.Format("<font style='color:{0}'>{1}</font>", _starColors[(int)s.Class], s.StarSystemName);
				ltStarClass.Text = s.ClassName;
				ltX.Text = s.Coordinates.X.ToString ();
				ltY.Text = s.Coordinates.Y.ToString ();
				ltZ.Text = s.Coordinates.Z.ToString ();
				imgStar.Src = String.Format("Images/Stars/{0}.png", s.Class);

				ltUniverse.Text = ActiveUniverse.Name;
				ltUniverseTick.Text = ActiveUniverse.CurrentTick.ToString ();
				ltUniverseTurn.Text = ActiveUniverse.CurrentTurn.ToString ();
				ltUniversePeriod.Text = ActiveUniverse.CurrentPeriod.ToString ();
                ltGalaxy.Text = g.Name;
                ltScript.Text += String.Format("m1 = \"{0}\"; c1 = '{1}';\r\n", s.Class, _starColors[(int)s.Class]);

                gvNearestStars.DataSource = ActiveUniverse.Galaxies[0].Stars.Where (p => p._id != s._id).OrderBy (p => Math.Sqrt (Math.Pow (s.Coordinates.X - p.Coordinates.X, 2) + Math.Pow (s.Coordinates.Y - p.Coordinates.Y, 2) + Math.Pow (s.Coordinates.Z - p.Coordinates.Z, 2))).Take (5);
                gvNearestStars.DataBind ();
                /*
                  if (!s.IsLoaded) {
                      _sh = new StarHelper (s);
                      _sh.RefreshOrbitalBodies ();
                      StarOrbitalBodyHelper sobh = new StarOrbitalBodyHelper ();
                      foreach (StarOrbitalBody sob in s.OrbitalBodies) {
                          sobh.OrbitalBody = sob;
                          sobh.LoadOrbitalBody ();
                      }
                  }
                */              
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
            ltScript.Text += "</script>\r\n";
        }

        protected void Page_Load (object sender, EventArgs e)
		{
			if (!IsPostBack) {
				LoadStarData (false);
			}
		}
	}
}

