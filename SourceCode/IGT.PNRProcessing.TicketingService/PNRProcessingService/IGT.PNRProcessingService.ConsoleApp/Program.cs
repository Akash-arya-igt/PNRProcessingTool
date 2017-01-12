using IGT.PNRProcessing.WorkflowLibrary;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkFlowSettingAction objWFSetting = new WorkFlowSettingAction();
            List<GetTicketingFlowSettings> lstFlowSettings = objWFSetting.GetTicketFlowSettingList();

            foreach (GetTicketingFlowSettings objTktFlowSetting in lstFlowSettings)
            {
                var input = new Dictionary<string, object> { { "TicketingFlowSetting", objTktFlowSetting } };

                TicketingWorkflow objTktWf = new TicketingWorkflow();
                WorkflowInvoker.Invoke(objTktWf, input);
                //WorkflowApplication objWF = new WorkflowApplication(new TicketingWorkflow());
                //objWF.Run();
            }
        }
    }
}
