namespace HOO.Core.Model.Universe
{
    public class StarOrbitalBody : BaseObject
    {
        public int OrbitNo { get; set; }
        public Star Star { get; set; }

		public StarOrbitalBody(Star s):base()
		{
			this.Star = s;
			this.ObjectType = 4;
		}
    }
}
