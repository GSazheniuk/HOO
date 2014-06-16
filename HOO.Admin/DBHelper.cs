using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOO.Core.Model.Configuration.Enums;

namespace HOO.Admin
{
    public class DBCommandResult
    {
        public int ResultCode { get; set; }
        public string ResultMsg { get; set; }
        public object Tag { get; set; }

        public DBCommandResult()
        {
            ResultCode = -1;
            ResultMsg = "Not implemented";
        }
    }

    public class DBHelper
    {
        #region Private Fields
        private DataGate _dg;
        #endregion

        #region Constructors
        public DBHelper()
        {

        }

        public DBHelper(string connStr)
        {
            _dg = new DataGate(connStr);
        }
        #endregion

        #region Public Methods
        public DBCommandResult AddUniverse(Universe u)
        {
            DBCommandResult res = new DBCommandResult();
            
            SqlCommand com = new SqlCommand("ADM.AddUniverse", _dg.Connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlParameter spName = new SqlParameter("@Name", u.Name);
            SqlParameter spDescrip = new SqlParameter("@Descrip", u.Descrip);
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
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, ex.InnerException.Message);
            }
            return res;
        }

        public DBCommandResult AddGalaxy(Galaxy gal)
        {
            DBCommandResult res = new DBCommandResult();
            
            SqlCommand com = new SqlCommand("ADM.AddGalaxy", _dg.Connection);
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter spUniverseId = new SqlParameter("@UniverseId", gal.Universe.Id);
            SqlParameter spName = new SqlParameter("@Name", gal.Name);
            SqlParameter spDimX = new SqlParameter("@DimX", gal.DimensionX);
            SqlParameter spDimY = new SqlParameter("@DimY", gal.DimensionY);
            SqlParameter spDimZ = new SqlParameter("@DimZ", gal.DimensionZ);

            com.Parameters.AddRange(new SqlParameter[] { spUniverseId, spName, spDimX, spDimY, spDimZ});

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
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, ex.InnerException.Message);
            }
            
            return res;
        }

        public DBCommandResult AddStar(Star s)
        {
            DBCommandResult res = new DBCommandResult();

            SqlCommand com = new SqlCommand("ADM.AddStar", _dg.Connection);
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter spGalaxyId = new SqlParameter("@GalaxyId", s.Galaxy.Id);
            SqlParameter spSystemName = new SqlParameter("@SystemName", s.StarSystemName);
            SqlParameter spX = new SqlParameter("@X", s.Coordinates.X);
            SqlParameter spY = new SqlParameter("@Y", s.Coordinates.Y);
            SqlParameter spZ = new SqlParameter("@Z", s.Coordinates.Z);
            SqlParameter spClass = new SqlParameter("@Class", s.Class);
            SqlParameter spSize = new SqlParameter("@Size", s.Size);
            SqlParameter spTempLvl = new SqlParameter("@TempLvl", s.TemperatureLevel);

            com.Parameters.AddRange(new SqlParameter[] { spClass, spGalaxyId, spSize, spSystemName, spTempLvl, spX, spY, spZ });

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
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, ex.InnerException.Message);
            }

            return res;
        }

        public DBCommandResult AddOrbitalBody(StarOrbitalBody sob)
        {
            DBCommandResult res = new DBCommandResult();

            if (sob is Planet)
            {
                Planet p = (Planet)sob;
                SqlCommand com = new SqlCommand("ADM.AddPlanet", _dg.Connection);
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter spStarId = new SqlParameter("@StarId", p.Star.Id);
                SqlParameter spOrbitNo = new SqlParameter("@OrbitNo", p.OrbitNo);
                SqlParameter spSize = new SqlParameter("@Size", p.Size);
                SqlParameter spType = new SqlParameter("@Type", p.Type);

                com.Parameters.AddRange(new SqlParameter[] { spStarId, spSize, spOrbitNo, spType });

                try
                {
                    DataSet ds = _dg.GetDataSet(com);
                    DataRow dr = ds.Tables[0].Rows[0];
                    Planet rp = new Planet(sob.Star);
                    rp.Id = Convert.ToInt32(dr["Id"]);
                    rp.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
                    rp.Size = (PlanetSize)Convert.ToInt32(dr["Size"]);
                    rp.Type = (PlanetType)Convert.ToInt32(dr["Type"]);
                    res.Tag = rp;
                    res.ResultCode = 0;
                    res.ResultMsg = "Ok";
                }
                catch (Exception ex)
                {
                    res.ResultCode = -2;
                    res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, ex.InnerException.Message);
                }
                return res;
            }

            if (sob is GasGiant)
            {
                GasGiant p = (GasGiant)sob;
                SqlCommand com = new SqlCommand("ADM.AddGasGiant", _dg.Connection);
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter spStarId = new SqlParameter("@StarId", p.Star.Id);
                SqlParameter spOrbitNo = new SqlParameter("@OrbitNo", p.OrbitNo);

                com.Parameters.AddRange(new SqlParameter[] { spStarId, spOrbitNo });

                try
                {
                    DataSet ds = _dg.GetDataSet(com);
                    DataRow dr = ds.Tables[0].Rows[0];
                    GasGiant rg = new GasGiant(sob.Star);
                    rg.Id = Convert.ToInt32(dr["Id"]);
                    rg.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
                    res.Tag = rg;
                    res.ResultCode = 0;
                    res.ResultMsg = "Ok";
                }
                catch (Exception ex)
                {
                    res.ResultCode = -2;
                    res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, ex.InnerException.Message);
                }
                return res;
            }

            if (sob is AsteroidBelt)
            {
                AsteroidBelt p = (AsteroidBelt)sob;
                SqlCommand com = new SqlCommand("ADM.AddAsteroidBelt", _dg.Connection);
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter spStarId = new SqlParameter("@StarId", p.Star.Id);
                SqlParameter spOrbitNo = new SqlParameter("@OrbitNo", p.OrbitNo);
                SqlParameter spDensity = new SqlParameter("@Density", p.Density);

                com.Parameters.AddRange(new SqlParameter[] { spStarId, spOrbitNo, spDensity });

                try
                {
                    DataSet ds = _dg.GetDataSet(com);
                    DataRow dr = ds.Tables[0].Rows[0];
                    AsteroidBelt ra = new AsteroidBelt(sob.Star);
                    ra.Id = Convert.ToInt32(dr["Id"]);
                    ra.OrbitNo = Convert.ToInt32(dr["OrbitNo"]);
                    ra.Density = (AsteroidDensity)Convert.ToInt32(dr["Density"]);
                    res.Tag = ra;
                    res.ResultCode = 0;
                    res.ResultMsg = "Ok";
                }
                catch (Exception ex)
                {
                    res.ResultCode = -2;
                    res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, ex.InnerException.Message);
                }
                return res;
            }
            return res;
        }
        #endregion
    }
}
