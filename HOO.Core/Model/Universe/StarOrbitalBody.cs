using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model.Universe
{
    [Serializable]
    [KnownType(typeof(Planet))]
    [KnownType(typeof(GasGiant))]
    [KnownType(typeof(AsteroidBelt))]
    [BsonKnownTypes(typeof(Planet))]
    [BsonKnownTypes(typeof(GasGiant))]
    [BsonKnownTypes(typeof(AsteroidBelt))]
    public class StarOrbitalBody : BaseObject
    {
        public int OrbitNo { get; set; }
        public long StarId { get; set; }
        public string StarSystemName { get; set; }

        public StarOrbitalBody(long sId):base()
		{
			this.StarId = sId;
			this.ObjectType = 4;
        }
    }
}
