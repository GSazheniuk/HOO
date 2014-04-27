using HOO.Core.Model.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
