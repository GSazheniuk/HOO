using HOO.Core.Model.Configuration;
using HOO.Core.Model.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class Star
    {
        public int Id { get; set; }
        public StarClass Class { get; set; }
        public int TemperatureLevel { get; set; }
        public StarSize Size { get; set; }
        public Galaxy Galaxy { get; set; }
        public string StarSystemName { get; set; }

        public Point3D Coordinates { get; set; }

        public List<StarOrbitalBody> OrbitalBodies { get; set; }

        public string ClassName
        {
            get { return String.Format("{0}{1}{2}", Class, TemperatureLevel, Size); }
        }

        public Star()
        {
            this.OrbitalBodies = new List<StarOrbitalBody>();
        }

        public Star(Galaxy g)
        {
            this.Galaxy = g;
            this.Class = ((StarClass)MrRandom.rnd.Next((int)StarClass.MrRandom));
            this.Size = ((StarSize) MrRandom.rnd.Next((int) StarSize.MrRandom));
            this.TemperatureLevel = MrRandom.rnd.Next(ConstantParameters.MaxStarTemperatureLevel);
            this.OrbitalBodies = new List<StarOrbitalBody>();
        }
    }
}
