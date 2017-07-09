using MongoDB.Bson;
using MongoDB.Driver;
using HOO.Core.Model.Universe;
using System.Collections.Generic;
using System;
using HOO.Core.Model;

namespace HOO.DB
{
    public partial class MongoDBHelper
    {
        public DBCommandResult AddNewPlayer(Player p)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var players = db.GetCollection<Player>("Players");

                p._id = players.Count(new BsonDocument());

                players.InsertOne(p, new InsertOneOptions { BypassDocumentValidation = false });

                p.IsLoaded = p.IsSaved = true;

                res.Tag = p;
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

        public DBCommandResult AuthPlayer(string userName, string password)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var players = db.GetCollection<Player>("Players").Find(x => x.Username == userName && x.Password == password).ToList();
                Player p = (players.Count > 0) ? players[0] : new Player();

                p.IsLoaded = p.IsSaved = (players.Count > 0);

                res.Tag = p;
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

        public DBCommandResult AllPlayers()
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var players = db.GetCollection<Player>("Players").Find(new BsonDocument()).ToList();

                res.Tag = players;
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

        public DBCommandResult SavePlayer(Player p)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var players = db.GetCollection<Player>("Players");

                res.Tag = players.ReplaceOne(new BsonDocument("_id", p._id), p, new UpdateOptions { IsUpsert = false });
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
    }
}
