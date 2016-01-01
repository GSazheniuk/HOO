using MongoDB.Bson;
using MongoDB.Driver;
using HOO.Core.Model.Universe;
using System.Collections.Generic;
using System;

namespace HOO.DB
{
    public partial class MongoDBHelper
    {
        MongoClient client;
        IMongoDatabase db;

        public MongoDBHelper()
        {
            client = new MongoClient(SensitiveData.ConnectionString);
            db = client.GetDatabase("hootestdb");

        }

        public DBCommandResult AllUniverses()
        {
            DBCommandResult res = new DBCommandResult();
            try
            {
                res.Tag = db.GetCollection<Universe>("Universe").Find(new BsonDocument()).ToList();
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

        public DBCommandResult SaveUniverse(Universe u)
        {
            DBCommandResult res = new DBCommandResult();
            try
            {
                var us = db.GetCollection<Universe>("Universe");
                res.Tag = us.ReplaceOne(new BsonDocument("_id", u._id), u, new UpdateOptions { IsUpsert = true });
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

        public DBCommandResult LoadUniverse(Universe u)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var resu = db.GetCollection<Universe>("Universe").Find(new BsonDocument("_id", u._id)).ToList();
                Universe ru = (resu.Count > 0) ? resu[0] : new Universe();

                DBCommandResult rg = GetAllGalaxies(ru._id);

                if (rg.ResultCode == 0)
                    ru.Galaxies = (List<Galaxy>)rg.Tag;

                ru.IsLoaded = ru.IsSaved = true;
                res.Tag = ru;
                res.ResultCode = 0;
                res.ResultMsg = "Ok";
            }
            catch (Exception ex)
            {
                res.Tag = new Universe();
                res.ResultCode = -2;
                res.ResultMsg = String.Format("{0} ----> {1}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }
            return res;
        }
    }
}
