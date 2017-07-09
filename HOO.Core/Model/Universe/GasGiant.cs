using HOO.Core.Model.Configuration.Enums;
using System.Runtime.Serialization;

namespace HOO.Core.Model.Universe
{
    public class GasGiant : StarOrbitalBody
    {
        [DataMember]
		public GasGiantSize Size { get; set; }
        [DataMember]
		public GasGiantClass Class { get; set; }

        public GasGiant()
            : base()
        {

        }

        public GasGiant(long sId)
            : base(sId)
        {

        }
    }
}
