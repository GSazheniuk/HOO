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

				foreach (DataRow aRow in ds.Tables[1].Rows)
				{
					sob.Attributes.Add(Convert.ToInt32(aRow["Attribute"]), aRow["Value"]); 
				}

				foreach (DataRow eRow in ds.Tables[2].Rows)
				{
					sob.Effects.Add(Convert.ToInt32(eRow["AttrId"]), eRow["Value"]);
				}

				//Requisites load here
				//TO-DO

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
			DBCommandResult res = new DBCommandResult ();

			try
			{
				//Save Attributes
				for (int i = 0; i<sob.Attributes.Count;i++)
				{
					int key = sob.Attributes.Keys[i];
					MySqlCommand com = new MySqlCommand("GM_SaveObjectAttribute", _dg.Connection);
					com.CommandType = CommandType.StoredProcedure;
					MySqlParameter spAttrId = new MySqlParameter("pAttrId", key);
					MySqlParameter spObjectType = new MySqlParameter("pObjectType", ObjectTypes.OrbitalBody);
					MySqlParameter spObjectId = new MySqlParameter("pObjectId", sob.OBID);
					MySqlParameter spValue = new MySqlParameter("pValue", sob.Attributes[key]);

					com.Parameters.AddRange (new MySqlParameter[] {spAttrId, spObjectType, spObjectId, spValue});
					_dg.ExecuteCommand(com);
				}

				//Save Effects
				for (int i = 0; i<sob.Effects.Count;i++)
				{
					int key = sob.Effects.Keys[i];
					MySqlCommand com = new MySqlCommand("GM_SaveObjectEffect", _dg.Connection);
					com.CommandType = CommandType.StoredProcedure;
					MySqlParameter spAttrId = new MySqlParameter("pAttrId", key);
					MySqlParameter spObjectType = new MySqlParameter("pObjectType", ObjectTypes.OrbitalBody);
					MySqlParameter spObjectId = new MySqlParameter("pObjectId", sob.OBID);
					MySqlParameter spValue = new MySqlParameter("pValue", sob.Effects[key]);

					com.Parameters.AddRange (new MySqlParameter[] {spAttrId, spObjectType, spObjectId, spValue});
					_dg.ExecuteCommand(com);
				}
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
				res.Tag = sob;
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}
	}
}

