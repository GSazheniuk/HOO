namespace HOO.WebClient
{
	using System;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using HOO.Core;
	using HOO.DB;

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
			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);

			int uid = 0;

			if (Request ["uid"] != null && int.TryParse (Request ["uid"], out uid)) {
				DBCommandResult res = dh.GetAllGalaxies (uid);
				if (res.ResultCode == 0) {
					gvGalaxies.DataSource = res.Tag;
					gvGalaxies.DataBind ();
				}
			}
		}
	}
}

