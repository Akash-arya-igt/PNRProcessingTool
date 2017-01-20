using IGT.PNRProcessing.WorkflowLibrary;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;
using IGT.PNRProcessingService.BusinessEngine.LoggerEngine;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.WinService
{
    partial class WorkflowService : ServiceBase
    {
        List<TicketingWorkflow> _lstWF = new List<TicketingWorkflow>();
        public WorkflowService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
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
                        _lstWF.Add(objTktWf);
                        WorkflowInvoker.Invoke(objTktWf, input);
                    }
                }
            }
            catch(Exception ex)
            {
                NLogManager._instance.LogMsg(NLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        public void RunService()
        {
            this.OnStart(null);
        }
    }
}
