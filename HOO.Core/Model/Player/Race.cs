using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Player
{
    public class Race
    {
        public string Specie { get; set; }

        public double ProductionBonus { get; set; }
        public double FarmingBonus { get; set; }
        public double ResearchBonus { get; set; }
        public double PopulationGrowthBonus { get; set; }

    }
}
