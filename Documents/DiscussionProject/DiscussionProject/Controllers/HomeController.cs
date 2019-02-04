using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiscussionProject.Models;

namespace DiscussionProject.Controllers
{
    public class HomeController : Controller
    {
        private SUPContext db = new SUPContext();


        public ActionResult Index()
        {
            var sUPDiscussions = db.SUPDiscussions.Include(s => s.SUPUser);
            return View(sUPDiscussions.ToList());
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