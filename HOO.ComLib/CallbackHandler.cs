using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HOO.ComLib
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CallbackHandler 
    {
        public string Hello(string name)
        {
            Console.WriteLine("Hellp {0} ", name);
            return "hell";
        }
    }
}
