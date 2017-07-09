using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;

namespace HOO.Core.Model
{
    public class Product : BaseObject
    {
        public ProductType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<OAttribute> Reqs { get; set; }

        public Product()
        {
            this.Reqs = new List<OAttribute>();
        }
    }
}
