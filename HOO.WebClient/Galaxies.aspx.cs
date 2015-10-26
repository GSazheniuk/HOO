using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOO.Core.Model.Universe;
using HOO.SvcLib.Helpers;
using HOO.Core.Configuration;

namespace HOO.WebClient
{

	public partial class Galaxies : System.Web.UI.Page
	{
		protected void gvGalaxies_OnRowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				Literal lt = (Literal)e.Row.FindControl("ltLink");
				int uId = ((HOO.Core.Model.Universe.Galaxy)e.Row.DataItem).Id;
				lt.Text = "<a href='/StarData.aspx?gid=" + uId.ToString () + "'>&gt;&gt;</a>";
			}
		}

		protected void Page_Load (object sender, EventArgs e)
		{
//			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);

			int uid = 0;

			if (Request ["uid"] != null && int.TryParse (Request ["uid"], out uid)) {
				if (Session ["Universe"] == null || ((Universe)Session ["Universe"]).Id != uid) {
					UniverseHelper uh = new UniverseHelper ();
					uh.Universe = new Universe ();
					uh.Universe.Id = uid;
					uh.Load ();
					Session ["Universe"] = uh.Universe;
				}

				gvGalaxies.DataSource = ((Universe)Session ["Universe"]).Galaxies;
				gvGalaxies.DataBind ();

				/*
				DBCommandResult res = dh.GetAllGalaxies (uid);
				if (res.ResultCode == 0) {
					gvGalaxies.DataSource = res.Tag;
					gvGalaxies.DataBind ();
				}
*/
			}
		}
	}
}

