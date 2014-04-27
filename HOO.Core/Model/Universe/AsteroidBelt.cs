using HOO.Core.Model.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class AsteroidBelt : StarOrbitalBody
    {
        public AsteroidDensity Density { get; set; }

        public AsteroidBelt(Star s)
            : base(s)
        {

        }
    }
}
