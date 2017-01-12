using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEngine.DALEngine;
using IGT.PNRProcessingService.BusinessEntities;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class UpdateTicketingDetailInDB : CodeActivity
    {
        public InArgument<PNRProcessingStatus> ProcessingStatus { get; set; }
        public InArgument<string> TraceID { get; set; }
        public InArgument<string> ErrorOccured { get; set; }
        public InArgument<bool> IsCompleted { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            bool isCompleted = context.GetValue(this.IsCompleted);
            string strTrackId = context.GetValue(this.TraceID);
            string strError = context.GetValue(this.ErrorOccured);
            PNRProcessingStatus enmProcessingStatus = context.GetValue(this.ProcessingStatus);

            PNRProcessingAction objProcessTrace = new PNRProcessingAction();
            objProcessTrace.UpdateProcessingStatus(strTrackId, enmProcessingStatus, strError, isCompleted);
        }
    }
}
