using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class QCountActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<int> QueueNumber { get; set; }

        public OutArgument<int> QCount { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            int intQueueNumber = context.GetValue(this.QueueNumber);

            QueueProcessing objQProcessing = new QueueProcessing();
            int intQKnt = objQProcessing.QueueCount(objHAP, intQueueNumber);

            context.SetValue(QCount, intQKnt);
        }
    }
}
