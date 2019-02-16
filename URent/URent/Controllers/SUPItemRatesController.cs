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
    public class SUPItemRatesController : Controller
    {
        private SUPContext db = new SUPContext();

        // GET: SUPItemRates
        public ActionResult Index()
        {
            var sUPItemRates = db.SUPItemRates.Include(s => s.SUPItem);
            return View(sUPItemRates.ToList());
        }

        // GET: SUPItemRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItemRate sUPItemRate = db.SUPItemRates.Find(id);
            if (sUPItemRate == null)
            {
                return HttpNotFound();
            }
            return View(sUPItemRate);
        }

        // GET: SUPItemRates/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName");
            return View();
        }

        // POST: SUPItemRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HourlyRate,DailyRate,WeeklyRate,MonthlyRate,ItemID")] SUPItemRate sUPItemRate)
        {
            if (ModelState.IsValid)
            {
                db.SUPItemRates.Add(sUPItemRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPItemRate.ItemID);
            return View(sUPItemRate);
        }

        // GET: SUPItemRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItemRate sUPItemRate = db.SUPItemRates.Find(id);
            if (sUPItemRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPItemRate.ItemID);
            return View(sUPItemRate);
        }

        // POST: SUPItemRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HourlyRate,DailyRate,WeeklyRate,MonthlyRate,ItemID")] SUPItemRate sUPItemRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPItemRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPItemRate.ItemID);
            return View(sUPItemRate);
        }

        // GET: SUPItemRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItemRate sUPItemRate = db.SUPItemRates.Find(id);
            if (sUPItemRate == null)
            {
                return HttpNotFound();
            }
            return View(sUPItemRate);
        }

        // POST: SUPItemRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPItemRate sUPItemRate = db.SUPItemRates.Find(id);
            db.SUPItemRates.Remove(sUPItemRate);
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
