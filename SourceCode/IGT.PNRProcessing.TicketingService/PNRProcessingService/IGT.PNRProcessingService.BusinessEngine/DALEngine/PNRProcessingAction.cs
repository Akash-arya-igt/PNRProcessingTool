using IGT.PNRProcessing.DataAccessLayer;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.BusinessEngine.DALEngine
{
    public class PNRProcessingAction
    {
        PNRProcessingDAL _objPNRProcessDAL = new PNRProcessingDAL();

        public string SavePNRProcessTrace(string _pPCC, string _pRecloc)
        {
            GetPNRProcessingTrace objPNRTrace = new GetPNRProcessingTrace();
            objPNRTrace.PCC = _pPCC;
            objPNRTrace.Recloc = _pRecloc;
            objPNRTrace.NoOfFares = 0;
            objPNRTrace.NoOfTicketedFares = 0;
            objPNRTrace.Status = PNRProcessingStatus.Recorded.ToString();
            return _objPNRProcessDAL.SavePNRProcessingRequest(objPNRTrace);
        }

        public List<GetPNRProcessingTrace> GetRecordedPNRList()
        {
            return _objPNRProcessDAL.GetRecordedPNRList();
        }

        public List<GetPNRProcessingTrace> GetPNRRecordList(int _pRecordCount)
        {
            return _objPNRProcessDAL.GetPNRRecordList(_pRecordCount);
        }

        public void UpdateProcessingStatus(string _pTraceID, PNRProcessingStatus _pStatus, string _pError, int _pNoOfFares, int _pNoOfTicketIssued, bool _pIsProcessCompleted)
        {
            _objPNRProcessDAL.UpdateProcessingStatus(_pTraceID, _pStatus, _pError, _pNoOfFares, _pNoOfTicketIssued, _pIsProcessCompleted);
        }

        public void UpdateProcessingStatus(string _pTraceID, PNRProcessingStatus _pStatus, string _pError, bool _pIsProcessCompleted)
        {
            _objPNRProcessDAL.UpdateProcessingStatus(_pTraceID, _pStatus, _pError, _pIsProcessCompleted);
        }

        public string GetProcessingStatus(string _pTraceID)
        {
            return _objPNRProcessDAL.GetProcessingStatus(_pTraceID);
        }
    }
}
