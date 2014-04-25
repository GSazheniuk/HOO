using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class Planet : StarOrbitalBody
    {
        public Planet(Star s)
            : base(s)
        {

        }
    }
}
