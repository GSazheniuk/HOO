using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOO.Core.Model.Configuration.Enums;
using HOO.Core.Model;

namespace HOO.DB
{
	public partial class MySqlDBHelper
	{
		public DBCommandResult LoadOrbitalBody(StarOrbitalBody sob)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetOrbitalBodyById", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spSOBID = new MySqlParameter("pSOBId", sob.OBID);
			com.Parameters.Add(spSOBID);
			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				sob.OBID = Convert.ToInt32(dr["OBID"]);
				sob.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);

				sob.Attributes = new Attributes();
				sob.Attributes.ParentObject = sob;
				sob.Attributes.Load(LoadAttributes(ds.Tables[1]));

				res.Tag = sob;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult SaveOrbitalBody(StarOrbitalBody sob)
		{
			return SaveAttributes(sob);
		}
	}
}

