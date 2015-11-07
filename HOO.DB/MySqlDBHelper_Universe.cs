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
					u.OBID = Convert.ToInt32(dr["OBID"]);
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

			MySqlCommand com = new MySqlCommand("ADM_GetUniverseById", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spUID = new MySqlParameter("pId", u.OBID);
			com.Parameters.Add(spUID);
			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				Universe resU = new Universe();
				resU.OBID = Convert.ToInt32(dr["OBID"]);
				resU.Name = Convert.ToString(dr["Name"]);
				resU.Descrip = Convert.ToString(dr["Description"]);
				resU.CurrentTick = Convert.ToInt32(dr["CurrentTick"]);
				resU.CurrentTurn = Convert.ToInt32(dr["CurrentTurn"]);
				resU.CurrentPeriod = Convert.ToInt32(dr["CurrentPeriod"]);

				foreach (DataRow aRow in ds.Tables[1].Rows)
				{
					resU.Attributes.Add(Convert.ToInt32(aRow["Attribute"]), aRow["Value"]); 
				}

				foreach (DataRow eRow in ds.Tables[2].Rows)
				{
					resU.Effects.Add(Convert.ToInt32(eRow["AttrId"]), eRow["Value"]);
				}

				//Requisites load here
				//TO-DO

				foreach (DataRow gRow in ds.Tables[4].Rows)
				{
					Galaxy g = new Galaxy();
					g.Universe = resU;
					g.OBID = Convert.ToInt32(gRow["OBID"]);
					g.Name = Convert.ToString(gRow["Name"]);
					g.DimensionX = Convert.ToInt32(gRow["DimX"]);
					g.DimensionY = Convert.ToInt32(gRow["DimY"]);
					g.DimensionZ = Convert.ToInt32(gRow["DimZ"]);
					resU.Galaxies.Add(g);
				}

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
					g.OBID = Convert.ToInt32(dr["OBID"]);
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

		public DBCommandResult AddStarName(string pName)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_AddStarName", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spName = new MySqlParameter("pName", pName);
			com.Parameters.Add(spName);

			try
			{
				_dg.ExecuteCommand(com);
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult GetStarOrbitalBodies(Star s)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetStarOrbitalBodies", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spStarId = new MySqlParameter("pStarId", s.OBID);
			com.Parameters.Add(spStarId);

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				s.OrbitalBodies = new List<StarOrbitalBody>();

				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					switch (Convert.ToInt32(dr["BodyType"]))
					{
					case 1:
						Planet p = new Planet(s);
						p.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
						p.Size = (PlanetSize)Convert.ToInt32(dr["Size"]);
						p.Type = (PlanetType)Convert.ToInt32(dr["Class"]);
						p.OBID = Convert.ToInt32(dr["OBID"]);
						s.OrbitalBodies.Add(p);
						break;
					case 2:
						GasGiant g = new GasGiant(s);
						g.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
						g.Size = (GasGiantSize)Convert.ToInt32(dr["Size"]);
						g.Class = (GasGiantClass)Convert.ToInt32(dr["Class"]);
						g.OBID = Convert.ToInt32(dr["OBID"]);
						s.OrbitalBodies.Add(g);
						break;
					case 3:
						AsteroidBelt a = new AsteroidBelt(s);
						a.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
						a.Density = (AsteroidDensity)Convert.ToInt32(dr["Size"]);
						a.Type = (AsteroidType)Convert.ToInt32(dr["Class"]);
						a.OBID = Convert.ToInt32(dr["OBID"]);
						s.OrbitalBodies.Add(a);
						break;
					}
				}

				res.ResultCode = 0;
				res.ResultMsg = "Ok";
				res.Tag = s;
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult EndTurn(int uId)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("GM_TickUniverse", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spUniverseId = new MySqlParameter("pUniverseId", uId);
			com.Parameters.Add(spUniverseId);

			try
			{
				_dg.ExecuteCommand(com);
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
				//res.Tag = s;
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult SaveUniverse(Universe u)
		{
			DBCommandResult res = new DBCommandResult ();
			MySqlCommand com = new MySqlCommand ("GM_SaveUniverse",_dg.Connection);
			com.CommandType = CommandType.StoredProcedure;

			MySqlParameter spUniverseId = new MySqlParameter("pUID", u.OBID);
			MySqlParameter spTick = new MySqlParameter("pTick", u.CurrentTick);
			MySqlParameter spTurn = new MySqlParameter("pTurn", u.CurrentTurn);
			MySqlParameter spPeriod = new MySqlParameter("pPeriod", u.CurrentPeriod);

			com.Parameters.AddRange (new MySqlParameter[] {spUniverseId, spTick, spTurn, spPeriod});

			try
			{
				_dg.ExecuteCommand(com);
				//Save Attributes
				for (int i = 0; i<u.Attributes.Count;i++)
				{
					int key = u.Attributes.Keys[i];
					com = new MySqlCommand("GM_SaveObjectAttribute", _dg.Connection);
					MySqlParameter spAttrId = new MySqlParameter("pAttrId", key);
					MySqlParameter spObjectType = new MySqlParameter("pObjectType", ObjectTypes.Universe);
					MySqlParameter spObjectId = new MySqlParameter("pObjectId", u.OBID);
					MySqlParameter spValue = new MySqlParameter("pValue", u.Attributes[key]);

					com.Parameters.AddRange (new MySqlParameter[] {spAttrId, spObjectType, spObjectId, spValue});
					_dg.ExecuteCommand(com);
				}

				//Save Effects
				for (int i = 0; i<u.Effects.Count;i++)
				{
					int key = u.Effects.Keys[i];
					com = new MySqlCommand("GM_SaveObjectEffect", _dg.Connection);
					MySqlParameter spAttrId = new MySqlParameter("pAttrId", key);
					MySqlParameter spObjectType = new MySqlParameter("pObjectType", ObjectTypes.Universe);
					MySqlParameter spObjectId = new MySqlParameter("pObjectId", u.OBID);
					MySqlParameter spValue = new MySqlParameter("pValue", u.Effects[key]);

					com.Parameters.AddRange (new MySqlParameter[] {spAttrId, spObjectType, spObjectId, spValue});
					_dg.ExecuteCommand(com);
				}
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
				res.Tag = u;
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
