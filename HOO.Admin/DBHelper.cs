using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Admin
{
    public class DBCommandResult
    {
        public int ResultCode { get; set; }
        public string ResultMsg { get; set; }
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
        public DBCommandResult AddGalaxy(Galaxy gal)
        {
            return new DBCommandResult();
        }

        public DBCommandResult AddStar(Star s)
        {
            return new DBCommandResult();
        }

        public DBCommandResult AddOrbitalBody(StarOrbitalBody sob)
        {
            return new DBCommandResult();
        }
        #endregion
    }
}
