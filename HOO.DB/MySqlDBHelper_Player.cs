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
		public DBCommandResult AddNewPlayer(string userName, string password, string email, Player p)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_AddPlayer", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spUsername = new MySqlParameter("pUserName", userName);
			MySqlParameter spEmail = new MySqlParameter("pEmail", email);
			MySqlParameter spPassword = new MySqlParameter("pPassword", password);
			MySqlParameter spLeaderName = new MySqlParameter("pLeaderName", p.LeaderName);
			MySqlParameter spRace = new MySqlParameter("pRace", p.Race);
			MySqlParameter spMotto = new MySqlParameter("pMotto", p.Motto);
			MySqlParameter spColor = new MySqlParameter("pColor", p.Color);
			com.Parameters.AddRange(new MySqlParameter[] {spUsername, spEmail, spPassword, spLeaderName, spRace, spMotto, spColor});

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				p.OBID = Convert.ToInt32(dr["OBID"]);
				p.LeaderName = Convert.ToString(dr["LeaderName"]);
				p.Race = Convert.ToString(dr["Race"]);
				p.Motto = Convert.ToString(dr["Motto"]);
				p.Color = Convert.ToString(dr["Color"]);

				res = SaveBaseProperties(p);
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult AuthPlayer(string userName, string password)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("GM_AuthPlayer", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spUsername = new MySqlParameter("pUserName", userName);
			MySqlParameter spPassword = new MySqlParameter("pPassword", password);

			com.Parameters.AddRange(new MySqlParameter[] {spUsername, spPassword});

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				Player p = new Player();
				p.OBID = Convert.ToInt32(dr["OBID"]);
				p.LeaderName = Convert.ToString(dr["LeaderName"]);
				p.Race = Convert.ToString(dr["Race"]);
				p.Motto = Convert.ToString(dr["Motto"]);
				p.Color = Convert.ToString(dr["Color"]);

				foreach (DataRow aRow in ds.Tables[1].Rows)
				{
					p.Attributes.Add(Convert.ToInt32(aRow["AttributeID"]), aRow["Value"]); 
				}

				foreach (DataRow eRow in ds.Tables[2].Rows)
				{
					p.Effects.Add(Convert.ToInt32(eRow["AttributeID"]), eRow["Value"]);
				}

				foreach (DataRow eRow in ds.Tables[3].Rows)
				{
					p.Requisites.Add(Convert.ToInt32(eRow["RequisiteID"]), eRow["Value"]);
				}

				res.Tag = p;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}
	}
}