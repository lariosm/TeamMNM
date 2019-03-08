using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public FileResult Photo(int? id)
        {
            Console.WriteLine("in resources controller");
            SUPImage p = db.SUPImages.Find(id);
            //Stream stream = new MemoryStream(p.Input);
            //Image file = Image.FromStream(stream);
            return File(p.Input, "image");
        }
    }
}