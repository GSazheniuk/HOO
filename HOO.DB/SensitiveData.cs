using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOO.DB
{
    public class SensitiveData
    {
        private static string mongoDbUri = "mongodb://test:test@ds059524.mongolab.com:59524/hootestdb";
        public static string ConnectionString = mongoDbUri;
    }
}
