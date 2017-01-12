using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Xml;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class PNRMoveToQActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Session { get; set; }
        public InArgument<string> Remark { get; set; }
        public InArgument<int> ToQueueNumber { get; set; }

        public OutArgument<XmlElement> PNRXml { get; set; }
        public OutArgument<string> Recloc { get; set; }
        public OutArgument<string> TraceID { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strRemark = context.GetValue(this.Remark);
            string strSession = context.GetValue(this.Session);
            int intToQueueNumber = context.GetValue(this.ToQueueNumber);

            QueueProcessing objQProcessing = new QueueProcessing();
            PNRProcessingAction objProcessTrace = new PNRProcessingAction();
            XmlElement xmlPNR = objQProcessing.MoveTOQueue(objHAP, intToQueueNumber, strRemark, strSession);

            string strTraceID = string.Empty;
            string strRecloc = objQProcessing.GetReclocFromPNRXml(xmlPNR);
            if (!string.IsNullOrEmpty(strRecloc))
                strTraceID = objProcessTrace.SavePNRProcessTrace(objHAP.PCC, strRecloc);

            context.SetValue(PNRXml, xmlPNR);
            context.SetValue(Recloc, strRecloc);
            context.SetValue(TraceID, strTraceID);
        }
    }
}
