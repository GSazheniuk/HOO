using MongoDB.Bson;
using MongoDB.Driver;
using HOO.Core.Model.Universe;
using System.Collections.Generic;
using System;

namespace HOO.DB
{
    public partial class MongoDBHelper
    {
        public DBCommandResult GetAllGalaxies(long uId)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                res.Tag = db.GetCollection<Galaxy>("Galaxies").Find(new BsonDocument("UniverseId", uId)).ToList();
                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.Tag = new List<Galaxy>();
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }
            return res;
        }

        public DBCommandResult SaveGalaxy(Galaxy g)
        {
            DBCommandResult res = new DBCommandResult();
            try
            {
                var us = db.GetCollection<Galaxy>("Galaxies");
                if (g.IsLoaded)
                    res.Tag = us.ReplaceOne(new BsonDocument("_id", g._id), g, new UpdateOptions { IsUpsert = true });
                else
                {
                    us.InsertOne(g, new InsertOneOptions { BypassDocumentValidation = false });
                    res.Tag = g;
                }

                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }
            return res;
        }

        public DBCommandResult LoadGalaxy(Galaxy g)
        {
            if (g != null)
                return LoadGalaxy(g._id);
            else
                return new DBCommandResult() { ResultCode = -1, ResultMsg = "Null Galaxy specified as parameter." };
        }

        public DBCommandResult LoadGalaxy(long galaxyId)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var rg = db.GetCollection<Galaxy>("Galaxies").Find(new BsonDocument("_id", galaxyId)).ToList();
                Galaxy g = (rg.Count > 0) ? rg[0] : new Galaxy();

                DBCommandResult rs = GetAllStars(galaxyId);

                if (rs.ResultCode == 0)
                    g.Stars = (List<Star>)rs.Tag;

                g.IsLoaded = g.IsSaved = true;
                res.Tag = g;
                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.Tag = new Galaxy();
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return res;
        }
    }
}
