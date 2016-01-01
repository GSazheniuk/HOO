
namespace HOO.WebClient
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using HOO.Core;
    using ComLib;

    public partial class Default : System.Web.UI.Page
	{
		protected void Page_Init(object sender, EventArgs e)
		{

		}

		protected void gvUniverses_OnRowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType == DataControlRowType.DataRow)
			{
				Literal lt = (Literal)e.Row.FindControl("ltLink");
                long uId = ((HOO.Core.Model.Universe.Universe)e.Row.DataItem)._id;
				lt.Text = "<a href='/Galaxies.aspx?uid=" + uId.ToString () + "'>&gt;&gt;</a>";
			}
		}

		protected void Page_Load (object sender, EventArgs e)
		{
            Log.Logger log = new Log.Logger("HOO.WebClient", this.GetType());
            log.Entry.MethodName = "Page_Load";
            log.Entry.StepName = "Log started.";
            log.Debug("Another log online message.");

            IHOOService Channel = BackServiceHelper.ConnectToBackService();

            log.Entry.StepName = "Loading Universes from DB.";
            try
            {
                var allUniverses = Channel.GetAllUniverses();
                gvUniverses.DataSource = allUniverses;
                gvUniverses.DataBind();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            /*
			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);

			DBCommandResult res = dh.GetAllUniverses ();
			if (res.ResultCode == 0) {
				gvUniverses.DataSource = res.Tag;
				gvUniverses.DataBind ();
			}
            */
		}
	}
}

