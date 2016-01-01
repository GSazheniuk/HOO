using HOO.Core.Model.Universe;
using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model
{
    [Serializable]
    [KnownType(typeof(Player))]
    [BsonKnownTypes(typeof(Player))]
    public class OnlinePlayer : BaseObject
    {
        [BsonIgnore]
        public string TokenID { get; set; }

        public string LeaderName { get; set; }
        public string Race { get; set; }
        public string Motto { get; set; }
        public string Color { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
