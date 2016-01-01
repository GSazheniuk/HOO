using HOO.ComLib;
using HOO.Core.Model.Universe;
using HOO.Log;
using HOO.SvcLib;
using HOO.SvcLib.Helpers;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Timers;

namespace HOO.Service
{
    public partial class HOOCycleHandlerService : ServiceBase
    {
        public ServiceHost host = null;
        private Timer t;
        private bool tickEnabled = true;
        private Logger log;

        private List<Universe> World;
        private UniverseHelper uHelper;
        private GalaxyHelper gHelper;

        public HOOCycleHandlerService()
        {
            InitializeComponent();
            log = new Logger("HOO.Service", this.GetType());
        }

        public void Start(string[] args)
        {
            OnStart(args);
            while (true)
            {
                System.Threading.Thread.Sleep(60000);
            }
        }

        protected override void OnStart(string[] args)
        {
            t = new Timer(60000);
            t.Elapsed += T_Elapsed;
            uHelper = new UniverseHelper();

            if (this.host != null)
            {
                this.host.Close();
            }

            try
            {
                log.Entry.MethodName = "Timer.Elapsed";
                log.Entry.StepName = "Start";
                if (World == null || World.Count < 1)
                {
                    this.World = uHelper.AllUniverses();
                    log.Entry.StepName = "World loaded";
                    if (World.Count > 0)
                        log.Debug(String.Format("Universes: {0}, Galaxies: {1}", this.World.Count, this.World[0].Galaxies.Count));
                    else
                        log.Debug(String.Format("Universes: {0}", this.World.Count));
                }

                this.host = new HOOServiceHost(this.World, typeof(HOOService), new Uri[0]);
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

            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!tickEnabled)
                return;

            tickEnabled = false;
            try
            {
                log.Entry.MethodName = "Timer.Elapsed";
                log.Entry.StepName = "Start";
                if (World == null || World.Count < 1)
                {
                    this.World = uHelper.AllUniverses();
                    log.Entry.StepName = "World loaded";
                    log.Debug(String.Format("Universes: {0}", this.World.Count));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                tickEnabled = true;
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

            t.Stop();
        }
    }
}
