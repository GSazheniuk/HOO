using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOO.Core.Model.Configuration.Enums;

namespace HOO.DB
{
	public class MySqlDBHelper
	{
		#region Private Fields
		private MySqlDataGate _dg;
		#endregion

		#region Constructors
		public MySqlDBHelper()
		{

		}

		public MySqlDBHelper(string connStr)
		{
			_dg = new MySqlDataGate(connStr);
		}
		#endregion

		#region Public Methods
		public DBCommandResult GetAllUniverses()
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetAllUniverses", _dg.Connection);

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				List<Universe> unis = new List<Universe>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Universe u = new Universe();
					u.Id = Convert.ToInt32(dr["UniverseID"]);
					u.Descrip = Convert.ToString(dr["Description"]);
					u.Name = Convert.ToString(dr["Name"]);
					unis.Add(u);
				}
				res.Tag = unis;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult LoadUniverse(Universe u)
		{
			DBCommandResult res = new DBCommandResult();

			MySqlCommand com = new MySqlCommand("ADM_LoadUniverse", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spUID = new MySqlParameter("pUID", u.Id);
			com.Parameters.Add(spUID);
			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				Universe resU = new Universe();
				resU.Id = Convert.ToInt32(dr["Id"]);
				resU.Name = Convert.ToString(dr["Name"]);
				resU.Descrip = Convert.ToString(dr["Description"]);
				res.Tag = resU;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex)
			{
				res.ResultCode = -2;
				res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null)?ex.InnerException.Message:"");
			}
			return res;
		}

		public DBCommandResult GetAllGalaxies(int uId)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetAllGalaxies", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spUID = new MySqlParameter("pUId", uId);
			com.Parameters.Add(spUID);

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				List<Galaxy> gals = new List<Galaxy>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Galaxy g = new Galaxy();
					g.Id = Convert.ToInt32(dr["GalaxyId"]);
					g.Name = Convert.ToString(dr["Name"]);
					g.DimensionX = Convert.ToInt32(dr["DimX"]);
					g.DimensionY = Convert.ToInt32(dr["DimY"]);
					g.DimensionZ = Convert.ToInt32(dr["DimZ"]);

					gals.Add(g);
				}
				res.Tag = gals;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		#endregion
	}
}
