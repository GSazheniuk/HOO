using HOO.Core.Model.Configuration.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace HOO.Core.Model.Universe
{
    [DataContract]
    public class Planet : StarOrbitalBody
    {
        [IgnoreDataMember]
        private string _dummy;

        [DataMember]
        [BsonIgnore]
        public string PlanetFriendlyName { get { return string.Format("{0}-{1}", this.StarSystemName, this.OrbitNo); } set { this._dummy = value; } }

        [DataMember]
        public PlanetSize Size { get; set; }

        [DataMember]
        public PlanetType Type { get; set; }

        public Planet(long sId)
            : base(sId)
        {

        }
    }
}
