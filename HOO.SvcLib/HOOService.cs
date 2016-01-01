using System;
using System.Linq;
using System.Collections.Generic;
using HOO.ComLib;
using HOO.Core.Model.Universe;
//using HOO.SvcLib.Helpers;
using HOO.Log;
using HOO.SvcLib.Helpers;
using HOO.Core.Model;

namespace HOO.SvcLib
{
    public class HOOService : IHOOService
    {
        private List<Universe> World;
        //private UniverseHelper uHelper;
        //private GalaxyHelper gHelper;
        private StarHelper sHelper;
        private PlayerHelper pHelper;
        private Logger log;

        public HOOService(object dep)
        {
            log = new Logger("HOO.SvcLib", this.GetType());
            log.Entry.MethodName = "Constructor";
            log.Entry.StepName = "Start";
            log.Debug(dep.ToString());
            if (dep is List<Universe>)
                this.World = (List<Universe>)dep;
        }

        public HOOService()
        {
            //log = new Logger("HOO.SvcLib", this.GetType());
            //log.Entry.MethodName = "Constructor";
            //uHelper = new UniverseHelper();
            //this.World = uHelper.AllUniverses();
            //log.Entry.StepName = "World loaded";
            //log.Debug(String.Format("Universes: {0}, Galaxies:{1}", this.World.Count, this.World[0].Galaxies.Count));
        }

        public List<Universe> GetAllUniverses()
        {
            log.Entry.MethodName = "GetAllUniverses";
            log.Entry.StepName = "Start";
            try
            {
                log.Debug(String.Format("Universes: {0}, Galaxies:{1}", this.World.Count, this.World[0].Galaxies.Count));
                return this.World;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return new List<Universe>();
        }

        public Universe GetUniverseById(long uId)
        {
            if (this.World.Exists(u => u._id == uId))
                return this.World.First(u => u._id == uId);

            return new Universe();
        }

        public Star GenerateNewStar(long universeId, long galaxyId)
        {
            sHelper = new StarHelper();
            if (World.Exists(x=>x._id == universeId))
            {
                Universe u = World.First(x => x._id == universeId);
                if (u.Galaxies.Exists(x=>x._id == galaxyId))
                {
                    Galaxy g = u.Galaxies.First(x => x._id == galaxyId);
                    return sHelper.GenerateNewStar(g);
                }
            }
            return sHelper.GenerateNewStar(new Galaxy());
        }

        public OnlinePlayer RegisterNewPlayer(Player p)
        {
            pHelper = new PlayerHelper();

            pHelper.Player = p;
            pHelper.Register();

            return pHelper.Player;
        }

        public OnlinePlayer AuthPlayer(string sessionId, string userName, string password)
        {
            pHelper = new PlayerHelper();
            pHelper.AuthUser(userName, password);

            if (pHelper.Player.IsLoaded)
            {
                pHelper.Player.TokenID = sessionId;
            }

            return pHelper.Player;
        }
    }
}
