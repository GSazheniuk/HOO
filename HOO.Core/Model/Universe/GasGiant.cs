using HOO.Core.Model.Configuration.Enums;

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
