using IGT.PNRProcessingService.BusinessEngine.DALEngine;
using IGT.PNRProcessingService.BusinessEngine.LoggerEngine;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessing.WorkflowLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkFlowSettingAction objWFSetting = new WorkFlowSettingAction();
            List<GetTicketingFlowSettings> lstFlowSettings = objWFSetting.GetTicketFlowSettingList();

            foreach (GetTicketingFlowSettings objTktFlowSetting in lstFlowSettings)
            {
                string strHAP = objTktFlowSetting.HAPDetail.Profile;

                if (!string.IsNullOrEmpty(strHAP) && strHAP.Trim().ToUpper() == "DYNGALILEOCOPY_5KL6")
                {
                    NLogManager._instance.LogMsg(NLogLevel.Trace, "Robo started for " + strHAP.Trim().ToUpper());
                    Console.WriteLine("Robo started for " + strHAP.Trim().ToUpper());
                    var input = new Dictionary<string, object> { { "TicketingFlowSetting", objTktFlowSetting }, { "IsFlowEnable", true } };

                    TicketingWorkflow objTktWf = new TicketingWorkflow();
                    WorkflowInvoker.Invoke(objTktWf, input);
                }
            }
        }
    }
}
