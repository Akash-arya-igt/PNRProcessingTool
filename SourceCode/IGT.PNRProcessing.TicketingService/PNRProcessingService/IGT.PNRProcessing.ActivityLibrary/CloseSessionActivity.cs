using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class CloseSessionActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Session { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strSession = context.GetValue(this.Session);

            QueueProcessing objQProcessing = new QueueProcessing();
            objQProcessing.CloseSession(objHAP, strSession);
        }
    }
}
