using HOO.Core.Model.Configuration.Enums;
using System.Runtime.Serialization;

namespace HOO.Core.Model.Universe
{
    [DataContract]
    public class GasGiant : StarOrbitalBody
    {
        [DataMember]
		public GasGiantSize Size { get; set; }
        [DataMember]
		public GasGiantClass Class { get; set; }

		public GasGiant(long sId)
            : base(sId)
        {

        }
    }
}
