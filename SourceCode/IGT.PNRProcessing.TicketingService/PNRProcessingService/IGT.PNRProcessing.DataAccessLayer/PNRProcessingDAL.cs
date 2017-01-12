using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessing.DataAccessLayer
{
    public class PNRProcessingDAL
    {
        public string SavePNRProcessingRequest(GetPNRProcessingTrace _pPNRProcessingDetail)
        {
            string strPNRProcessingID = Guid.NewGuid().ToString();

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                tblPNRProcessingTrace objPNRProcessingTrace = new tblPNRProcessingTrace();
                objPNRProcessingTrace.PNRProcessingTraceId = strPNRProcessingID;
                objPNRProcessingTrace.PCC = _pPNRProcessingDetail.PCC;
                objPNRProcessingTrace.Recloc = _pPNRProcessingDetail.Recloc;
                objPNRProcessingTrace.Status = _pPNRProcessingDetail.Status;
                objPNRProcessingTrace.NoOfFares = _pPNRProcessingDetail.NoOfFares;
                objPNRProcessingTrace.NoOfTicketedFares = _pPNRProcessingDetail.NoOfTicketedFares;
                objPNRProcessingTrace.IsProcessComplete = false;
                objPNRProcessingTrace.PNRCaptureDate = DateTime.Now;
                objPNRProcessingTrace.LastModified = DateTime.Now;

                dbctx.tblPNRProcessingTraces.Add(objPNRProcessingTrace);
                dbctx.SaveChanges();
            }

            return strPNRProcessingID;
        }

        public List<GetPNRProcessingTrace> GetRecordedPNRList()
        {
            List<GetPNRProcessingTrace> lstUnProcessedPNR = new List<GetPNRProcessingTrace>();

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                string strRecordedStatus = PNRProcessingStatus.Recorded.ToString();
                lstUnProcessedPNR = (from lst in dbctx.tblPNRProcessingTraces
                                     where lst.Status != null && lst.Status == strRecordedStatus
                                     select new GetPNRProcessingTrace
                                     {
                                         PNRProcessingTraceId = lst.PNRProcessingTraceId,
                                         PCC = lst.PCC,
                                         Recloc = lst.Recloc,
                                         Status = lst.Status,
                                         IsProcessComplete = lst.IsProcessComplete != null ? lst.IsProcessComplete.Value : false,
                                         Error = lst.Error,
                                         PNRCaptureDate = lst.PNRCaptureDate != null ? lst.PNRCaptureDate.Value : DateTime.MinValue,
                                         LastModified = lst.LastModified != null ? lst.LastModified.Value : DateTime.MinValue,
                                         NoOfFares = lst.NoOfFares,
                                         NoOfTicketedFares = lst.NoOfTicketedFares
                                     }).ToList();
            }

            return lstUnProcessedPNR;
        }

        public List<GetPNRProcessingTrace> GetPNRRecordList(int _pRecordCount)
        {
            List<GetPNRProcessingTrace> lstPNRRecord = new List<GetPNRProcessingTrace>();

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                lstPNRRecord = (from lst in dbctx.tblPNRProcessingTraces
                                orderby lst.LastModified descending
                                select new GetPNRProcessingTrace
                                {
                                    PNRProcessingTraceId = lst.PNRProcessingTraceId,
                                    PCC = lst.PCC,
                                    Recloc = lst.Recloc,
                                    Status = lst.Status,
                                    IsProcessComplete = lst.IsProcessComplete != null ? lst.IsProcessComplete.Value : false,
                                    Error = lst.Error,
                                    PNRCaptureDate = lst.PNRCaptureDate != null ? lst.PNRCaptureDate.Value : DateTime.MinValue,
                                    LastModified = lst.LastModified != null ? lst.LastModified.Value : DateTime.MinValue,
                                    NoOfFares = lst.NoOfFares,
                                    NoOfTicketedFares = lst.NoOfTicketedFares
                                }).Take(_pRecordCount).ToList();
            }

            return lstPNRRecord;
        }

        public void UpdateProcessingStatus(string _pTraceID, PNRProcessingStatus _pStatus, string _pError, int _pNoOfFares, int _pNoOfTicketIssued, bool _pIsProcessCompleted)
        {
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                var ProcessingTrace = (from lst in dbctx.tblPNRProcessingTraces
                                       where lst.PNRProcessingTraceId == _pTraceID
                                       select lst).FirstOrDefault();

                ProcessingTrace.Status = _pStatus.ToString();
                ProcessingTrace.Error = _pError;
                ProcessingTrace.NoOfFares = _pNoOfFares;
                ProcessingTrace.NoOfTicketedFares = _pNoOfTicketIssued;
                ProcessingTrace.IsProcessComplete = _pIsProcessCompleted;
                ProcessingTrace.LastModified = DateTime.Now;

                dbctx.Entry(ProcessingTrace).State = EntityState.Modified;
                dbctx.SaveChanges();
            }
        }

        public void UpdateProcessingStatus(string _pTraceID, PNRProcessingStatus _pStatus, string _pError, bool _pIsProcessCompleted)
        {
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                var ProcessingTrace = (from lst in dbctx.tblPNRProcessingTraces
                                       where lst.PNRProcessingTraceId == _pTraceID
                                       select lst).FirstOrDefault();

                ProcessingTrace.Status = _pStatus.ToString();
                ProcessingTrace.Error = _pError;
                ProcessingTrace.IsProcessComplete = _pIsProcessCompleted;
                ProcessingTrace.LastModified = DateTime.Now;

                dbctx.Entry(ProcessingTrace).State = EntityState.Modified;
                dbctx.SaveChanges();
            }
        }

        public void UpdateTicketingFares(string _pTraceID, int _pNoOfFares, int _pNoOfTicketIssued)
        {
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                var ProcessingTrace = (from lst in dbctx.tblPNRProcessingTraces
                                       where lst.PNRProcessingTraceId == _pTraceID
                                       select lst).FirstOrDefault();

                ProcessingTrace.NoOfFares = _pNoOfFares;
                ProcessingTrace.NoOfTicketedFares = _pNoOfTicketIssued;
                ProcessingTrace.LastModified = DateTime.Now;

                dbctx.Entry(ProcessingTrace).State = EntityState.Modified;
                dbctx.SaveChanges();
            }
        }

        public string GetProcessingStatus(string _pTraceID)
        {
            string strStatus = string.Empty;

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                strStatus = (from lst in dbctx.tblPNRProcessingTraces
                             where lst.PNRProcessingTraceId == _pTraceID
                             select lst.Status).FirstOrDefault().ToString();
            }

            return string.IsNullOrEmpty(strStatus) ? "Record not found" : strStatus;
        }
    }
}
