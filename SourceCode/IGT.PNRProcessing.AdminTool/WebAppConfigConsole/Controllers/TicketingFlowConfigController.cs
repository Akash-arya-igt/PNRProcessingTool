using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigConsole.Controllers
{
    public class TicketingFlowConfigController : Controller
    {
        // GET: TicketingFlowConfig
        public ActionResult Index()
        {
            return View();
        }

        // GET: TicketingFlowConfig/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketingFlowConfig/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketingFlowConfig/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        // POST: TicketingFlowConfig/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: TicketingFlowConfig/Delete/5
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
