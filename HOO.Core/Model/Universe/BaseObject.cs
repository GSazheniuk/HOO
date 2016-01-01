using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    [Serializable]
    public class BaseObject
	{
        [BsonId]
        public long _id { get; set; }

        public int OBID { get; set; }
		public List<OAttribute> Attributes;

        [BsonIgnore]
        public bool IsLoaded { get; set; }

        [BsonIgnore]
        public bool IsSaved { get; set; }

        public int ObjectType { get; set; }

		public BaseObject ()
		{
            this.Attributes = new List<OAttribute>();
			this.IsLoaded = this.IsSaved = false;
		}
	}
}

