using HOO.Core.Model;
using HOO.Core.Model.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Service
{
    public class HOOServiceHostFactory: ServiceHostFactory
    {
        private readonly List<Universe> _world;
        private readonly List<Player> _players;
        private readonly List<Product> _products;

        public HOOServiceHostFactory()
        {
            this._world = new List<Universe>();
            this._players = new List<Player>();
            this._products = new List<Product>();
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new HOOServiceHost(this._world, this._players, this._products, serviceType, baseAddresses);
        }

        protected WebServiceHost CreateWebServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new HOOWebServiceHost(this._world, this._players, this._products, serviceType, baseAddresses);
        }
    }
}
