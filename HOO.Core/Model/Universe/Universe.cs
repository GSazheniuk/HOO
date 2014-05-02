using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    public class Universe
    {
        public List<Galaxy> Galaxies { get; set; } 
        public Universe()
        {
            this.Galaxies = new List<Galaxy>();
        }
    }
}
