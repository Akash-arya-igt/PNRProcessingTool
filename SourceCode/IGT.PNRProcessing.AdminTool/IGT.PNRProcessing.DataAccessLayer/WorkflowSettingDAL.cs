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
                lstHAPs = (from lst in dbctx.tblPCCConfigurations
                           select new GetHAPDetail
                           {
                               GDS = lst.GDS,
                               PCCID = lst.PCCID,
                               DisplayPCCName = lst.PCC + " - " + lst.HAP,
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
                               DisplayPCCName = lst.PCC + " - " + lst.HAP,
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
                                             IsCashFOP = lst.AllowedFOP.Contains(((int)FOPType.Cash).ToString()),
                                             IsCCFOP = lst.AllowedFOP.Contains(((int)FOPType.CreditCard).ToString()),
                                             IsPublished = lst.AllowedFareType.Contains(FareType.PUBLISHED.ToString()),
                                             IsPrivate = lst.AllowedFareType.Contains(FareType.PUBLISHED.ToString()),
                                             IsCheckAVL = lst.IsAVLCheck,
                                             IsCheckMCT = lst.IsMCTCheck,
                                             SuccessQ = lst.SuccessQNo,
                                             SuccessMsg = lst.SuccessMsg,
                                             FailQ = lst.FailQNo,
                                             FailMsg = lst.FailMsg,
                                             TargetQNo = lst.TargetQNo,
                                             ScanFrequency = lst.ScanFrequency                                             
                                         }).ToList();

                    foreach (var obj in lstTktSetting)
                    {
                        obj.PCCName = objPCC.PCC + " - " + objPCC.Profile;
                    }

                    if (lstTktSetting != null && lstTktSetting.Count > 0)
                        lstSettings.AddRange(lstTktSetting);
                }
            }

            return lstSettings;
        }

        public GetTicketingFlowSettings GetTFSDetail(int _pFlowID)
        {
            GetTicketingFlowSettings objTFSs = new GetTicketingFlowSettings();
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                objTFSs = (from lst in dbctx.tblTicketingFlowConfigurations
                           where lst.TicketingflowId == _pFlowID
                           select new GetTicketingFlowSettings
                           {
                               PCCID = lst.PCCID,
                               FlowID = lst.TicketingflowId,
                               FlowName = lst.TicketingflowName,
                               PreFormatedRemark = lst.PreFormatedRemark,
                               CommissionPct = lst.CommissionPct != null ? lst.CommissionPct.Value:-1,
                               CommissionRemarkFormat = lst.CommissionRemarkFormat,
                               AllowedFOPId = lst.AllowedFOP,
                               AllowedFareType = lst.AllowedFareType,
                               IsCashFOP = lst.AllowedFOP.Contains(((int)FOPType.Cash).ToString()),
                               IsCCFOP = lst.AllowedFOP.Contains(((int)FOPType.CreditCard).ToString()),
                               IsPublished = lst.AllowedFareType.Contains(FareType.PUBLISHED.ToString()),
                               IsPrivate = lst.AllowedFareType.Contains(FareType.PUBLISHED.ToString()),
                               IsCheckAVL = lst.IsAVLCheck,
                               IsCheckMCT = lst.IsMCTCheck,
                               SuccessQ = lst.SuccessQNo,
                               SuccessMsg = lst.SuccessMsg,
                               FailQ = lst.FailQNo,
                               FailMsg = lst.FailMsg,
                               TargetQNo = lst.TargetQNo,
                               ScanFrequency = lst.ScanFrequency
                           }).FirstOrDefault();

                var objPCC = (from lst in dbctx.tblPCCConfigurations
                              where lst.PCCID == objTFSs.PCCID
                              select lst).FirstOrDefault();

                objTFSs.PCCName = objPCC.PCC + " - " + objPCC.HAP;
            }

            return objTFSs;
        }

        public void SaveTFSDetail(GetTicketingFlowSettings _pTFSDetail)
        {
            string strAllowedFOP = (_pTFSDetail.IsCashFOP ? (int)FOPType.Cash + "," : string.Empty) +
                                   (_pTFSDetail.IsCCFOP ? ((int)FOPType.CreditCard).ToString() : string.Empty);

            string strFareType = (_pTFSDetail.IsPublished ? FareType.PUBLISHED.ToString() + "," : string.Empty) +
                                 (_pTFSDetail.IsPrivate ? FareType.PRIVATE.ToString() : string.Empty);

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                tblTicketingFlowConfiguration objFlowConfig = new tblTicketingFlowConfiguration()
                {
                    PCCID = _pTFSDetail.PCCID,
                    PreFormatedRemark = _pTFSDetail.PreFormatedRemark,
                    CommissionRemarkFormat = _pTFSDetail.CommissionRemarkFormat,
                    AllowedFareType = strFareType,
                    AllowedFOP = strAllowedFOP,
                    CommissionPct = _pTFSDetail.CommissionPct,
                    FailQNo = _pTFSDetail.FailQ,
                    IsAVLCheck = _pTFSDetail.IsCheckAVL,
                    IsMCTCheck = _pTFSDetail.IsCheckMCT,
                    SuccessQNo = _pTFSDetail.SuccessQ,
                    TicketingflowName = _pTFSDetail.FlowName,
                    SuccessMsg = _pTFSDetail.SuccessMsg,
                    FailMsg = _pTFSDetail.FailMsg,
                    TargetQNo = _pTFSDetail.TargetQNo,
                    ScanFrequency = _pTFSDetail.ScanFrequency

                };

                dbctx.tblTicketingFlowConfigurations.Add(objFlowConfig);
                dbctx.SaveChanges();
            }
        }

        public void UpdateTFSDetail(GetTicketingFlowSettings _pTFSDetail)
        {
            string strAllowedFOP = (_pTFSDetail.IsCashFOP ? (int)FOPType.Cash + "," : string.Empty) +
                                   (_pTFSDetail.IsCCFOP ? ((int)FOPType.CreditCard).ToString() : string.Empty);

            string strFareType = (_pTFSDetail.IsPublished ? FareType.PUBLISHED.ToString() + "," : string.Empty) +
                                 (_pTFSDetail.IsPrivate ? FareType.PRIVATE.ToString() : string.Empty);

            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                tblTicketingFlowConfiguration objTFC = new tblTicketingFlowConfiguration()
                {
                    TicketingflowId = _pTFSDetail.FlowID,
                    PreFormatedRemark = _pTFSDetail.PreFormatedRemark,
                    CommissionRemarkFormat = _pTFSDetail.CommissionRemarkFormat,
                    AllowedFareType = strFareType,
                    AllowedFOP = strAllowedFOP,
                    CommissionPct = _pTFSDetail.CommissionPct,
                    FailQNo = _pTFSDetail.FailQ,
                    IsAVLCheck = _pTFSDetail.IsCheckAVL,
                    IsMCTCheck = _pTFSDetail.IsCheckMCT,
                    SuccessQNo = _pTFSDetail.SuccessQ,
                    TicketingflowName = _pTFSDetail.FlowName,
                    SuccessMsg = _pTFSDetail.SuccessMsg,
                    FailMsg = _pTFSDetail.FailMsg,
                    TargetQNo = _pTFSDetail.TargetQNo,
                    ScanFrequency = _pTFSDetail.ScanFrequency
                };

                dbctx.Entry(objTFC).State = EntityState.Modified;
                dbctx.SaveChanges();
            }
        }

        public void DeleteTFSDetail(GetTicketingFlowSettings _pTFSDetail)
        {
            using (PNRProcessingDBEntities dbctx = new PNRProcessingDBEntities())
            {
                tblTicketingFlowConfiguration objTFS = new tblTicketingFlowConfiguration()
                {
                };

                dbctx.Entry(objTFS).State = EntityState.Deleted;
                dbctx.SaveChanges();
            }
        }
    }


}
