using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using URent.Models;

namespace URent.Controllers
{
    public class SUPItemsController : Controller
    {
        private SUPContext db = new SUPContext();

        // GET: SUPItems
        public ActionResult Index()
        {
            var sUPItems = db.SUPItems.Include(s => s.SUPUser);
            return View(sUPItems.ToList());
        }

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
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName");
            return View();
        }

        // POST: SUPItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemName,Description,IsAvailable,OwnerID")] SUPItem sUPItem)
        {
            if (ModelState.IsValid)
            {
                db.SUPItems.Add(sUPItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
            return View(sUPItem);
        }

        // GET: SUPItems/Edit/5
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
            ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
            return View(sUPItem);
        }

        // POST: SUPItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName,Description,TimeStamp,IsAvailable,OwnerID")] SUPItem sUPItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
            return View(sUPItem);
        }

        // GET: SUPItems/Delete/5
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
            return View(sUPItem);
        }

        // POST: SUPItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPItem sUPItem = db.SUPItems.Find(id);
            db.SUPItems.Remove(sUPItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
