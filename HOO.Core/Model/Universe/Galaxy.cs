using HOO.Core.Model.Configuration;
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace HOO.Core.Model.Universe
{
    public class Galaxy : BaseObject
    {
//        [BsonIgnore]
//        private int _dummy;

        //[BsonIgnore]
        //internal Universe Universe { get; set; }

        public long UniverseId { get; set; }

        public string Name { get; set; }

        [BsonIgnore]
        public BlackHole BlackHole { get; set; }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int DimensionZ { get; set; }

        [BsonIgnore]
        public List<Star> Stars { get; set; }

		private void InitGalaxy()
		{
			this.Stars = new List<Star>();
//			this.Attributes.RadiationLevel = 3;
		}

        public Galaxy():base()
        {
			InitGalaxy ();
        }

        public Galaxy(int x, int y, int z):base()
		{
			this.DimensionX = x;
			this.DimensionY = y;
			this.DimensionZ = z;
			InitGalaxy ();
		}

        public bool AddStar(Star s, double minDistance)
        {
            int x = ((MrRandom.rnd.Next(2) == 0) ? 1 : -1) * MrRandom.rnd.Next(this.DimensionX);
            int y = ((MrRandom.rnd.Next(2) == 0) ? 1 : -1) * MrRandom.rnd.Next(this.DimensionY);
            int z = ((MrRandom.rnd.Next(2) == 0) ? 1 : -1) * MrRandom.rnd.Next(this.DimensionZ);

                double d = Math.Pow(x, 2) / Math.Pow(this.DimensionX, 2) + Math.Pow(y, 2) / Math.Pow(this.DimensionY, 2) + Math.Pow(z, 2) / Math.Pow(this.DimensionZ, 2);
                double dc = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

                if (d <= 1 && dc > minDistance)
                {
                    if (!Stars.Exists(p => Math.Sqrt(Math.Pow(x - p.Coordinates.X, 2) + Math.Pow(y - p.Coordinates.Y, 2) + Math.Pow(z - p.Coordinates.Z, 2)) <= minDistance))
                    {
                        s.Coordinates = new Point3D {X = x, Y = y, Z = z};
                        Stars.Add(s);
                        return true;
                    }
                }
            return false;
        }
    }
}
