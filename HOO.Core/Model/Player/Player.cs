using HOO.Core.Model.Universe;
using HOO.Core.Model.Configuration.Enums;

namespace HOO.Core.Model.Universe
{
    public class Player : BaseObject
    {
        public string LeaderName { get; set; }
        public string Race { get; set; }
		public string Motto { get; set; }
		public string Color { get; set; }

		public Player():base()
		{
			this.ObjectType = (int)ObjectTypes.Player;
		}
    }
}
