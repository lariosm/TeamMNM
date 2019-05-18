using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace URent.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error_RentYourOwnItem()
        {
            return View();
        }

        public ActionResult Error_EditViewAnotherAccount()
        {
            return View();
        }
    }
}