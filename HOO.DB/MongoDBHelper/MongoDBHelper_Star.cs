using MongoDB.Bson;
using MongoDB.Driver;
using HOO.Core.Model.Universe;
using System.Collections.Generic;
using System;

namespace HOO.DB
{
    public partial class MongoDBHelper
    {
        public DBCommandResult SaveStar(Star s)
        {
            DBCommandResult res = new DBCommandResult();
            try
            {
                var stars = db.GetCollection<Star>("Stars");
                if (s.IsLoaded)
                    res.Tag = stars.ReplaceOne(new BsonDocument("_id", s._id), s, new UpdateOptions { IsUpsert = true });
                else
                {
                    stars.InsertOne(s, new InsertOneOptions { BypassDocumentValidation = false });
                    res.Tag = s;
                }

                s.IsLoaded = s.IsSaved = true;

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

        public DBCommandResult GetStarNames()
        {
            DBCommandResult res = new DBCommandResult();
            var snames = db.GetCollection<StarName>("StarDictionary").Find(new BsonDocument()).ToList();
            res.Tag = snames;
            res.ResultCode = 0;
            res.ResultMsg = "Ok";
            return res;
        }

        public DBCommandResult GetAllStars(long galaxyId)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                res.Tag = db.GetCollection<Star>("Stars").Find(new BsonDocument("GalaxyId", galaxyId)).ToList();
                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.Tag = new List<Star>();
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return res;
        }
    }
}
