using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace URent.Controllers
{
    public class ResourcesController : Controller
    {
        // GET: Resources
        public ActionResult Index(int? id)
        {
            
            return View();
        }
    }
}