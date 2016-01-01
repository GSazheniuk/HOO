using HOO.Core.Model.Configuration.Enums;
using System.Runtime.Serialization;

namespace HOO.Core.Model.Universe
{
    [DataContract]
    public class AsteroidBelt : StarOrbitalBody
    {
        [DataMember]
		public AsteroidDensity Density { get; set; }
        [DataMember]
        public AsteroidType Type { get; set; }

        public AsteroidBelt(long sId)
            : base(sId)
        {

        }
    }
}
