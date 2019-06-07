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

        public ActionResult Error400()
        {
            HttpContext.Response.StatusCode = 400;
            HttpContext.Response.TrySkipIisCustomErrors = true;

            return View();
        }

        public ActionResult Error404()
        {
            HttpContext.Response.StatusCode = 404;
            HttpContext.Response.TrySkipIisCustomErrors = true;

            return View();
        }

        public ActionResult Error500()
        {
            HttpContext.Response.StatusCode = 500;
            HttpContext.Response.TrySkipIisCustomErrors = true;

            return View();
        }
    }
}