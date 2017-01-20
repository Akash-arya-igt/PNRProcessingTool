using IGT.PNRProcessingService.BusinessEngine.LoggerEngine;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new WorkflowService()
                };
                ServiceBase.Run(ServicesToRun);
                NLogManager._instance.LogMsg(NLogLevel.Trace, "Robo service started successfully");
            }
            else
            {
                WorkflowService objWF = new WorkflowService();
                objWF.RunService();
            }
        }
    }
}
