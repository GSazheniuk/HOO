using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class Galaxy
    {
        public string Name { get; set; }
        public BlackHole BlackHole { get; set; }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int DimensionZ { get; set; }

        public List<Star> Stars { get; set; }
    }
}
