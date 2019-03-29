﻿using System;
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

        /// <summary>
        /// Retrieves photo to display to view.
        /// </summary>
        /// <param name="id">ID of a photo to display.</param>
        /// <returns>Photo to display to view.</returns>
        // GET: Resources
        public FileResult Photo(int? id)
        {
            SUPImage p = db.SUPImages.Find(id);
            //Stream stream = new MemoryStream(p.Input);
            //Image file = Image.FromStream(stream);
            return File(p.Input, "image");
        }
    }
}