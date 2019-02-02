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
    public class SUPDiscussionsController : Controller
    {
        private SUPContext db = new SUPContext();

        // GET: SUPDiscussions
        public ActionResult Index()
        {
            var sUPDiscussions = db.SUPDiscussions.Include(s => s.SUPUser);
            return View(sUPDiscussions.ToList());
        }

        // GET: SUPDiscussions/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            
            //if (sUPDiscussion == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(sUPDiscussion);
            var model = new SUPDiscussionDetails();
            model.sUPDiscussion = db.SUPDiscussions.Find(id);
            model.sUPComments = db.SUPComments.Include(s => s.SUPDiscussion).Include(s => s.SUPUser).ToList();
            return View(model);
        }

        // GET: SUPDiscussions/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName");
            return View();
        }

        // POST: SUPDiscussions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,URL,TIMESTAMP,UserID")] SUPDiscussion sUPDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.SUPDiscussions.Add(sUPDiscussion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName", sUPDiscussion.UserID);
            return View(sUPDiscussion);
        }

        // GET: SUPDiscussions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPDiscussion sUPDiscussion = db.SUPDiscussions.Find(id);
            if (sUPDiscussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName", sUPDiscussion.UserID);
            return View(sUPDiscussion);
        }

        // POST: SUPDiscussions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,URL,TIMESTAMP,UserID")] SUPDiscussion sUPDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPDiscussion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.SUPUsers, "ID", "FirstName", sUPDiscussion.UserID);
            return View(sUPDiscussion);
        }

        // GET: SUPDiscussions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPDiscussion sUPDiscussion = db.SUPDiscussions.Find(id);
            if (sUPDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(sUPDiscussion);
        }

        // POST: SUPDiscussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPDiscussion sUPDiscussion = db.SUPDiscussions.Find(id);
            db.SUPDiscussions.Remove(sUPDiscussion);
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
