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
		public OAttribute[] LoadAttributes(DataTable table)
		{
			if (table != null) {
				OAttribute[] res = new OAttribute[table.Rows.Count];
				for (int i=0; i<res.Length; i++) {
					DataRow dr = table.Rows [i];
                    res[i] = new OAttribute() { Attribute = (ObjectAttribute)dr["AttributeID"], AttributeType = (AttributeType)dr["AttributeType"], Value = dr["Value"] };
				}
				return res;
			} else
				return new OAttribute[0];
		}

		public DBCommandResult SaveAttributes(BaseObject bo)
		{
			DBCommandResult res = new DBCommandResult ();

			try {
                //Save Attributes
    //            OAttribute[] list = bo.Attributes;//.ChangedAttributes ();
				//for (int i = 0; i<list.Length; i++) {
				//	OAttribute key = list [i];
				//	//throw new Exception (String.Format("{0}. {1}, {2}, {3}", bo.ObjectType, bo.OBID, key, bo.Attributes [key]));
				//	MySqlCommand com = new MySqlCommand ("GM_SaveAttribute", _dg.Connection);
				//	com.CommandType = CommandType.StoredProcedure;
				//	MySqlParameter spAId = new MySqlParameter ("pAID", key._id);
				//	MySqlParameter spAttrId = new MySqlParameter ("pAttrId", key.AttributeID);
				//	MySqlParameter spObjectType = new MySqlParameter ("pAttrType", key.AttributeType);
				//	MySqlParameter spObjectId = new MySqlParameter ("pObjectId", bo.OBID);
				//	MySqlParameter spValue = new MySqlParameter ("pValue", key.Value);

				//	com.Parameters.AddRange (new MySqlParameter[] { spAId, spAttrId, spObjectType, spObjectId, spValue });
				//	_dg.ExecuteCommand (com);
				//}

				//bo.Attributes.SaveAll();
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

