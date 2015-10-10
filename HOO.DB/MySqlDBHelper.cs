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

		public DBCommandResult GetGalaxy(int gId)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetGalaxyById", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spGalID = new MySqlParameter("pGalId", gId);
			com.Parameters.Add(spGalID);

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				Galaxy g = new Galaxy();
				DataRow dr = ds.Tables[0].Rows[0];
				g.Id = Convert.ToInt32(dr["GalaxyId"]);
				g.Name = Convert.ToString(dr["Name"]);
				g.DimensionX = Convert.ToInt32(dr["DimX"]);
				g.DimensionY = Convert.ToInt32(dr["DimY"]);
				g.DimensionZ = Convert.ToInt32(dr["DimZ"]);

				res.Tag = g;
				res.ResultCode = 0;
				res.ResultMsg = "Ok";
			}
			catch (Exception ex) {
				res.ResultCode = -2;
				res.ResultMsg = String.Format ("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
			}
			return res;
		}

		public DBCommandResult GetAllStars(int gId)
		{
			DBCommandResult res = this.GetGalaxy (gId);

			if (res.ResultCode < 0)
			{
				return res;
			}

			Galaxy g = (Galaxy)res.Tag;

			res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetAllStars", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spGalId = new MySqlParameter("pGalId", gId);
			com.Parameters.Add(spGalId);

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				List<Star> stars = new List<Star>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Star s = new Star();
					s.Galaxy = g;
					s.Id = Convert.ToInt32(dr["StarId"]);
					s.Galaxy = new Galaxy();
					s.Coordinates= new HOO.Core.Model.Configuration.Point3D();
					s.Class = ((StarClass)Convert.ToInt32(dr["Class"]));
					s.TemperatureLevel = Convert.ToInt32(dr["TempLvl"]);
					s.Size = ((StarSize)Convert.ToInt32(dr["Size"]));
					s.StarSystemName = Convert.ToString(dr["SystemName"]);
					s.Coordinates.X = Convert.ToInt32(dr["X"]);
					s.Coordinates.Y = Convert.ToInt32(dr["Y"]);
					s.Coordinates.Z = Convert.ToInt32(dr["Z"]);

					stars.Add(s);
				}
				res.Tag = stars;
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
			MySqlParameter spStarId = new MySqlParameter("pStarId", s.Id);
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
						s.OrbitalBodies.Add(p);
						break;
					case 2:
						GasGiant g = new GasGiant(s);
						g.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
						g.Size = (GasGiantSize)Convert.ToInt32(dr["Size"]);
						g.Class = (GasGiantClass)Convert.ToInt32(dr["Class"]);
						s.OrbitalBodies.Add(g);
						break;
					case 3:
						AsteroidBelt a = new AsteroidBelt(s);
						a.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
						a.Density = (AsteroidDensity)Convert.ToInt32(dr["Size"]);
						a.Type = (AsteroidType)Convert.ToInt32(dr["Class"]);
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
		#endregion
	}
}
