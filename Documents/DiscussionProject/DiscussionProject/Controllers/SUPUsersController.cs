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
    public class SUPUsersController : Controller
    {
        private SUPContext db = new SUPContext();

        // GET: SUPUsers
        public ActionResult Index()
        {
            return View(db.SUPUsers.ToList());
        }

        // GET: SUPUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPUser sUPUser = db.SUPUsers.Find(id);
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            return View(sUPUser);
        }

        // GET: SUPUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUPUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,NetUserId")] SUPUser sUPUser)
        {
            if (ModelState.IsValid)
            {
                db.SUPUsers.Add(sUPUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUPUser);
        }

        // GET: SUPUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPUser sUPUser = db.SUPUsers.Find(id);
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            return View(sUPUser);
        }

        // POST: SUPUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,NetUserId")] SUPUser sUPUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUPUser);
        }

        // GET: SUPUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPUser sUPUser = db.SUPUsers.Find(id);
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            return View(sUPUser);
        }

        // POST: SUPUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPUser sUPUser = db.SUPUsers.Find(id);
            db.SUPUsers.Remove(sUPUser);
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
