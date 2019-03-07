using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using URent.Models;

namespace URent.Controllers
{
    public class ResourcesController : Controller
    {
        SUPContext db = new SUPContext();
        // GET: Resources
        public ActionResult Index(int? id)
        {
            SUPImage p = db.SUPImages.Find(id);
            if(id != null)
            {

            }
            return View();
        }
    }
}