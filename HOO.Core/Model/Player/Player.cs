using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Player
{
    public class Player
    {
        public string LeaderName { get; set; }
        public Race Race { get; set; }

        public Star HomeWorld { get; set; }
        public List<Planet> ControledPlanets { get; set; }

        public double Treasury { get; set; }
        public double Income { get; set; }
    }
}
