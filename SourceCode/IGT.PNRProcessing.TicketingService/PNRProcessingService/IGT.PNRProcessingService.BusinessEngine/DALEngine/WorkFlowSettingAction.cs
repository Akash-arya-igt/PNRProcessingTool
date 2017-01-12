using IGT.PNRProcessing.DataAccessLayer;
using IGT.PNRProcessingService.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.BusinessEngine.DALEngine
{
    public class WorkFlowSettingAction
    {
        WorkflowSettingDAL objWFSetting = new WorkflowSettingDAL();

        public List<GetTicketingFlowSettings> GetTicketFlowSettingList()
        {
            return objWFSetting.GetTicketFlowSettingList();
        }
    }
}
