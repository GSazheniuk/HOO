using HOO.Core.Model.Configuration;
using HOO.Core.Model.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model.Universe
{
    public class Star : BaseObject
    {
        [BsonIgnore]
        private string _dummy;

        [DataMember]
        public StarClass Class { get; set; }

        [DataMember]
        public int TemperatureLevel { get; set; }

        [DataMember]
        public StarSize Size { get; set; }

        public long GalaxyId { get; set; }

        [DataMember]
        public string StarSystemName { get; set; }

        [DataMember]
        public Point3D Coordinates { get; set; }

        [BsonIgnore]
        [DataMember]
        public List<StarOrbitalBody> OrbitalBodies { get; set; }

        [IgnoreDataMember]
        public long[] OrbitalIDs { get; set; }

        [BsonIgnore]
        [DataMember]
        public string ClassName
        {
            get{ return String.Format("{0}{1}{2}", Class, TemperatureLevel, Size); }
//            set{ this._dummy = value; }
        }

		private void InitStar()
		{
			this.OrbitalBodies = new List<StarOrbitalBody>();
			this.Class = ((StarClass)MrRandom.rnd.Next((int)StarClass.MrRandom));
			this.Size = ((StarSize) MrRandom.rnd.Next((int) StarSize.MrRandom));
			this.TemperatureLevel = MrRandom.rnd.Next(ConstantParameters.MaxStarTemperatureLevel);
            this.StarSystemName = "";
//			this.Attributes.Temperature = TemperatureLevel;
		}

        public Star():base()
        {
			InitStar ();
        }

        public Star(Galaxy g):base()
        {
            this.GalaxyId = g._id;
			InitStar ();
        }
    }
}
