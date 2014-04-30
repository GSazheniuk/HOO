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
        #region Constructors
        public DBHelper()
        {

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
