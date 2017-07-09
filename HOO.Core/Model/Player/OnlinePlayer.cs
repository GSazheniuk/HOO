using HOO.Core.Model.Universe;
using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model
{
    [DataContract]
    [KnownType(typeof(Player))]
    [BsonKnownTypes(typeof(Player))]
    public class OnlinePlayer : BaseObject
    {
        [BsonIgnore]
        [DataMember]
        public string TokenID { get; set; }

        [DataMember]
        public string LeaderName { get; set; }
        [DataMember]
        public string Race { get; set; }
        [DataMember]
        public string Motto { get; set; }
        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public DateTime LastActivity { get; set; }

        public OnlinePlayer()
            : base()
        {

        }
    }
}
