using MongoDB.Bson;
using MongoDB.Driver;
using HOO.Core.Model.Universe;
using System.Collections.Generic;
using System;

namespace HOO.DB
{
    public partial class MongoDBHelper
    {
        public DBCommandResult SaveOrbitalBody(StarOrbitalBody sob)
        {
            DBCommandResult res = new DBCommandResult();
            try
            {
                var stars = db.GetCollection<StarOrbitalBody>("StarOrbitalBodies");
                if (sob.IsLoaded)
                    res.Tag = stars.ReplaceOne(new BsonDocument("_id", sob._id), sob, new UpdateOptions { IsUpsert = true });
                else
                {
                    stars.InsertOne(sob, new InsertOneOptions { BypassDocumentValidation = false });
                    res.Tag = sob;
                }

                sob.IsLoaded = sob.IsSaved = true;

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

        public DBCommandResult GetStarOrbitalBodies(Star s)
        {
            if (s != null)
                return GetStarOrbitalBodies(s._id);
            else
                return new DBCommandResult() { ResultCode = -1, ResultMsg = "Null Star specified as argument." };
        }

        public DBCommandResult GetStarOrbitalBodies(long starId)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var rs = db.GetCollection<Star>("Stars").Find(new BsonDocument("_id", starId)).ToList();
                Star s = (rs.Count > 0) ? rs[0] : new Star();

                DBCommandResult ro = GetAllOrbitalBodies(starId);

                if (ro.ResultCode == 0)
                    s.OrbitalBodies = (List<StarOrbitalBody>)ro.Tag;

                s.IsLoaded = s.IsSaved = true;
                res.Tag = s;
                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.Tag = new Star();
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return res;
        }

        public DBCommandResult GetAllOrbitalBodies(long starId)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                res.Tag = db.GetCollection<StarOrbitalBody>("StarOrbitalBodies").Find(new BsonDocument("StarId", starId)).ToList();
                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.Tag = new List<StarOrbitalBody>();
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return res;
        }
    }
}
