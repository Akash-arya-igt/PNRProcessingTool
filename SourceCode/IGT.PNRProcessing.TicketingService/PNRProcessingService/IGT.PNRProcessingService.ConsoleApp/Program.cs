using IGT.PNRProcessing.WorkflowLibrary;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;
using IGT.PNRProcessingService.BusinessEngine.LoggerEngine;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.ConsoleApp
{
    class Program
    {
        public const string ServiceName = "IGT.AutoPNRProcessingService";

        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = Program.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.Start(args);
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }


        static void Main(string[] args)
        {
            try
            {
                Start(args);
                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                EventLogger.WriteLog(ex.Message);
            }
            //if (!Environment.UserInteractive)
            //    // running as service
            //    using (var service = new Service())
            //        ServiceBase.Run(service);
            //else
            //{
            //    // running as console app
            //    Start(args);

            //    Console.WriteLine("Press any key to stop...");
            //    Console.ReadKey(true);

            //    Stop();
            //}            
        }


        private static void Start(string[] args)
        {
            WorkFlowSettingAction objWFSetting = new WorkFlowSettingAction();
            List<GetTicketingFlowSettings> lstFlowSettings = objWFSetting.GetTicketFlowSettingList();

            foreach (GetTicketingFlowSettings objTktFlowSetting in lstFlowSettings)
            {
                string strHAP = objTktFlowSetting.HAPDetail.Profile;

                if (!string.IsNullOrEmpty(strHAP) && strHAP.Trim().ToUpper() == "DYNGALILEOCOPY_5KL6")
                {
                    Console.WriteLine("Robo started for " + strHAP.Trim().ToUpper());
                    var input = new Dictionary<string, object> { { "TicketingFlowSetting", objTktFlowSetting }, { "IsFlowEnable", true } };

                    TicketingWorkflow objTktWf = new TicketingWorkflow();
                    WorkflowInvoker.Invoke(objTktWf, input);
                    
                }
            }
        }

        private static void Stop()
        {
            //objTktWf.
        }

    }
}
