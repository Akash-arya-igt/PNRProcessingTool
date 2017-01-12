using IGT.PNRProcessing.BusinessEntities;
using IGT.PNRProcessing.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigConsole.Controllers
{
    public class TicketingFlowConfigController : Controller
    {
        WorkflowSettingDAL objWFSettingDAL = new WorkflowSettingDAL();
        // GET: TicketingFlowConfig
        public ActionResult Index()
        {
            List<GetTicketingFlowSettings> lstTFC = objWFSettingDAL.GetTicketFlowSettingList();
            return View(lstTFC);
        }

        // GET: TicketingFlowConfig/Details/5
        public ActionResult Details(int id)
        {
            GetTicketingFlowSettings objTFS = objWFSettingDAL.GetTFSDetail(id);
            return View(objTFS);
        }

        // GET: TicketingFlowConfig/Create
        public ActionResult Create()
        {
            List<GetHAPDetail> lstHAPs = objWFSettingDAL.GetHAPDetailList();
            ViewBag.PCCList = new SelectList(lstHAPs, "PCCID", "DisplayPCCName");
            return View();
        }

        // POST: TicketingFlowConfig/Create
        [HttpPost]
        public ActionResult Create(GetTicketingFlowSettings collection)
        {
            try
            {
                // TODO: Add insert logic here
                objWFSettingDAL.SaveTFSDetail(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketingFlowConfig/Edit/5
        public ActionResult Edit(int id)
        {
            GetTicketingFlowSettings objTFS = objWFSettingDAL.GetTFSDetail(id);
            return View(objTFS);
        }

        // POST: TicketingFlowConfig/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GetTicketingFlowSettings collection)
        {
            try
            {
                objWFSettingDAL.UpdateTFSDetail(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketingFlowConfig/Delete/5
        public ActionResult Delete(int id)
        {
            GetTicketingFlowSettings objTFS = objWFSettingDAL.GetTFSDetail(id);
            return View(objTFS);
        }

        // POST: TicketingFlowConfig/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, GetTicketingFlowSettings collection)
        {
            try
            {
                objWFSettingDAL.DeleteTFSDetail(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
