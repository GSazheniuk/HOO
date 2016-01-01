using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOO.Core.Model.Universe;
using HOO.ComLib;

namespace HOO.WebClient
{

	public partial class Galaxies : System.Web.UI.Page
	{
		protected void gvGalaxies_OnRowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				Literal lt = (Literal)e.Row.FindControl("ltLink");
                Literal ltS = (Literal)e.Row.FindControl("ltStarCount");
				long uId = ((Galaxy)e.Row.DataItem)._id;
				lt.Text = "<a href='/StarData.aspx?gid=" + uId.ToString () + "'>&gt;&gt;</a>";
                ltS.Text = ((Galaxy)e.Row.DataItem).Stars.Count.ToString();
            }
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            int uid = 0;

            if (Request["uid"] != null && int.TryParse(Request["uid"], out uid))
            {
                IHOOService Channel = BackServiceHelper.ConnectToBackService();

                Log.Logger log = new Log.Logger("HOO.WebClient", this.GetType());
                log.Entry.MethodName = "Page_Load";

                log.Entry.StepName = "Loading Universes from DB.";
                try
                {
                    Session["Universe"] = Channel.GetUniverseById(uid);
                    gvGalaxies.DataSource = ((Universe)Session["Universe"]).Galaxies;
                    gvGalaxies.DataBind();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }
	}
}

