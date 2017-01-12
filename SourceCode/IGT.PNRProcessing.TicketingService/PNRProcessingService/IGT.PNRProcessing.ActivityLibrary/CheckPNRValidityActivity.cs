using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using System.Xml;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;
using System.Text.RegularExpressions;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class CheckPNRValidityActivity : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Session { get; set; }
        public InArgument<string> SupportedFOP { get; set; }
        public InArgument<string> PreFormatedRemark { get; set; }
        public InArgument<string> AllowedFareType { get; set; }
        public InArgument<XmlElement> PNRXml { get; set; }


        public OutArgument<string> TraceID { get; set; }
        public OutArgument<string> Recloc { get; set; }
        public OutArgument<bool> IsMCTError { get; set; }
        public OutArgument<bool> IsSupportedFOP { get; set; }
        public OutArgument<bool> IsValidFareType { get; set; }
        public OutArgument<bool> IsValidPreFormatedRemark { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strSession = context.GetValue(this.Session);
            XmlElement xmlPNR = context.GetValue(this.PNRXml);
            string strSupportedFOP = context.GetValue(this.SupportedFOP);
            string strPreFormatedRemark = context.GetValue(this.PreFormatedRemark);
            string strAllowedFareType = context.GetValue(this.AllowedFareType);
            
            QueueProcessing objQProcessing = new QueueProcessing();
            FareProcessing objFareProcessing = new FareProcessing();
            PNRProcessingAction objProcessTrace = new PNRProcessingAction();

            string strRecloc = objQProcessing.GetReclocFromPNRXml(xmlPNR);
            List<string> lstGenRemarks = objQProcessing.GetGeneralRemarks(xmlPNR);

            string strMCTError = objFareProcessing.CheckMCTError(strSession, objHAP);
            bool isMCTError = !string.IsNullOrEmpty(strMCTError);
            bool isSupportedFOP = objFareProcessing.IsFOPTypeSupported(xmlPNR, strSupportedFOP);
            bool isValidFareType = objFareProcessing.GetFareType(objHAP, strRecloc, strSession).ToString().ToUpper() == strAllowedFareType.Trim().ToUpper();
            bool isValidPreFormatedRemark = string.IsNullOrEmpty(strPreFormatedRemark)
                                            || lstGenRemarks.Any(x => Regex.IsMatch(x, ("^" + strPreFormatedRemark.Trim().Replace("#", "[A-Z0-9]{1}").Replace("*", "[A-Z0-9]*").Replace(" ", "") + "$"), RegexOptions.IgnoreCase));


            string strTraceID = string.Empty;
            if (!string.IsNullOrEmpty(strRecloc))
                strTraceID = objProcessTrace.SavePNRProcessTrace(objHAP.PCC, strRecloc);

            context.SetValue(Recloc, strRecloc);
            context.SetValue(TraceID, strTraceID);
            context.SetValue(IsMCTError, isMCTError);
            context.SetValue(IsSupportedFOP, isSupportedFOP);
            context.SetValue(IsValidFareType, isValidFareType);
            context.SetValue(IsValidPreFormatedRemark, isValidPreFormatedRemark);
        }
    }
}
