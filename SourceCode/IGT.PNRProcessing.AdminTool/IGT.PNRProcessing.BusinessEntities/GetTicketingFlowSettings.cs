using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessing.BusinessEntities
{
    public class GetTicketingFlowSettings
    {
        [DisplayName("PCC")]
        public string PCCName { get; set; }
        public int PCCID { get; set; }
        public int FlowID { get; set; }
        [DisplayName("Flow")]
        public string FlowName { get; set; }

        [DisplayName("Pre Formatted Remark Check")]
        public string PreFormatedRemark { get; set; }

        [DisplayName("Commission % to be added")]        
        public int CommissionPct { get; set; }

        [DisplayName("Commision Remark to be added")]
        public string CommissionRemarkFormat { get; set; }

        [DisplayName("Allowed FOPs")]
        public string AllowedFOPId { get; set; }

        [DisplayName("Allowe Fare Type")]
        public string AllowedFareType { get; set; }
        public bool IsCashFOP { get; set; }
        public bool IsCCFOP { get; set; }
        public bool IsPublished { get; set; }
        public bool IsPrivate { get; set; }

        [DisplayName("Check for AVL")]
        public bool IsCheckAVL { get; set; }

        [DisplayName("Check for MCT")]
        public bool IsCheckMCT { get; set; }

        [DisplayName("Success Queue No")]
        public int SuccessQ { get; set; }

        [DisplayName("Remark to be inserted in case of Success")]
        public string SuccessMsg { get; set; }

        [DisplayName("Failure Queue No")]
        public int FailQ { get; set; }

        [DisplayName("Remark to be inserted in case of Failure")]
        public string FailMsg { get; set; }

        [DisplayName("Source Queue")]
        public int TargetQNo { get; set; }

        [DisplayName("Scan Frequency")]
        public int ScanFrequency { get; set; }
    }

    public enum FOPType
    {
        Cash = 1,
        CreditCard = 6
    }

    public enum FareType
    {
        PUBLISHED,
        PRIVATE
    }
}
