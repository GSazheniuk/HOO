using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    [DataContract]
    public class BaseObject
	{
        [BsonId]
        [DataMember]
        public long _id { get; set; }

        [DataMember]
        public List<OAttribute> Attributes;

        [IgnoreDataMember]
        [BsonIgnore]
        public int OBID { get; set; }

        [IgnoreDataMember]
        [BsonIgnore]
        public bool IsLoaded { get; set; }

        [IgnoreDataMember]
        [BsonIgnore]
        public bool IsSaved { get; set; }

        [IgnoreDataMember]
        [BsonIgnore]
        public int ObjectType { get; set; }

		public BaseObject ()
		{
            this.Attributes = new List<OAttribute>();
			this.IsLoaded = this.IsSaved = false;
		}
	}
}

