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
        public DBCommandResult AddNewProduct(Product p)
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var products = db.GetCollection<Product>("Products");

                p._id = products.Count(new BsonDocument());

                products.InsertOne(p, new InsertOneOptions { BypassDocumentValidation = false });

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

        public DBCommandResult GetAllProducts()
        {
            DBCommandResult res = new DBCommandResult();

            try
            {
                var products = db.GetCollection<Product>("Products").Find(new BsonDocument()).ToList();
                res.Tag = products;
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
