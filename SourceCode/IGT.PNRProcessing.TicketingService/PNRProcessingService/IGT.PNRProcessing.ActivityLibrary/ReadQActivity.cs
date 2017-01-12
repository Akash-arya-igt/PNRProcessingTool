using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using System.Xml;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;
using System.Text.RegularExpressions;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class ReadQActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Session { get; set; }
        public InArgument<int> QueueNumber { get; set; }

        public OutArgument<XmlElement> PNRXml { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strSession = context.GetValue(this.Session);
            int intQueueNumber = context.GetValue(this.QueueNumber);

            QueueProcessing objQProcessing = new QueueProcessing();

            XmlElement xmlPNR = objQProcessing.ReadQueue(objHAP, intQueueNumber, strSession);

            context.SetValue(PNRXml, xmlPNR);
        }
    }
}
