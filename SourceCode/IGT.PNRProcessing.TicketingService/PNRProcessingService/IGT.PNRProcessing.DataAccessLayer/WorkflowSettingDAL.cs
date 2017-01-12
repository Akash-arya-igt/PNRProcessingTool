using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessing.DataAccessLayer
{
    public class WorkflowSettingDAL
    {
        public List<GetTicketingFlowSettings> GetTicketFlowSettingList()
        {
            List<GetTicketingFlowSettings> lstSettings = new List<GetTicketingFlowSettings>();

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                var lstPCCs = (from lst in dbctx.tblPCCConfigurations
                               select new GetHAPDetail
                               {
                                   GDS = lst.GDS,
                                   PCCID = lst.PCCID,
                                   PCC = lst.PCC,
                                   Password = lst.Password,
                                   UserID = lst.UserId,
                                   Profile = lst.HAP,
                                   GWSConnURL = lst.URL
                               }).ToList();

                foreach (GetHAPDetail objPCC in lstPCCs)
                {
                    var lstTktSetting = (from lst in dbctx.tblTicketingFlowConfigurations
                                         where lst.PCCID == objPCC.PCCID
                                         select new GetTicketingFlowSettings
                                         {
                                             FlowID = lst.TicketingflowId,
                                             FlowName = lst.TicketingflowName,
                                             PreFormatedRemark = lst.PreFormatedRemark,
                                             CommissionPct = lst.CommissionPct != null ? lst.CommissionPct.Value : -1,
                                             CommissionRemarkFormat = lst.CommissionRemarkFormat,
                                             AllowedFareType = lst.AllowedFareType,
                                             AllowedFOPId = lst.AllowedFOP,
                                             IsCheckAVL = lst.IsAVLCheck,
                                             IsCheckMCT = lst.IsMCTCheck,
                                             SuccessQ = lst.SuccessQNo,
                                             SuccessMsg = lst.SuccessMsg,
                                             FailQ = lst.FailQNo,
                                             FailMsg = lst.FailMsg,
                                             TargetQNo = lst.TargetQNo,
                                             ScanFrequency = lst.ScanFrequency
                                         }).ToList();

                    foreach (var tktSetting in lstTktSetting)
                    {
                        tktSetting.HAPDetail = objPCC;
                    }

                    if (lstTktSetting != null && lstTktSetting.Count > 0)
                        lstSettings.AddRange(lstTktSetting);
                }
            }

            return lstSettings;
        }
    }
}
