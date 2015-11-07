using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    public class Universe : BaseObject
    {
        public string Name { get; set; }
        public string Descrip { get; set; }
		public int CurrentTick { get; set; }
		public int CurrentTurn { get; set; }
		public int CurrentPeriod { get; set; }

        public List<Galaxy> Galaxies { get; set; } 

		public Universe():base()
		{
			this.Galaxies = new List<Galaxy> ();
		}
	}
}
