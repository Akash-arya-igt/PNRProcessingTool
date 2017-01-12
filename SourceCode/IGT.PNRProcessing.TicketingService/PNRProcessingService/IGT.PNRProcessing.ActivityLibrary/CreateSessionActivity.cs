using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class CreateSessionActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }

        public OutArgument<string> Session { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);

            QueueProcessing objQProcessing = new QueueProcessing();
            string strSession = objQProcessing.CreateSession(objHAP);
            context.SetValue(Session, strSession);
        }
    }
}
