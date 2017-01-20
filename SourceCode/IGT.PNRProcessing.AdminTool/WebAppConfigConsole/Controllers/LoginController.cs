using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppConfigConsole.Models;

namespace WebAppConfigConsole.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (!string.IsNullOrEmpty(login.Name) && login.Name.ToUpper() == "INTERGLOBE" && login.Password == "IGT")
            {
                return RedirectToAction("Index", "PCCConfig");
            }
            return RedirectToAction("Index");
        }
    }
}