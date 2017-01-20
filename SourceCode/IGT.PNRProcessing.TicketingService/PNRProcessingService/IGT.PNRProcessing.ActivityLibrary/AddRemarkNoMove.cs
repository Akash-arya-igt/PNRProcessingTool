using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.GALEngine;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class AddRemarkNoMove : CodeActivity
    {
        public InArgument<GetHAPDetail> HAPSetting { get; set; }
        public InArgument<string> Remark { get; set; }
        public InArgument<string> Session { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            GetHAPDetail objHAP = context.GetValue(this.HAPSetting);
            string strRemark = context.GetValue(this.Remark);
            string strSession = context.GetValue(this.Session);

            FareProcessing objFareProcessing = new FareProcessing();
            objFareProcessing.AddRemarkNoMove(objHAP, strRemark, strSession);
        }
    }
}
