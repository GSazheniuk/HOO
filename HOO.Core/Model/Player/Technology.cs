using HOO.Core.Model.Configuration.Enums;
using System.Collections.Generic;

namespace HOO.Core.Model.Universe
{
    public class Technology
    {
        public TechnologyGroup TechGroup { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }

        public int RPCost { get; set; }
        public List<Requisite> PreRequisites { get; set; }
    }
}
