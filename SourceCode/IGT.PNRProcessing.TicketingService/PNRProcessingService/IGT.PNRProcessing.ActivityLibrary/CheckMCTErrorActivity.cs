using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class CheckMCTErrorActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> TraceID { get; set; }
        public InArgument<string> SessionID { get; set; }

        public OutArgument<bool> IsErrorOccured { get; set; }
        public OutArgument<string> Error { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strSessionID = context.GetValue(this.SessionID);
            string strTraceID = context.GetValue(this.TraceID);

            FareProcessing objFareProcessing = new FareProcessing();
            string strError = objFareProcessing.CheckMCTError(strSessionID, objHAP);

            if (!string.IsNullOrEmpty(strError))
            {
                context.SetValue(Error, strError);
                context.SetValue(IsErrorOccured, true);

                PNRProcessingAction objProcessTrace = new PNRProcessingAction();
                objProcessTrace.UpdateProcessingStatus(strTraceID, PNRProcessingStatus.Completed, strError, true);
            }
            else
            {
                context.SetValue(Error, string.Empty);
                context.SetValue(IsErrorOccured, false);
            }
        }
    }
}
