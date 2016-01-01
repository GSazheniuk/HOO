using System;
using HOO.Core.Model.Universe;
using HOO.DB;
using HOO.Core.Model.Configuration;
using System.Collections.Generic;
using HOO.Core.Model.Configuration.Enums;

namespace HOO.SvcLib.Helpers
{
	public class StarHelper
	{
		public Star Star { get; set; }
		private MySqlDBHelper _dh ;
        private MongoDBHelper _mdh;
        private Log.Logger log;

        public StarHelper ()
		{
			//this._dh = new MySqlDBHelper (SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.Star = new Star ();
            this.log = new Log.Logger("HOO.SvcLib", typeof(StarHelper));
		}

		public StarHelper(Star s)
		{
//			this._dh = new MySqlDBHelper (SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.Star = s;
            this.log = new Log.Logger("HOO.SvcLib", typeof(StarHelper));
        }

        public StarHelper(int starId)
		{
//			this._dh = new MySqlDBHelper (SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.Star = new Star ();
			this.Star.OBID = starId;
            this.log = new Log.Logger("HOO.SvcLib", typeof(StarHelper));
        }

        private void InitDefaultParameters(){

		}

		public void RefreshOrbitalBodies()
		{
			DBCommandResult res = _dh.GetStarOrbitalBodies (this.Star);
			if (res.ResultCode == 0) {
				this.Star = (Star)res.Tag;
				this.Star.IsLoaded = this.Star.IsSaved = true;
				if (this.Star.Attributes.Count == 0)
					InitDefaultParameters ();
				//ELSE add missing attributes, if any exists.
			}
		}

        public void LoadStar(Star s)
        {
            LoadStar(s._id);
        }

        public void LoadStar(long starId)
        {
            DBCommandResult res = _mdh.GetStarOrbitalBodies(starId);
            if (res.ResultCode == 0)
            {
                this.Star = (Star)res.Tag;
                this.Star.IsLoaded = this.Star.IsSaved = true;
                if (this.Star.Attributes.Count == 0)
                    InitDefaultParameters();
                //ELSE add missing attributes, if any exists.
            }
        }

        public string GenerateStarName()
        {
            string pref = "";
            string suff = "";

            switch (MrRandom.rnd.Next(4))
            {
                case 0:
                    pref = "";
                    suff = "";
                    break;
                case 1:
                    pref = Config.StarSuffixesAndPrefixes.Prefixes[MrRandom.rnd.Next(Config.StarSuffixesAndPrefixes.Prefixes.Length)];
                    suff = "";
                    break;
                case 2:
                    pref = "";
                    suff = Config.StarSuffixesAndPrefixes.Suffixes[MrRandom.rnd.Next(Config.StarSuffixesAndPrefixes.Suffixes.Length)];
                    break;
                case 3:
                    pref = Config.StarSuffixesAndPrefixes.Prefixes[MrRandom.rnd.Next(Config.StarSuffixesAndPrefixes.Prefixes.Length)];
                    suff = Config.StarSuffixesAndPrefixes.Suffixes[MrRandom.rnd.Next(Config.StarSuffixesAndPrefixes.Suffixes.Length)];
                    break;
            }

            if (pref != "")
            {
                int x = MrRandom.rnd.Next(10);
                switch (x)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        pref = pref + "-" + (x + 1).ToString();
                        break;
                }
                pref += " ";
            }

            if (suff != "")
                suff = " " + suff;

            DBCommandResult res = _mdh.GetStarNames();
            if (res.ResultCode == 0)
            {
                List<StarName> starNames = (List<StarName>)res.Tag;
                string randomName = starNames[MrRandom.rnd.Next(starNames.Count)].Name;
                return pref + randomName + suff;
            }
            else
            {
                return res.ResultMsg;
            }
        }

        public Star GenerateNewStar(Galaxy g)
        {
            Star s = new Star();
            s._id = g.Stars.Count;

            while (!g.AddStar(s, ConstantParameters.MinDistanceBetweenStars))
            {

            }

            s.StarSystemName = this.GenerateStarName();
            StarOrbitalBodyHelper sobHelper = new StarOrbitalBodyHelper();

            //s.Coordinates = new Point3D() { X = MrRandom.rnd.Next(1000), Y = MrRandom.rnd.Next(800), Z = MrRandom.rnd.Next(600) };
            int orbits = MrRandom.rnd.Next(ConstantParameters.MaxOrbitalBodiesForStar);
            s.OrbitalIDs = new long[orbits];
            List<int> freeOrbits = new List<int>();
            freeOrbits.AddRange(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int k = 0; k < orbits; k++)
            {
                int bodyType = MrRandom.rnd.Next(3);

                switch (bodyType)
                {
                    case 0:
                        Planet p = new Planet(s._id);
                        p._id = 10 * s._id + s.OrbitalBodies.Count;
                        s.OrbitalIDs[k] = p._id;
                        p.Size = (PlanetSize)MrRandom.rnd.Next((int)PlanetSize.MrRandom);
                        p.Type = (PlanetType)MrRandom.rnd.Next((int)PlanetType.MrRandom);
                        p.OrbitNo = freeOrbits[MrRandom.rnd.Next(freeOrbits.Count)];
                        p.StarSystemName = s.StarSystemName;
                        freeOrbits.Remove(p.OrbitNo);
                        s.OrbitalBodies.Add(p);
                        sobHelper.OrbitalBody = p;
                        break;
                    case 1:
                        GasGiant gg = new GasGiant(s._id);
                        gg._id = 10 * s._id + s.OrbitalBodies.Count;
                        s.OrbitalIDs[k] = gg._id;
                        gg.Class = (GasGiantClass)MrRandom.rnd.Next((int)GasGiantClass.MrRandom);
                        gg.Size = (GasGiantSize)MrRandom.rnd.Next((int)GasGiantSize.MrRandom);
                        gg.OrbitNo = freeOrbits[MrRandom.rnd.Next(freeOrbits.Count)];
                        gg.StarSystemName = s.StarSystemName;
                        freeOrbits.Remove(gg.OrbitNo);
                        s.OrbitalBodies.Add(gg);
                        sobHelper.OrbitalBody = gg;
                        break;
                    case 2:
                        AsteroidBelt a = new AsteroidBelt(s._id);
                        a._id = 10 * s._id + s.OrbitalBodies.Count;
                        s.OrbitalIDs[k] = a._id;
                        a.Density = (AsteroidDensity)MrRandom.rnd.Next((int)AsteroidDensity.MrRandom);
                        a.Type = (AsteroidType)MrRandom.rnd.Next((int)AsteroidType.MrRandom);
                        a.OrbitNo = freeOrbits[MrRandom.rnd.Next(freeOrbits.Count)];
                        a.StarSystemName = s.StarSystemName;
                        freeOrbits.Remove(a.OrbitNo);
                        s.OrbitalBodies.Add(a);
                        sobHelper.OrbitalBody = a;
                        break;
                }
                sobHelper.Save();
            }

            SaveStar(s);
            this.Star = s;
            return s;
        }

        public void SaveStar(Star s)
        {
            DBCommandResult res = _mdh.SaveStar(s);

            if (res.ResultCode != 0)
            {
                log.Error(new Exception(res.ResultMsg));
            }
        }
    }
}

