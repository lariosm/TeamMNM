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
    public class SUPCommentsController : Controller
    {
        private SUPContext db = new SUPContext();

        // GET: SUPComments
        public ActionResult Index()
        {
            var sUPComments = db.SUPComments.Include(s => s.SUPDiscussion).Include(s => s.SUPUser);
            return View(sUPComments.ToList());
        }

        // GET: SUPComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPComment sUPComment = db.SUPComments.Find(id);
            if (sUPComment == null)
            {
                return HttpNotFound();
            }
            return View(sUPComment);
        }

        // GET: SUPComments/Create
        public ActionResult Create()
        {
            ViewBag.DiscussionID = new SelectList(db.SUPDiscussions, "ID", "Title");
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName");
            return View();
        }

        // POST: SUPComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Details,TIMESTAMP,UserID,DiscussionID")] SUPComment sUPComment)
        {
            if (ModelState.IsValid)
            {
                db.SUPComments.Add(sUPComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiscussionID = new SelectList(db.SUPDiscussions, "ID", "Title", sUPComment.DiscussionID);
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName", sUPComment.UserID);
            return View(sUPComment);
        }

        // GET: SUPComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPComment sUPComment = db.SUPComments.Find(id);
            if (sUPComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscussionID = new SelectList(db.SUPDiscussions, "ID", "Title", sUPComment.DiscussionID);
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName", sUPComment.UserID);
            return View(sUPComment);
        }

        // POST: SUPComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Details,TIMESTAMP,UserID,DiscussionID")] SUPComment sUPComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscussionID = new SelectList(db.SUPDiscussions, "ID", "Title", sUPComment.DiscussionID);
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName", sUPComment.UserID);
            return View(sUPComment);
        }

        // GET: SUPComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPComment sUPComment = db.SUPComments.Find(id);
            if (sUPComment == null)
            {
                return HttpNotFound();
            }
            return View(sUPComment);
        }

        // POST: SUPComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPComment sUPComment = db.SUPComments.Find(id);
            db.SUPComments.Remove(sUPComment);
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
