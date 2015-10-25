using System;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class StarHelper
	{
		public Star Star { get; set; }
		private MySqlDBHelper _dh ;

		public StarHelper ()
		{
			this._dh = new MySqlDBHelper (SensitiveData.ConnectionString);
			this.Star = new Star ();
		}

		public StarHelper(Star s)
		{
			this._dh = new MySqlDBHelper (SensitiveData.ConnectionString);
			this.Star = s;
		}

		public StarHelper(int starId)
		{
			this._dh = new MySqlDBHelper (SensitiveData.ConnectionString);
			this.Star = new Star ();
			this.Star.Id = starId;
		}

		public void RefreshOrbitalBodies()
		{
			DBCommandResult res = _dh.GetStarOrbitalBodies (this.Star);
			if (res.ResultCode == 0) {
				this.Star = (Star)res.Tag;
			}
		}
	}
}

