using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    public class Universe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrip { get; set; }
		public Attributes Attributes;
		public Effects Effects;
		public bool IsLoaded { get; set; }
		public bool IsSaved { get; set; }
		public int CurrentTick { get; set; }
		public int CurrentTurn { get; set; }
		public int CurrentPeriod { get; set; }

        public List<Galaxy> Galaxies { get; set; } 
        public Universe()
        {
            this.Galaxies = new List<Galaxy>();
			this.Attributes = new Attributes ();
			this.Effects = new Effects ();
			this.IsLoaded = this.IsSaved = false;
        }
    }
}
