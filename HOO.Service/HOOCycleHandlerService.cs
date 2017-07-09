using HOO.ComLib;
using HOO.Core.Model;
using HOO.Core.Model.Universe;
using HOO.Log;
using HOO.SvcLib;
using HOO.SvcLib.Helpers;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Timers;

namespace HOO.Service
{
    public partial class HOOCycleHandlerService : ServiceBase
    {
        public ServiceHost host = null;
        public WebServiceHost webhost = null;
        private bool tickEnabled = true;
        private Logger log;

        private List<Universe> World;
        private List<Player> Players;
        private List<Product> AllProducts;

        private UniverseHelper uHelper;
        private GalaxyHelper gHelper;
        private StarHelper sHelper;
        private StarOrbitalBodyHelper sobHelper;
        private PlayerHelper pHelper;

        public HOOCycleHandlerService()
        {
            InitializeComponent();
            InitHelpers();
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
            tLoadData.Start();
            tStartWCF.Start();
            tStartRest.Start();
            t.Start();
            tTurn.Start();
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

            if (this.webhost != null)
            {
                try
                {
                    this.webhost.Close();
                }
                catch
                {
                }
                finally
                {
                    this.webhost = null;
                }
            }

            if (t != null && t.Enabled)
                t.Stop();
            if (tLoadData != null && tLoadData.Enabled)
                tLoadData.Stop();
            if (tStartWCF != null && tStartWCF.Enabled)
                tStartWCF.Stop();
            if (tStartRest != null && tStartRest.Enabled)
                tStartRest.Stop();
        }
    }
}
