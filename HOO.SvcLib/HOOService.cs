using System;
using System.Linq;
using System.Collections.Generic;
using HOO.ComLib;
using HOO.Core.Model.Universe;
//using HOO.SvcLib.Helpers;
using HOO.Log;
using HOO.SvcLib.Helpers;
using HOO.Core.Model;
using Newtonsoft.Json;
using HOO.Core.Model.Configuration;

namespace HOO.SvcLib
{
    public class HOOService : IHOOService
    {
        private List<Universe> World;
        private List<Player> AllPlayers;
        private List<Product> AllProducts;
        //private UniverseHelper uHelper;
        private GalaxyHelper gHelper;
        private StarHelper sHelper;
        private PlayerHelper pHelper;
        private Logger log;

        public HOOService(List<Universe> world, List<Player> players, List<Product> allProducts)
        {
            log = new Logger("HOO.SvcLib", this.GetType());
            log.Entry.MethodName = "Constructor";
            log.Entry.StepName = "Start";
            //            log.Debug(dep.ToString());
            this.AllProducts = allProducts;
            this.AllPlayers = players;
            this.World = world;
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

        public OnlinePlayer RegisterNewPlayerJS(string sessionId, string leaderName, string race, string motto, string color, string userName, string password, string eMail)
        {
            pHelper = new PlayerHelper();

            pHelper.Player = new Player() { Email = eMail, Color = color, LeaderName = leaderName, Motto = motto, Password = password, Race = race, Username = userName };
            pHelper.Register();

            return pHelper.Player;
        }

        public OnlinePlayer AuthPlayer(string sessionId, string userName, string password)
        {
            var p = AllPlayers.SingleOrDefault(x=>x.Username == userName && x.Password == password);
            if (p != null)
            {
                p.TokenID = MrCrypt.GetMD5Hash(sessionId+userName+"ThisIsMySalt");
                p.LastActivity = DateTime.Now;
                p.IsSaved = false;
            }

            return p;
        }

        public OnlinePlayer RefreshPlayer(string sessionId, string userName, string tokenId)
        {
            var tId = MrCrypt.GetMD5Hash(sessionId + userName + "ThisIsMySalt");
            if (tId != tokenId)
                return null;

            var p = AllPlayers.SingleOrDefault(x => x.Username == userName && x.TokenID == tId);
            if (p != null)
            {
                p.LastActivity = DateTime.Now;
                p.IsSaved = false;
            }

            return p;
        }

        public OnlinePlayer SetPlayerInitAttrs(string sessionId, string userName, string tokenId, List<OAttribute> attributes)
        {
            var tId = MrCrypt.GetMD5Hash(sessionId + userName + "ThisIsMySalt");
            if (tId != tokenId)
                return null;

            var p = AllPlayers.SingleOrDefault(x => x.Username == userName && x.TokenID == tId);
            if (p != null)
            {
                var halves = new[] { -1M, -0.5M, 0M, 0.5M, 1M };
                var quarters = new[] { -0.5M, -0.25M, 0M, 0.25M, 0.5M };

                var bProd = attributes.SingleOrDefault(a => a.AttributeType == AttributeType.RaceBonus && a.Attribute == ObjectAttribute.BaseProduction);
                var bFarm = attributes.SingleOrDefault(a => a.AttributeType == AttributeType.RaceBonus && a.Attribute == ObjectAttribute.BaseFarming);
                var bRes = attributes.SingleOrDefault(a => a.AttributeType == AttributeType.RaceBonus && a.Attribute == ObjectAttribute.BaseResearch);
                var bGrow = attributes.SingleOrDefault(a => a.AttributeType == AttributeType.RaceBonus && a.Attribute == ObjectAttribute.BasePopulation);

                if (bProd == null || !halves.Contains((decimal)bProd.Value) ||
                    bFarm == null || !halves.Contains((decimal)bFarm.Value) ||
                    bRes == null || !halves.Contains((decimal)bRes.Value) ||
                    bGrow == null || !quarters.Contains((decimal)bGrow.Value))
                    return null;

                p.Attributes.RemoveAll(a => a.Attribute == ObjectAttribute.BaseProduction && a.AttributeType == AttributeType.RaceBonus);
                p.Attributes.RemoveAll(a => a.Attribute == ObjectAttribute.BaseFarming && a.AttributeType == AttributeType.RaceBonus);
                p.Attributes.RemoveAll(a => a.Attribute == ObjectAttribute.BaseResearch && a.AttributeType == AttributeType.RaceBonus);
                p.Attributes.RemoveAll(a => a.Attribute == ObjectAttribute.BasePopulation && a.AttributeType == AttributeType.RaceBonus);

                p.Attributes.AddRange(new[] { bProd, bFarm, bRes, bGrow });

                p.LastActivity = DateTime.Now;
                p.IsSaved = false;

                return p;
            }

            return p;
        }

        public Galaxy AddNewGalaxy()
        {
            gHelper = new GalaxyHelper();
            gHelper.Galaxy = new Galaxy(1000, 800, 600);
            gHelper.Galaxy.UniverseId = this.World[0]._id;
            gHelper.Galaxy.Name = "M83";
            gHelper.Galaxy._id = this.World[0].Galaxies.Count;
            gHelper.Save();
            return gHelper.Galaxy;
        }

        public List<Product> GetAvailableProducts(string objectType, int OBID, string sessionId, string userName, string tokenId)
        {
            var tId = MrCrypt.GetMD5Hash(sessionId + userName + "ThisIsMySalt");
            if (tId != tokenId)
                return null;

            var p = AllPlayers.SingleOrDefault(x => x.Username == userName && x.TokenID == tId);
            if (p != null)
            {
                List<Product> res = new List<Product>();
                if (objectType == "Planet")
                {
                    Star s = World[0].Galaxies[0].Stars.Where(x => x.OrbitalBodies.Exists(b => b._id == OBID)).SingleOrDefault();
                    Planet o = (Planet)s.OrbitalBodies.Where(b => b._id == OBID).SingleOrDefault();

                    if (!((Planet)o).Attributes.Exists(a => a.Attribute == ObjectAttribute.Owner))
                    {
                        var prods = AllProducts.Where(x => x.Type == ProductType.UnInhabitatedPlanetConstruction);
                        if (prods != null)
                            prods = prods.Where(x => !x.Reqs.Exists(r =>
                                                                      r.AttributeType == AttributeType.FiniteRequirement && (!p.Attributes.Exists(a => a.AttributeType == r.AttributeType && a.Attribute == r.Attribute) ||
                                                                                                                              (float)p.Attributes.SingleOrDefault(a => a.Attribute == r.Attribute && a.AttributeType == r.AttributeType).Value < (float)r.Value)));

                        if (prods != null)
                            res = prods.ToList();
                    }
                    return res;
                }
            }
            return new List<Product>();
        }

        public string GetWorldState()
        {
            return JsonConvert.SerializeObject(new { Period = this.World[0].CurrentPeriod, Turn = this.World[0].CurrentTurn, Tick = this.World[0].CurrentTick });
        }

        public Star GetRandomStarForHomeWorld(string sessionId, string userName, string tokenId)
        {
            var tId = MrCrypt.GetMD5Hash(sessionId + userName + "ThisIsMySalt");
            if (tId != tokenId)
                return null;

            var p = AllPlayers.SingleOrDefault(x => x.Username == userName && x.TokenID == tId);
            if (p != null)
            {
                var stars = World[0].Galaxies[0].Stars.Where(s => !s.Attributes.Exists(a => a.Attribute == ObjectAttribute.Owner) && s.OrbitalBodies.Exists(o => o is Planet)).ToArray();
                return stars[MrRandom.rnd.Next(stars.Length)];
            }

            return new Star();
        }

        public string HelloWorld(string name)
        {
            return String.Format("Hello {0}", name);
        }
    }
}