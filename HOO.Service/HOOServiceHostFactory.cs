using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Service
{
    public class HOOServiceHostFactory: ServiceHostFactory
    {
        private readonly object dep;

        public HOOServiceHostFactory()
        {
            this.dep = new object();
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new HOOServiceHost(this.dep, serviceType, baseAddresses);
        }
    }
}
