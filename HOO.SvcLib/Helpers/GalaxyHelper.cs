using System;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class GalaxyHelper
	{
		public Galaxy Galaxy { get; set; }
		private MySqlDBHelper _dh ;

		public GalaxyHelper ()
		{
			this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			this.Galaxy = new Galaxy ();
		}

		public void Load()
		{
			DBCommandResult res = _dh.LoadGalaxy (Galaxy);
			if (res.ResultCode == 0) {
				StarHelper sh = new StarHelper ();
				foreach (Star s in Galaxy.Stars) {
					sh.Star = s;
					sh.RefreshOrbitalBodies ();
				}
				this.Galaxy.IsLoaded = this.Galaxy.IsSaved = true;
			} else {
				throw new Exception (res.ResultMsg);
			}
		}
	}
}

