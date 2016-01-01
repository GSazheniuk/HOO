using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model.Universe
{
    public class Universe : BaseObject
    {
        public string Name { get; set; }

        [BsonIgnore]
        public string Descrip { get; set; }

        public string Description { get; set; }

        [BsonIgnore]
        public int CurrentTick { get; set; }

        [BsonIgnore]
        public int CurrentTurn { get; set; }

        [BsonIgnore]
        public int CurrentPeriod { get; set; }

        public int Tick { get; set; }
        public int Turn { get; set; }
        public int Period { get; set; }

        [BsonIgnore]
        public List<Galaxy> Galaxies { get; set; }

        [BsonIgnore]
        public List<OnlinePlayer> OnlinePlayers { get; set; }

        public int[] GalaxyArray { get; set; }

		public Universe():base()
		{
			this.Galaxies = new List<Galaxy> ();
		}
	}
}
