using HOO.Core.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class Galaxy
    {
        public string Name { get; set; }
        public BlackHole BlackHole { get; set; }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int DimensionZ { get; set; }

        public List<Star> Stars { get; set; }

        public Galaxy(int x, int y, int z)
        {
            this.DimensionX = x;
            this.DimensionY = y;
            this.DimensionZ = z;
            this.Stars = new List<Star>();
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
