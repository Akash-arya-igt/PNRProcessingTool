using IGT.PNRProcessing.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessing.DataAccessLayer
{
    public class WorkflowSettingDAL
    {
        public List<GetHAPDetail> GetHAPDetailList()
        {
            List<GetHAPDetail> lstHAPs = new List<GetHAPDetail>();
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                lstHAPs= (from lst in dbctx.tblPCCConfigurations
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
            }

            return lstHAPs;
        }

        public GetHAPDetail GetHAPDetail(int _pPCCId)
        {
            GetHAPDetail objHAPs = new GetHAPDetail();
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                objHAPs = (from lst in dbctx.tblPCCConfigurations
                           where lst.PCCID == _pPCCId
                           select new GetHAPDetail
                           {
                               GDS = lst.GDS,
                               PCCID = lst.PCCID,
                               PCC = lst.PCC,
                               Password = lst.Password,
                               UserID = lst.UserId,
                               Profile = lst.HAP,
                               GWSConnURL = lst.URL
                           }).FirstOrDefault();
            }

            return objHAPs;
        }

        public void SaveHAPDetail(GetHAPDetail _pHAPDetail)
        {
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                tblPCCConfiguration objPCC = new tblPCCConfiguration()
                {
                    GDS = _pHAPDetail.GDS,
                    PCC = _pHAPDetail.PCC,
                    Password = _pHAPDetail.Password,
                    UserId = _pHAPDetail.UserID,
                    HAP = _pHAPDetail.Profile,
                    URL = _pHAPDetail.GWSConnURL
                };
                dbctx.tblPCCConfigurations.Add(objPCC);
                dbctx.SaveChanges();
            }
        }

        public void UpdateHAPDetail(GetHAPDetail _pHAPDetail)
        {
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                tblPCCConfiguration objPCC = new tblPCCConfiguration()
                {
                    PCCID = _pHAPDetail.PCCID,
                    GDS = _pHAPDetail.GDS,
                    PCC = _pHAPDetail.PCC,
                    Password = _pHAPDetail.Password,
                    UserId = _pHAPDetail.UserID,
                    HAP = _pHAPDetail.Profile,
                    URL = _pHAPDetail.GWSConnURL
                };

                dbctx.Entry(objPCC).State = EntityState.Modified;
                dbctx.SaveChanges();
            }
        }

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
                                             PCCID = objPCC.PCCID,
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

                    if (lstTktSetting != null && lstTktSetting.Count > 0)
                        lstSettings.AddRange(lstTktSetting);
                }
            }

            return lstSettings;
        }
    }
}
