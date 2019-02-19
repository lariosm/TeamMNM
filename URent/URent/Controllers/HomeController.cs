using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using URent.Models;

namespace URent.Controllers
{
    public class HomeController : Controller
    {
        private SUPContext db = new SUPContext();

        public ActionResult Index()
        {
            var sUPItems = db.SUPItems.Include(s => s.SUPUser);
            return View(sUPItems.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}