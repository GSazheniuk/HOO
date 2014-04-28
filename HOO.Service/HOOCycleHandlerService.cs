using HOO.ComLib;
using HOO.SvcLib;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;

namespace HOO.Service
{
    public partial class HOOCycleHandlerService : ServiceBase
    {
        public ServiceHost host = null;
        public HOOCycleHandlerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (this.host != null)
            {
                this.host.Close();
            }
            try
            {
                this.host = new ServiceHost(typeof(HOOService), new Uri[0]);
                this.host.CloseTimeout = TimeSpan.MaxValue;
                this.host.OpenTimeout = TimeSpan.MaxValue;
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None)
                {
                    MaxBufferPoolSize = 0x7fffffffL,
                    MaxBufferSize = 0x7fffffff,
                    MaxConnections = 0x4b0,
                    MaxReceivedMessageSize = 0x7fffffffL
                };
                this.host.AddServiceEndpoint(typeof(IHOOService), binding, "net.tcp://localhost:8000/HOOService");
                foreach (ServiceEndpoint endpoint in this.host.Description.Endpoints)
                {
                    endpoint.Behaviors.Add(new HOOBehavior());
                }
                ServiceDebugBehavior behavior = this.host.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (behavior == null)
                {
                    ServiceDebugBehavior item = new ServiceDebugBehavior
                    {
                        IncludeExceptionDetailInFaults = true
                    };
                    this.host.Description.Behaviors.Add(item);
                }
                else if (!behavior.IncludeExceptionDetailInFaults)
                {
                    behavior.IncludeExceptionDetailInFaults = true;
                }
                foreach (ServiceEndpoint endpoint2 in this.host.Description.Endpoints)
                {
                    foreach (OperationDescription description in endpoint2.Contract.Operations)
                    {
                        DataContractSerializerOperationBehavior behavior3 = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
                        if (behavior3 != null)
                        {
                            behavior3.MaxItemsInObjectGraph = 0x989680;
                        }
                    }
                }
                this.host.Open();
                this.EventLog.WriteEntry("Open OK");
            }
            catch (Exception exception)
            {
                this.EventLog.WriteEntry(exception.Message);
            }
        }

        protected override void OnStop()
        {
            if (this.host != null)
            {
                try
                {
                    this.host.Close();
                }
                catch
                {
                }
                finally
                {
                    this.host = null;
                }
            }
        }
    }
}
