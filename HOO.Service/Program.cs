using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(params string[] args)
        {
            HOOCycleHandlerService svc = new HOOCycleHandlerService();
            ServiceBase[] ServicesToRun;
            if (!Environment.UserInteractive)
            {
                ServicesToRun = new ServiceBase[] { svc };
                ServiceBase.Run(ServicesToRun);
            }
            else
                svc.Start(args);
        }
    }
}
