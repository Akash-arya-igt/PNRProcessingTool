using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;
using System.Xml;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class IssueTicketActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Recloc { get; set; }
        public InArgument<string> TraceID { get; set; }
        public InArgument<float> CommissionPct { get; set; }
        public InArgument<string> SuccessRemark { get; set; }

        public OutArgument<string> Error { get; set; }
        public OutArgument<bool> IsErrorOccured { get; set; }
        public OutArgument<string> TicketingSession { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            string strTicketingSession = string.Empty;
            string strRecloc = context.GetValue(this.Recloc);
            string strTraceID = context.GetValue(this.TraceID);
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strSuccessRemark = context.GetValue(this.SuccessRemark);

            try
            {
                FareProcessing objFareProcessing = new FareProcessing();
                objFareProcessing.IssueTicket(objHAP, strRecloc, strTraceID, strSuccessRemark, out strTicketingSession);

                context.SetValue(Error, string.Empty);
                context.SetValue(IsErrorOccured, false);
            }
            catch (Exception ex)
            {
                context.SetValue(Error, ex.Message);
                context.SetValue(IsErrorOccured, true);
            }

            context.SetValue(TicketingSession, strTicketingSession);
        }
    }
}
