using HOO.Core.Model.Universe;
using HOO.Core.Model.Configuration.Enums;
using System.Runtime.Serialization;

namespace HOO.Core.Model
{
    public class Player : OnlinePlayer
    {
        [IgnoreDataMember]
        public string Username { get; set; }
        [IgnoreDataMember]
        public string Email { get; set; }
        [IgnoreDataMember]
        public string Password { get; set; }

		public Player():base()
		{
			this.ObjectType = (int)ObjectTypes.Player;
		}
    }
}
