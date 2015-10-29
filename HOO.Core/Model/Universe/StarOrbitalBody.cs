using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class StarOrbitalBody
    {
        public int Id { get; set; }
        public int OrbitNo { get; set; }
        public Star Star { get; set; }
		public bool IsLoaded;
		public bool IsSaved;


        public StarOrbitalBody(Star s)
        {
            this.Star = s;
			this.IsLoaded = this.IsSaved = false;
        }
    }
}
