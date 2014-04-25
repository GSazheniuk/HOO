using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    public class Universe
    {
        public List<Galaxy> Galaxies { get; set; } 
        public Universe(int galaxyCount)
        {
            for (int i = 0; i < galaxyCount; i++)
            {
                Galaxies.Add(new Galaxy());
            }
        }
    }
}
