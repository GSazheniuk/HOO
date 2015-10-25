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
		public DBCommandResult LoadGalaxy(Galaxy g)
		{
			DBCommandResult res = new DBCommandResult ();

			MySqlCommand com = new MySqlCommand ("ADM_GetGalaxyById", _dg.Connection);
			com.CommandType = CommandType.StoredProcedure;
			MySqlParameter spGalID = new MySqlParameter("pGalId", g.Id);
			com.Parameters.Add(spGalID);

			try
			{
				DataSet ds = _dg.GetDataSet(com);
				DataRow dr = ds.Tables[0].Rows[0];
				g.Id = Convert.ToInt32(dr["GalaxyId"]);
				g.Name = Convert.ToString(dr["Name"]);
				g.DimensionX = Convert.ToInt32(dr["DimX"]);
				g.DimensionY = Convert.ToInt32(dr["DimY"]);
				g.DimensionZ = Convert.ToInt32(dr["DimZ"]);

				foreach (DataRow aRow in ds.Tables[1].Rows)
				{
					g.Attributes.Add(Convert.ToInt32(aRow["Attribute"]), aRow["Value"]); 
				}

				foreach (DataRow eRow in ds.Tables[2].Rows)
				{
					g.Effects.Add(Convert.ToInt32(eRow["AttrId"]), eRow["Value"]);
				}

				//Requisites load here
				//TO-DO

				foreach (DataRow sRow in ds.Tables[4].Rows)
				{
					Star s = new Star();
					s.Galaxy = g;
					s.Id = Convert.ToInt32(sRow["StarId"]);
					s.Coordinates= new HOO.Core.Model.Configuration.Point3D();
					s.Class = ((StarClass)Convert.ToInt32(sRow["Class"]));
					s.TemperatureLevel = Convert.ToInt32(sRow["TempLvl"]);
					s.Size = ((StarSize)Convert.ToInt32(sRow["Size"]));
					s.StarSystemName = Convert.ToString(sRow["SystemName"]);
					s.Coordinates.X = Convert.ToInt32(sRow["X"]);
					s.Coordinates.Y = Convert.ToInt32(sRow["Y"]);
					s.Coordinates.Z = Convert.ToInt32(sRow["Z"]);

					g.Stars.Add(s);
				}

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
	}
}