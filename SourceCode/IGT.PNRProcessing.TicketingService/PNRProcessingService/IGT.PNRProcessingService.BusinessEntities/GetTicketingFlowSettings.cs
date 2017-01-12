using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.BusinessEntities
{
    public class GetTicketingFlowSettings
    {
        public GetHAPDetail HAPDetail { get; set; }
        public int FlowID { get; set; }
        public string FlowName { get; set; }
        public string PreFormatedRemark { get; set; }
        public float CommissionPct { get; set; }
        public string CommissionRemarkFormat { get; set; }
        public string AllowedFOPId { get; set; }
        public string AllowedFareType { get; set; }
        public bool IsCheckAVL { get; set; }
        public bool IsCheckMCT { get; set; }
        public int SuccessQ { get; set; }
        public string SuccessMsg { get; set; }
        public int FailQ { get; set; }
        public string FailMsg { get; set; }
        public int TargetQNo { get; set; }
        public int ScanFrequency { get; set; }
    }
}
