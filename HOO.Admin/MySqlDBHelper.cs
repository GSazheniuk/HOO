using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOO.Core.Model.Configuration.Enums;
using HOO.DB;

namespace HOO.Admin
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
		public DBCommandResult AddUniverse(Universe u)
		{
			DBCommandResult res = new DBCommandResult();

			MySqlCommand com = new MySqlCommand("ADM_AddUniverse", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spName = new MySqlParameter("pName", u.Name);
			MySqlParameter spDescrip = new MySqlParameter("pDescrip", u.Descrip);
			com.Parameters.Add(spName);
			com.Parameters.Add(spDescrip);
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

		public DBCommandResult AddGalaxy(Galaxy gal)
		{
			DBCommandResult res = new DBCommandResult();

			MySqlCommand com = new MySqlCommand("ADM_AddGalaxy", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;

			MySqlParameter spUniverseId = new MySqlParameter("pUniverseId", gal.Universe.Id);
			MySqlParameter spName = new MySqlParameter("pName", gal.Name);
			MySqlParameter spDimX = new MySqlParameter("pDimX", gal.DimensionX);
			MySqlParameter spDimY = new MySqlParameter("pDimY", gal.DimensionY);
			MySqlParameter spDimZ = new MySqlParameter("pDimZ", gal.DimensionZ);

			com.Parameters.AddRange(new MySqlParameter[] { spUniverseId, spName, spDimX, spDimY, spDimZ});

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				Galaxy rg = new Galaxy();
				rg.Id = Convert.ToInt32(dr["GalaxyId"]);
				rg.Universe = gal.Universe;
				rg.Name = Convert.ToString(dr["Name"]);
				rg.DimensionX = Convert.ToInt32(dr["DimX"]);
				rg.DimensionY = Convert.ToInt32(dr["DimY"]);
				rg.DimensionZ = Convert.ToInt32(dr["DimZ"]);
				res.Tag = rg;
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

		public DBCommandResult AddStar(Star s)
		{
			DBCommandResult res = new DBCommandResult();

			MySqlCommand com = new MySqlCommand("ADM_AddStar", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;

			MySqlParameter spGalaxyId = new MySqlParameter("pGalaxyId", s.Galaxy.Id);
			MySqlParameter spSystemName = new MySqlParameter("pSystemName", s.StarSystemName);
			MySqlParameter spX = new MySqlParameter("pX", s.Coordinates.X);
			MySqlParameter spY = new MySqlParameter("pY", s.Coordinates.Y);
			MySqlParameter spZ = new MySqlParameter("pZ", s.Coordinates.Z);
			MySqlParameter spClass = new MySqlParameter("pClass", s.Class);
			MySqlParameter spSize = new MySqlParameter("pSize", s.Size);
			MySqlParameter spTempLvl = new MySqlParameter("pTempLvl", s.TemperatureLevel);

			com.Parameters.AddRange(new MySqlParameter[] { spClass, spGalaxyId, spSize, spSystemName, spTempLvl, spX, spY, spZ });

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				Star rs = new Star();
				rs.Id = Convert.ToInt32(dr["StarId"]);
				rs.Galaxy = s.Galaxy;
				rs.StarSystemName = Convert.ToString(dr["SystemName"]);
				rs.Coordinates = new Core.Model.Configuration.Point3D();
				rs.Coordinates.X = Convert.ToInt32(dr["X"]);
				rs.Coordinates.Y = Convert.ToInt32(dr["Y"]);
				rs.Coordinates.Z = Convert.ToInt32(dr["Z"]);
				rs.Class = (StarClass)Convert.ToInt32(dr["Class"]);
				rs.Size = (StarSize)Convert.ToInt32(dr["Size"]);
				rs.TemperatureLevel = Convert.ToInt32(dr["TempLvl"]);
				res.Tag = rs;
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

		public DBCommandResult AddOrbitalBody(StarOrbitalBody sob)
		{
			DBCommandResult res = new DBCommandResult();

			if (sob is Planet)
			{
				Planet p = (Planet)sob;
				MySqlCommand com = new MySqlCommand("ADM_AddStarOrbitalBody", _dg.Connection);
				com.CommandType = CommandType.StoredProcedure;

				MySqlParameter spStarId = new MySqlParameter("pStarId", p.Star.Id);
				MySqlParameter spOrbitNo = new MySqlParameter("pOrbitNo", p.OrbitNo);
				MySqlParameter spBodyType = new MySqlParameter("pBodyType", 1);
				MySqlParameter spSize = new MySqlParameter("pSize", p.Size);
				MySqlParameter spType = new MySqlParameter("pClass", p.Type);

				com.Parameters.AddRange(new MySqlParameter[] { spStarId, spSize, spOrbitNo, spType, spBodyType });

				try
				{
					DataSet ds = _dg.GetDataSet(com);
					DataRow dr = ds.Tables[0].Rows[0];
					Planet rp = new Planet(sob.Star);
					rp.Id = Convert.ToInt32(dr["OBID"]);
					rp.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
					rp.Size = (PlanetSize)Convert.ToInt32(dr["Size"]);
					rp.Type = (PlanetType)Convert.ToInt32(dr["Class"]);
					res.Tag = rp;
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

			if (sob is GasGiant)
			{
				GasGiant p = (GasGiant)sob;
				MySqlCommand com = new MySqlCommand("ADM_AddStarOrbitalBody", _dg.Connection);
				com.CommandType = CommandType.StoredProcedure;

				MySqlParameter spStarId = new MySqlParameter("pStarId", p.Star.Id);
				MySqlParameter spOrbitNo = new MySqlParameter("pOrbitNo", p.OrbitNo);
				MySqlParameter spBodyType = new MySqlParameter("pBodyType", 2);
				MySqlParameter spSize = new MySqlParameter("pSize", p.Size);
				MySqlParameter spType = new MySqlParameter("pClass", p.Class);

				com.Parameters.AddRange(new MySqlParameter[] { spStarId, spSize, spOrbitNo, spType, spBodyType });

				try
				{
					DataSet ds = _dg.GetDataSet(com);
					DataRow dr = ds.Tables[0].Rows[0];
					GasGiant rg = new GasGiant(sob.Star);
					rg.Id = Convert.ToInt32(dr["OBID"]);
					rg.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
					rg.Class = (GasGiantClass)Convert.ToInt32(dr["Class"]);
					rg.Size = (GasGiantSize)Convert.ToInt32(dr["Size"]);
					res.Tag = rg;
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

			if (sob is AsteroidBelt)
			{
				AsteroidBelt p = (AsteroidBelt)sob;
				MySqlCommand com = new MySqlCommand("ADM_AddStarOrbitalBody", _dg.Connection);
				com.CommandType = CommandType.StoredProcedure;

				MySqlParameter spStarId = new MySqlParameter("pStarId", p.Star.Id);
				MySqlParameter spOrbitNo = new MySqlParameter("pOrbitNo", p.OrbitNo);
				MySqlParameter spBodyType = new MySqlParameter("pBodyType", 3);
				MySqlParameter spSize = new MySqlParameter("pSize", p.Density);
				MySqlParameter spType = new MySqlParameter("pClass", p.Type);

				com.Parameters.AddRange(new MySqlParameter[] { spStarId, spSize, spOrbitNo, spType, spBodyType });

				try
				{
					DataSet ds = _dg.GetDataSet(com);
					DataRow dr = ds.Tables[0].Rows[0];
					AsteroidBelt ra = new AsteroidBelt(sob.Star);
					ra.Id = Convert.ToInt32(dr["OBID"]);
					ra.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
					ra.Density = (AsteroidDensity)Convert.ToInt32(dr["Size"]);
					ra.Type = (AsteroidType)Convert.ToInt32(dr["Class"]);

					res.Tag = ra;
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
			return res;
		}
		#endregion
	}
}
