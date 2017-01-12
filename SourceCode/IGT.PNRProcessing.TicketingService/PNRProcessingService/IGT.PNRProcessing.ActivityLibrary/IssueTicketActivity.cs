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
        public InArgument<string> Session { get; set; }

        public OutArgument<string> Error { get; set; }
        public OutArgument<bool> IsErrorOccured { get; set; }
        public OutArgument<XmlElement> PNRXml { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strRecloc = context.GetValue(this.Recloc);
            string strTraceID = context.GetValue(this.TraceID);
            string strSuccessRemark = context.GetValue(this.SuccessRemark);
            string strSession = context.GetValue(this.Session);

            try
            {
                FareProcessing objFareProcessing = new FareProcessing();
                XmlElement xmlNextPNR = objFareProcessing.IssueTicket(objHAP, strRecloc, strTraceID, strSuccessRemark, strSession);

                context.SetValue(Error, string.Empty);
                context.SetValue(IsErrorOccured, false);
                //context.SetValue(PNRXml, xmlNextPNR);
            }
            catch (Exception ex)
            {
                context.SetValue(Error, ex.Message);
                context.SetValue(IsErrorOccured, true);
            }
        }
    }
}
