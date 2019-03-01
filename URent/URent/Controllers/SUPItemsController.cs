using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using URent.Models;

namespace URent.Controllers
{
    public class SUPItemsController : Controller
    {
        private SUPContext db = new SUPContext();

        [Authorize]
        private string getIdentityID()
        {
            return User.Identity.GetUserId();
        }

        [Authorize]
        private int getSUPUserID()
        {
            string id = getIdentityID();
            SUPUser supUser = db.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }

        [Authorize]
        private bool checkUser(int ?listingid)
        {
            int userid = getSUPUserID();
            int listingoid = db.SUPItems.Find(listingid).OwnerID;
            if(userid == listingoid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Authorize]
        public ActionResult Error()
        {
            return View();
        }

        // GET: SUPItems
        //public ActionResult Index()
        //{
        //    var sUPItems = db.SUPItems.Include(s => s.SUPUser);
        //    return View(sUPItems.ToList());
        //}

        // GET: SUPItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItem sUPItem = db.SUPItems.Find(id);
            if (sUPItem == null)
            {
                return HttpNotFound();
            }
            return View(sUPItem);
        }

        // GET: SUPItems/Create
        [Authorize]
        public ActionResult Create()
        {
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, supuser.Id, "FirstName");
            return View();
        }

        // POST: SUPItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemName,Description,IsAvailable,DailyPrice")] SUPItem sUPItem)
        {
            if (ModelState.IsValid)
            {
                sUPItem.OwnerID = getSUPUserID();
                db.SUPItems.Add(sUPItem);
                db.SaveChanges();
                return RedirectToAction("GetUserItems");
            }
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
            return View(sUPItem);
        }

        // GET: SUPItems/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItem sUPItem = db.SUPItems.Find(id);
            if (sUPItem == null)
            {
                return HttpNotFound();
            }
            bool check = checkUser(id);
            if ( check == false )
            {
                return RedirectToAction("Error");
            }
            else
            {
                return View(sUPItem);
            }
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
        }

        // POST: SUPItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName,Description,TimeStamp,IsAvailable,DailyPrice")] SUPItem sUPItem)
        {
            if (ModelState.IsValid)
            {
                sUPItem.OwnerID = getSUPUserID();
                db.Entry(sUPItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetUserItems");
            }
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
            return View(sUPItem);
        }

        // GET: SUPItems/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItem sUPItem = db.SUPItems.Find(id);
            if (sUPItem == null)
            {
                return HttpNotFound();
            }
            bool check = checkUser(id);
            if (check == false)
            {
                return RedirectToAction("Error");
            }
            else
            {
                return View(sUPItem);
            }
        }

        // POST: SUPItems/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPItem sUPItem = db.SUPItems.Find(id);
            db.SUPItems.Remove(sUPItem);
            db.SaveChanges();
            return RedirectToAction("GetUserItems");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult GetUserItems()
        {
            int userId = getSUPUserID();
            var myItems = db.SUPItems.Where(u => u.OwnerID == userId);
            return View(myItems.ToList());
        }

        /* DropZone Method called from Form element in View */
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    // base instance of Image for saving information
                    SUPImage i = new SUPImage();
                    //assign file name to filename
                    i.Filename = file.FileName;
                    // read in InputStream into input
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        i.Input = reader.ReadBytes((int)file.InputStream.Length);
                    }
                    //save file to local db
                    // !!!!!!!!! NOTE: it is saving into SUPUserTables database
                    db.SUPImages.Add(i);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}
