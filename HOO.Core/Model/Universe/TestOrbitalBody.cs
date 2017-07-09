using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class TestOrbitalBody : StarOrbitalBody
    {
        [IgnoreDataMember]
        private string _dummy;

        //        [BsonIgnore]
        [DataMember]
        public string PlanetFriendlyName { get { return string.Format("{0}-{1}", this.StarSystemName, this.OrbitNo); } set { this._dummy = value; } }

        [IgnoreDataMember]
        public string Size { get; set; }

        [IgnoreDataMember]
        public string Type { get; set; }

        public TestOrbitalBody()
            : base()
        {

        }

        public TestOrbitalBody(long sId)
            : base(sId)
        {

        }
    }
}
