using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    public class Universe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrip { get; set; }
		public Attributes.Attributes Attributes;

        public List<Galaxy> Galaxies { get; set; } 
        public Universe()
        {
            this.Galaxies = new List<Galaxy>();
			this.Attributes = new HOO.Core.Model.Attributes.Attributes ();
        }
    }
}
