using HOO.Core.Model.Configuration.Enums;

namespace HOO.Core.Model.Universe
{
    public class AsteroidBelt : StarOrbitalBody
    {
		public AsteroidDensity Density { get; set; }
		public AsteroidType Type { get; set; }

        public AsteroidBelt(Star s)
            : base(s)
        {

        }
    }
}
