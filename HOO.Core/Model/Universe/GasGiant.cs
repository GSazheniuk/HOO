using HOO.Core.Model.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class GasGiant : StarOrbitalBody
    {
		public GasGiantSize Size { get; set; }
		public GasGiantClass Class { get; set; }

		public GasGiant(Star s)
            : base(s)
        {

        }
    }
}
