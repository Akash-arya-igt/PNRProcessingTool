using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.BusinessEntities
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
        public string Recloc { get; set; }
        public string PCC { get; set; }
        public string Status { get; set; }
        public bool IsProcessComplete { get; set; }
        public string Error { get; set; }
        public DateTime PNRCaptureDate { get; set; }
        public DateTime LastModified { get; set; }
        public int NoOfFares { get; set; }
        public int NoOfTicketedFares { get; set; }
    }
}
