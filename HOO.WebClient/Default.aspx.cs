
namespace HOO.WebClient
{
	using System;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using HOO.Core;
	using HOO.DB;

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
				int uId = ((HOO.Core.Model.Universe.Universe)e.Row.DataItem).Id;
				lt.Text = "<a href='/Galaxies.aspx?uid=" + uId.ToString () + "'>&gt;&gt;</a>";
			}
		}

		protected void Page_Load (object sender, EventArgs e)
		{
			MySqlDBHelper dh = new MySqlDBHelper(SensitiveData.ConnectionString);

			DBCommandResult res = dh.GetAllUniverses ();
			if (res.ResultCode == 0) {
				gvUniverses.DataSource = res.Tag;
				gvUniverses.DataBind ();


			}
		}

	}
}

