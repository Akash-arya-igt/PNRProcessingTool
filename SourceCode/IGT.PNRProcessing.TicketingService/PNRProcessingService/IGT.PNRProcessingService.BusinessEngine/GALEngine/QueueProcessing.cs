using System;
using System.Xml;
using IGT.PNRProcessingService.GWSConnection;
using IGT.PNRProcessingService.BusinessEntities;
using IGT.PNRProcessingService.BusinessEngine.CommonUtil;
using System.Collections.Generic;

namespace IGT.PNRProcessingService.BusinessEngine.GALEngine
{
    public class QueueProcessing
    {
        private string _queueProcessRequest = "QueueProcessing.xml";
        private string _moveToQRequest = "PNRMoveToQueue.xml";


        public int QueueCount(GetHAPDetail _pHAP, int _pQNum)
        {
            int intQKnt = 0;
            bool responseFound = false;
            string strQAction = "QCT";
            XmlDocument reqTemplate = XMLUtil.ReadTemplate(_queueProcessRequest);
            reqTemplate.SetNodeTextIfExist("//Action", strQAction);
            reqTemplate.SetNodeTextIfExist("//QNum", _pQNum.ToString());

            GWSConn objGwsConn = new GWSConn(_pHAP);
            XmlElement response = objGwsConn.SubmitXml(reqTemplate.DocumentElement);

            string xPath = "//QueueCount/HeaderCount/TotPNRBFCnt";
            XmlNode nodePnrCnt = response.SelectSingleNode(xPath);
            if (nodePnrCnt != null)
            {
                responseFound = int.TryParse(nodePnrCnt.InnerText, out intQKnt);
            }

            return intQKnt;
        }

        public int QueueCount(GetHAPDetail _pHAP, int _pQNum, string _pXmlTemplatePath)
        {
            int intQKnt = 0;
            bool responseFound = false;
            string strQAction = "QCT";
            XmlDocument reqTemplate = XMLUtil.ReadTemplate(_pXmlTemplatePath, _queueProcessRequest);
            reqTemplate.SetNodeTextIfExist("//Action", strQAction);
            reqTemplate.SetNodeTextIfExist("//QNum", _pQNum.ToString());

            GWSConn objGwsConn = new GWSConn(_pHAP);
            XmlElement response = objGwsConn.SubmitXml(reqTemplate.DocumentElement);

            string xPath = "//QueueCount/HeaderCount/TotPNRBFCnt";
            XmlNode nodePnrCnt = response.SelectSingleNode(xPath);
            if (nodePnrCnt != null)
            {
                responseFound = int.TryParse(nodePnrCnt.InnerText, out intQKnt);
            }

            return intQKnt;
        }

        public XmlElement ReadQueue(GetHAPDetail _pHAP, int _pQNum, string _pSession)
        {
            string strQAction = "Q";
            XmlDocument reqTemplate = XMLUtil.ReadTemplate(_queueProcessRequest);
            reqTemplate.SetNodeTextIfExist("//Action", strQAction);
            reqTemplate.SetNodeTextIfExist("//QNum", _pQNum.ToString());

            GWSConn objGwsConn = new GWSConn(_pHAP);
            XmlElement response = objGwsConn.SubmitXmlOnSession(_pSession, reqTemplate.DocumentElement);

            return response;
        }

        public string GetReclocFromPNRXml(XmlElement PNRXml)
        {
            string strRecloc = string.Empty;

            if (PNRXml != null)
                strRecloc = PNRXml.GetStringChildNode("//GenPNRInfo/RecLoc");

            return strRecloc;
        }

        public List<string> GetGeneralRemarks(XmlElement pnr)
        {
            List<string> genRemarks = new List<string>();
            XmlNodeList nodes = pnr.SelectNodes("//GenRmkInfo");
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    string remark = XMLUtil.GetStringChildNode(nodes[i], "GenRmk").Trim();
                    genRemarks.Add(remark);
                }
            }

            nodes = null;

            return genRemarks;
        }

        public string CreateSession(GetHAPDetail _pHAP)
        {
            GWSConn objGwsConn = new GWSConn(_pHAP);
            return objGwsConn.CreateSession();
        }

        public void CloseSession(GetHAPDetail _pHAP, string _pSession)
        {
            GWSConn objGwsConn = new GWSConn(_pHAP);
            objGwsConn.EndSession(_pSession);
        }

        public XmlElement MoveTOQueue(GetHAPDetail _pSourceHAP, int _pQNum, string _pRemark, string _pSession)
        {
            XmlDocument reqTemplate = XMLUtil.ReadTemplate(_moveToQRequest);
            reqTemplate.SetNodeTextIfExist("//QNum", _pQNum.ToString());

            string xPath = "//Item[DataBlkInd='G']";
            XmlNode newNode = reqTemplate.SelectSingleNode(xPath);
            _pRemark = _pRemark.Replace(";", Environment.NewLine);
            string[] remarkList = _pRemark.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < remarkList.Length; i++)
            {
                string remark = remarkList[i];
                if (String.IsNullOrEmpty(remark))
                    continue;
                if (i != 0)
                {
                    newNode = newNode.CloneSiblingAfterExistingNode();
                }
                XmlNode rmkNode = newNode.SelectSingleNode(".//Rmk");
                remark = remark.Length > 85 ? remark.Substring(0, 85) : remark;
                rmkNode.InnerText = remark;
            }

            GWSConn objGwsConn = new GWSConn(_pSourceHAP);
            XmlElement response = objGwsConn.SubmitXmlOnSession(_pSession, reqTemplate.DocumentElement);


            string strErrorText = response.GetStringChildNode("//EndTransaction/EndTransactMessage/Text");
            string strErrorCode = response.GetStringChildNode("//EndTransaction/ErrorCode");

            if (!string.IsNullOrEmpty(strErrorText) || !string.IsNullOrEmpty(strErrorCode))
            {
                reqTemplate.RemoveChildIfExist("//RcvdFrom");
                reqTemplate.RemoveChildIfExist("//PNRBFSecondaryBldChgMods");
                response = objGwsConn.SubmitXmlOnSession(_pSession, reqTemplate.DocumentElement);
            }

            return response;
        }
    }
}
