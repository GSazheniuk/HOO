using HOO.Core.Model.Configuration.Enums;

namespace HOO.Core.Model.Player
{
    public class Requisite
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public RequisiteType Type { get; set; }

        public object Criteria { get; set; }
    }
}
