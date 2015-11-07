using HOO.Core.Model.Configuration.Enums;
namespace HOO.Core.Model.Universe
{
    public class Planet : StarOrbitalBody
    {
        public string PlanetFriendlyName { get { return string.Format("{0}-{1}", this.Star.StarSystemName, this.OrbitNo); } }
        public PlanetSize Size { get; set; }
        public PlanetType Type { get; set; }

        public Planet(Star s)
            : base(s)
        {

        }
    }
}
