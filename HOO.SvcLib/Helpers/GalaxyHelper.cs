using System;
using HOO.Core.Model.Universe;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class GalaxyHelper
	{
		public Galaxy Galaxy { get; set; }
		private MySqlDBHelper _dh ;
        private MongoDBHelper _mdh;
        private Log.Logger log;

        public GalaxyHelper ()
		{
            //			this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.Galaxy = new Galaxy ();
            this.log = new Log.Logger("HOO.SvcLib", typeof(GalaxyHelper));
        }

        private void InitDefaultParameters(){

		}

        public void Load()
        {
            log.Entry.MethodName = "Load";
            DBCommandResult res = _mdh.LoadGalaxy(Galaxy);
            if (res.ResultCode == 0 && res.Tag is Galaxy)
                Galaxy = (Galaxy)res.Tag;
            else
                log.Error(new Exception(res.ResultMsg));

            StarHelper sh = new StarHelper();
            for (int i = 0; i < Galaxy.Stars.Count && i < 100; i++)
            {
                sh.Star = Galaxy.Stars[i];
                sh.LoadStar(sh.Star);
                Galaxy.Stars[i] = sh.Star;
            }

            this.Galaxy.IsLoaded = this.Galaxy.IsSaved = true;
            if (this.Galaxy.Attributes.Count == 0)
                InitDefaultParameters();
            //ELSE add missing attributes, if any exists.
        }

        public void Save()
        {
            log.Entry.MethodName = "NewGalaxy";

            DBCommandResult res = _mdh.SaveGalaxy(this.Galaxy);

            if (res.ResultCode == 0 && res.Tag is Galaxy)
                Galaxy = (Galaxy)res.Tag;
            else
                log.Error(new Exception(res.ResultMsg));

            this.Galaxy.IsLoaded = this.Galaxy.IsSaved = true;
            if (this.Galaxy.Attributes.Count == 0)
                InitDefaultParameters();
            //ELSE add missing attributes, if any exists.
        }

    }
}

