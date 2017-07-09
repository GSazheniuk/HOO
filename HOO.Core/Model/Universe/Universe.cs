using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

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

        public int Tick { get { return this.CurrentTick; } set { this.CurrentTick = value; } }
        public int Turn { get { return this.CurrentTurn; } set { this.CurrentTurn = value; } }
        public int Period { get { return this.CurrentPeriod; } set { this.CurrentPeriod = value; } }

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
