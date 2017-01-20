using IGT.PNRProcessing.BusinessEntities;
using IGT.PNRProcessing.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigConsole.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            Response.AddHeader("Refresh", "10");
            PNRProcessingDAL objDAL = new PNRProcessingDAL();
            List<GetPNRProcessingTrace> lstTrace = objDAL.GetPNRRecordList(10);
            return View(lstTrace);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
