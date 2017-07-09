using System;
using System.Linq;
using HOO.Core.Model.Universe;
using HOO.DB;
using System.Collections.Generic;

namespace HOO.SvcLib.Helpers
{
	public class UniverseHelper
	{
		public Universe Universe { get; set; }
		private MySqlDBHelper _dh ;
        private MongoDBHelper _mdh;
        private Log.Logger log;

		public UniverseHelper ()
		{
            //this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
			this.Universe = new Universe ();
            this.log = new Log.Logger("HOO.SvcLib", typeof(UniverseHelper));
		}

		private void InitDefaultParameters(){

		}

        public List<Universe> AllUniverses()
        {
            DBCommandResult r = _mdh.AllUniverses();
            log.Entry.MethodName = "AllUniverses";

            if (r != null && r.ResultCode == 0 && r.Tag is List<Universe>)
            {
                List<Universe> x = (List<Universe>)r.Tag;
                for (int i = 0; i < x.Count; i++)
                {
                    x[i] = LoadUniverse(x[i]);
                    log.Debug(String.Format("Universe({0}:{1}) loaded", x[i].Description, x[i]._id));
                }
                return x;
            }
            else
            {
                log.Error(new Exception(r.ResultMsg));
            }

            return new List<Universe>();
        }

        public Universe LoadUniverse(Universe u)
        {
            log.Entry.MethodName = "LoadUniverse";
            DBCommandResult r = _mdh.LoadUniverse(u);

            if (r.ResultCode == 0 && r.Tag is Universe)
                u = (Universe)r.Tag;
            else
                log.Error(new Exception(r.ResultMsg));

            GalaxyHelper gHelper = new GalaxyHelper();
            for (int i = 0; i < u.Galaxies.Count; i++)
            {
                gHelper.Galaxy = u.Galaxies[i];
                gHelper.Load();
                u.Galaxies[i] = gHelper.Galaxy;
            }
            return u;
        }

        public void Save()
		{
			if (this.Universe.IsLoaded && !this.Universe.IsSaved) {
				DBCommandResult res = _mdh.SaveUniverse (Universe);
				if (res.ResultCode == 0)
					this.Universe.IsSaved = true;
			}

			//if (this.Universe.Galaxies.Exists (g => g.Stars.Exists (s => s.OrbitalBodies.Exists (ob => !ob.IsSaved)))) {
			//	Galaxy gal = this.Universe.Galaxies.First (g => g.Stars.Exists (s => s.OrbitalBodies.Exists (ob => !ob.IsSaved)));
			//	Star st = gal.Stars.First (s => s.OrbitalBodies.Exists (ob => !ob.IsSaved));
			//	StarOrbitalBody sob = st.OrbitalBodies.First (ob => !ob.IsSaved);
			//	StarOrbitalBodyHelper sobh = new StarOrbitalBodyHelper (sob);
			//	sobh.Save ();
			//}
		}

        public void Load()
        {
            DBCommandResult res = _mdh.LoadUniverse(Universe);
            if (res.ResultCode == 0)
            {
                this.Universe = (Universe)res.Tag;
                //GalaxyHelper gh = new GalaxyHelper();
                //foreach (Galaxy g in this.Universe.Galaxies)
                //{
                //    gh.Galaxy = g;
                //    gh.Load();
                //}
                //this.Universe.IsLoaded = this.Universe.IsSaved = true;

                if (this.Universe.Attributes.Count == 0)
                    InitDefaultParameters();
                //ELSE add missing attributes, if any exists.
            }
            else
            {
                throw new Exception(res.ResultMsg);
            }
        }

		public void Tick()
		{
			if (this.Universe.IsLoaded) {
				this.Universe.CurrentTick = (this.Universe.CurrentTick + 1) % 10;
				if (this.Universe.CurrentTick == 0) {
					this.Universe.CurrentTurn = (this.Universe.CurrentTurn + 1) % 100;
					if (this.Universe.CurrentTurn == 0) {
						this.Universe.CurrentPeriod++;
					}
//					Save ();
				}
                this.Universe.IsSaved = false;
            }
        }
	}
}
