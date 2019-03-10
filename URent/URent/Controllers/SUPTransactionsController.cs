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
    public class SUPTransactionsController : Controller
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

        // GET: SUPTransactions
        public ActionResult Index()
        {
            var sUPTransactions = db.SUPTransactions.Include(s => s.SUPItem).Include(s => s.SUPUser);
            return View(sUPTransactions.ToList());
        }

        // GET: SUPTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPTransaction sUPTransaction = db.SUPTransactions.Find(id);
            if (sUPTransaction == null)
            {
                return HttpNotFound();
            }
            return View(sUPTransaction);
        }

        // GET: SUPTransactions/Create
        public ActionResult Create(int? itemId, decimal? dailyPrice)
        {
            SUPTransaction transaction = new SUPTransaction();
            transaction.ItemID = itemId;

            ViewBag.dailyPrice = dailyPrice;

            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName");
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName");
            return View(transaction);
        }

        // POST: SUPTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,TotalPrice,RenterID,ItemID")] SUPTransaction sUPTransaction)
        {
            if (ModelState.IsValid)
            {
                sUPTransaction.RenterID = getSUPUserID();
                sUPTransaction.TimeStamp = DateTime.Now;
                db.SUPTransactions.Add(sUPTransaction);
                //db.Entry(sUPTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("getRentersTransactions");
            }

            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPTransaction.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPTransaction.RenterID);
            return View(sUPTransaction);
        }

        // GET: SUPTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPTransaction sUPTransaction = db.SUPTransactions.Find(id);
            if (sUPTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPTransaction.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPTransaction.RenterID);
            return View(sUPTransaction);
        }

        // POST: SUPTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,TimeStamp,TotalPrice,RenterID,ItemID")] SUPTransaction sUPTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPTransaction.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPTransaction.RenterID);
            return View(sUPTransaction);
        }

        // GET: SUPTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPTransaction sUPTransaction = db.SUPTransactions.Find(id);
            if (sUPTransaction == null)
            {
                return HttpNotFound();
            }
            return View(sUPTransaction);
        }

        // POST: SUPTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPTransaction sUPTransaction = db.SUPTransactions.Find(id);
            db.SUPTransactions.Remove(sUPTransaction);
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

        public ActionResult getRentersTransactions()
        {
            int id = getSUPUserID();
            var transactions = db.SUPTransactions.Where(u => u.RenterID == id);
            return View(transactions.ToList());
        }
    }
}
