using HOO.Core.Model;
using HOO.Core.Model.Universe;
using HOO.SvcLib;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace HOO.Service
{
    public class HOOWebInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly List<Universe> World;
        private readonly List<Player> Players;
        private readonly List<Product> AllProducts;

        public HOOWebInstanceProvider(List<Universe> world, List<Player> players, List<Product> allProducts)
        {
            this.World = world;
            this.Players = players;
            this.AllProducts = allProducts;
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new HOOService(World, Players, AllProducts);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}
