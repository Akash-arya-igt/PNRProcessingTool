using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using System.Xml;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class EndTransactNRetrieveActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Session { get; set; }

        public OutArgument<XmlElement> PNRXml { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strSession = context.GetValue(this.Session);

            FareProcessing objFareProcess = new FareProcessing();
            XmlElement xmlPNR = objFareProcess.EndTransactNRetrieve(objHAP, strSession);

            context.SetValue(PNRXml, xmlPNR);
        }
    }
}
