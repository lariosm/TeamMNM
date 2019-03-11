using Microsoft.AspNet.Identity;
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
    public class SUPRequestsController : Controller
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

        // GET: SUPRequests
        public ActionResult Index()
        {
            var sUPRequests = db.SUPRequests.Include(s => s.SUPItem).Include(s => s.SUPUser);
            return View(sUPRequests.ToList());
        }

        // GET: SUPRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPRequest sUPRequest = db.SUPRequests.Find(id);
            if (sUPRequest == null)
            {
                return HttpNotFound();
            }
            return View(sUPRequest);
        }

        // GET: SUPRequests/Create
        public ActionResult Create(int? itemId)
        {
            SUPRequest request = new SUPRequest();
            request.ItemID = itemId;

            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName");
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName");
            return View(request);
        }

        // POST: SUPRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,ItemID")] SUPRequest sUPRequest)
        {
            if (ModelState.IsValid)
            {
                sUPRequest.RenterID = getSUPUserID();
                sUPRequest.TimeStamp = DateTime.Now;
                db.SUPRequests.Add(sUPRequest);
                db.SaveChanges();
                return RedirectToAction("getRentersRequests");
            }

            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPRequest.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPRequest.RenterID);
            return View(sUPRequest);
        }

        // GET: SUPRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPRequest sUPRequest = db.SUPRequests.Find(id);
            if (sUPRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPRequest.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPRequest.RenterID);
            return View(sUPRequest);
        }

        // POST: SUPRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,TimeStamp,RenterID,ItemID")] SUPRequest sUPRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPRequest.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPRequest.RenterID);
            return View(sUPRequest);
        }

        // GET: SUPRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPRequest sUPRequest = db.SUPRequests.Find(id);
            if (sUPRequest == null)
            {
                return HttpNotFound();
            }
            return View(sUPRequest);
        }

        // POST: SUPRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPRequest sUPRequest = db.SUPRequests.Find(id);
            db.SUPRequests.Remove(sUPRequest);
            db.SaveChanges();
            return RedirectToAction("getRentersRequests");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult getRentersRequests()
        {
            int id = getSUPUserID();
            var requests = db.SUPRequests.Where(u => u.RenterID == id);
            return View(requests.ToList());
        }
    }
}
