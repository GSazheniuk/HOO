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
		public DBCommandResult SaveBaseProperties(BaseObject bo)
		{
			DBCommandResult res = new DBCommandResult ();

			try {
				//Save Attributes
				for (int i = 0; i<bo.Attributes.Count; i++) {
					int key = bo.Attributes.Keys [i];
					//throw new Exception (String.Format("{0}. {1}, {2}, {3}", bo.ObjectType, bo.OBID, key, bo.Attributes [key]));
					MySqlCommand com = new MySqlCommand ("GM_SaveObjectAttribute", _dg.Connection);
					com.CommandType = CommandType.StoredProcedure;
					MySqlParameter spAttrId = new MySqlParameter ("pAttrId", key);
					MySqlParameter spObjectType = new MySqlParameter ("pObjectType", bo.ObjectType);
					MySqlParameter spObjectId = new MySqlParameter ("pObjectId", bo.OBID);
					MySqlParameter spValue = new MySqlParameter ("pValue", bo.Attributes [key]);

					com.Parameters.AddRange (new MySqlParameter[] { spAttrId, spObjectType, spObjectId, spValue });
					_dg.ExecuteCommand (com);
				}

				//Save Effects
				for (int i = 0; i<bo.Effects.Count; i++) {
					int key = bo.Effects.Keys [i];
					MySqlCommand com = new MySqlCommand ("GM_SaveObjectEffect", _dg.Connection);
					com.CommandType = CommandType.StoredProcedure;
					MySqlParameter spAttrId = new MySqlParameter ("pAttrId", key);
					MySqlParameter spObjectType = new MySqlParameter ("pObjectType", bo.ObjectType);
					MySqlParameter spObjectId = new MySqlParameter ("pObjectId", bo.OBID);
					MySqlParameter spValue = new MySqlParameter ("pValue", bo.Effects [key]);

					com.Parameters.AddRange (new MySqlParameter[] { spAttrId, spObjectType, spObjectId, spValue });
					_dg.ExecuteCommand (com);
				}

				//Save Requisites
				for (int i = 0; i<bo.Requisites.Count; i++) {
					int key = bo.Requisites.Keys [i];
					MySqlCommand com = new MySqlCommand ("GM_SaveObjectRequisite", _dg.Connection);
					com.CommandType = CommandType.StoredProcedure;
					MySqlParameter spReqId = new MySqlParameter ("pReqId", key);
					MySqlParameter spObjectType = new MySqlParameter ("pObjectType", bo.ObjectType);
					MySqlParameter spObjectId = new MySqlParameter ("pObjectId", bo.OBID);
					MySqlParameter spValue = new MySqlParameter ("pValue", bo.Requisites [key]);

					com.Parameters.AddRange (new MySqlParameter[] { spReqId, spObjectType, spObjectId, spValue });
					_dg.ExecuteCommand (com);
				}

				res.Tag = bo;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			} catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}
	}
}

