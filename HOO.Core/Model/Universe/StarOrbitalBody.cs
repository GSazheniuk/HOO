using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model.Universe
{
    [DataContract]
    [KnownType(typeof(Planet))]
    [KnownType(typeof(GasGiant))]
    [KnownType(typeof(AsteroidBelt))]
    [BsonKnownTypes(typeof(Planet))]
    [BsonKnownTypes(typeof(GasGiant))]
    [BsonKnownTypes(typeof(AsteroidBelt))]
    public class StarOrbitalBody : BaseObject
    {
        [IgnoreDataMember]
        private string _dummy;

        [DataMember]
        public int OrbitNo { get; set; }

        [DataMember]
        public long StarId { get; set; }

        [DataMember]
        public string StarSystemName { get; set; }

        //[BsonIgnore]
        [DataMember]
        public string OrbitalBodyType
        {
            get { return this.GetType().ToString(); }
            set { this._dummy = value; }
        }

        public StarOrbitalBody() : base()
        {

        }

        public StarOrbitalBody(long sId) : base()
        {
            this.StarId = sId;
        }
    }
}
