using System;
using HOO.Core.Model.Universe;
using HOO.Core.Model;
using HOO.DB;
using System.Collections.Generic;

namespace HOO.SvcLib.Helpers
{
    public class ProductHelper
    {
        public Product Product;
        private MySqlDBHelper _dh;
        private MongoDBHelper _mdh;
        private Log.Logger log;

        public ProductHelper()
        {
            //			_dh = new MySqlDBHelper (SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.log = new Log.Logger("HOO.SvcLib", typeof(ProductHelper));
        }

        public List<Product> GetAllProducts()
        {
            DBCommandResult res = new DBCommandResult();
            res = _mdh.GetAllProducts();

            if (res.ResultCode == 0)
            {
                return (List<Product>)res.Tag;
            }
            else
            {
                throw new Exception(res.ResultMsg);
            }

        }
    }
}
