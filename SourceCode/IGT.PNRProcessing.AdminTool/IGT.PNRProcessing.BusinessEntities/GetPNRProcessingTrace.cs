using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessing.BusinessEntities
{
    public enum PNRProcessingStatus
    {
        Recorded,
        InProgress,
        Completed
    }
    public class GetPNRProcessingTrace
    {
        public string PNRProcessingTraceId { get; set; }

        [DisplayName("PNR")]
        public string Recloc { get; set; }
        public string PCC { get; set; }
        public string Status { get; set; }

        [DisplayName("Process Complete?")]
        public bool IsProcessComplete { get; set; }
        public string Error { get; set; }

        [DisplayName("PNR Capture Date")]
        public DateTime PNRCaptureDate { get; set; }

        [DisplayName("Last Modified")]
        public DateTime LastModified { get; set; }

        [DisplayName("No of Fares in PNR")]
        public int NoOfFares { get; set; }

        [DisplayName("No of Ticketed Fares")]
        public int NoOfTicketedFares { get; set; }
    }
}
