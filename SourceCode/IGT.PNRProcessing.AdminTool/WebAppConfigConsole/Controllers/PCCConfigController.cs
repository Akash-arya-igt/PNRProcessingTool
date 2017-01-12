using IGT.PNRProcessing.BusinessEntities;
using IGT.PNRProcessing.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigConsole.Controllers
{
    public class PCCConfigController : Controller
    {
        WorkflowSettingDAL objWFSettingDAL = new WorkflowSettingDAL();
        // GET: PCCConfig
        public ActionResult Index()
        {
            List<GetHAPDetail> lstPCCs = objWFSettingDAL.GetHAPDetailList();
            return View(lstPCCs);
        }

        // GET: PCCConfig/Details/5
        public ActionResult Details(int id)
        {
            GetHAPDetail objHAP = objWFSettingDAL.GetHAPDetail(id);
            return View(objHAP);
        }

        // GET: PCCConfig/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PCCConfig/Create
        [HttpPost]
        public ActionResult Create(GetHAPDetail collection)
        {
            try
            {
                objWFSettingDAL.SaveHAPDetail(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PCCConfig/Edit/5
        public ActionResult Edit(int id)
        {
            GetHAPDetail objHAP = objWFSettingDAL.GetHAPDetail(id);
            return View(objHAP);
        }

        // POST: PCCConfig/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GetHAPDetail collection)
        {
            try
            {
                objWFSettingDAL.UpdateHAPDetail(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PCCConfig/Delete/5
        public ActionResult Delete(int id)
        {
            GetHAPDetail objHAP = objWFSettingDAL.GetHAPDetail(id);
            return View(objHAP);
        }

        // POST: PCCConfig/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
