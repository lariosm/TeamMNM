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

        /// <summary>
        /// Retrieves user ID of current user from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current user from AspNetUsers table.</returns>
        [Authorize]
        private string getIdentityID()
        {
            return User.Identity.GetUserId();
        }

        /// <summary>
        /// Retrieves user ID of current user from SUPUsers table that is associated with user ID from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current user from SUPUsers table associated with user ID from AspNetUsers table.</returns>
        [Authorize]
        private int getSUPUserID()
        {
            string id = getIdentityID();
            SUPUser supUser = db.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }

        //// GET: SUPTransactions
        //[Authorize]
        //public ActionResult Index()
        //{
        //    var sUPTransactions = db.SUPTransactions.Include(s => s.SUPItem).Include(s => s.SUPUser);
        //    return View(sUPTransactions.ToList());
        //}

        // GET: SUPTransactions/Details/5
        /// <summary>
        /// Displays details of a transaction
        /// </summary>
        /// <param name="id">ID of a transaction</param>
        /// <returns>Details of a transaction.</returns>
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null) //No transaction ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPTransaction sUPTransaction = db.SUPTransactions.Find(id); //Finds the transaction with that ID.
            if (sUPTransaction == null) //Does the transaction exist?
            {
                return HttpNotFound();
            }
            return View(sUPTransaction);
        }

        // GET: SUPTransactions/Create
        /// <summary>
        /// Displays transaction Create page.
        /// </summary>
        /// <param name="itemId">ID of an item listing</param>
        /// <param name="dailyPrice">Price on the item listing.</param>
        /// <returns>View of Create.cshtml</returns>
        [Authorize]
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
        /// <summary>
        /// Creates a transaction
        /// </summary>
        /// <param name="sUPTransaction">The transaction to create</param>
        /// <returns>Whether a transaction was created successfully.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,TotalPrice,RenterID,OwnerID,ItemID")] SUPTransaction sUPTransaction)
        {
            //DateTime startDate = DateTime.TryParse(sUPTransaction.StartDate.ToString(), out DateTime output);
            //var endDate = new DateTime(sUPTransaction.EndDate.Ticks);
            //var areValidDates = IsValidDate(DateTime.TryParse(sUPTransaction.StartDate.Ticks));

            if (ModelState.IsValid) //Are required fields filled out?
            {
                //Are start and end date inputs in the proper format?
                if (DateTime.TryParse(sUPTransaction.StartDate.ToString(), out DateTime outputStartDate) && DateTime.TryParse(sUPTransaction.EndDate.ToString(), out DateTime outputEndDate))
                {
                    //Are start and end dates valid?
                    if(IsValidDate(sUPTransaction.StartDate, sUPTransaction.EndDate))
                    {
                        var totalDays = (sUPTransaction.EndDate - sUPTransaction.StartDate).TotalDays;
                        var dailyRate = db.SUPItems.Where(x => x.Id == sUPTransaction.ItemID).Select(x => x.DailyPrice).FirstOrDefault();
                        var totalPrice = dailyRate * (decimal)totalDays;

                        //verify total price are the same client and server side.
                        if(totalPrice == sUPTransaction.TotalPrice)
                        {
                            SUPItem i = db.SUPItems.Find(sUPTransaction.ItemID);
                            i.IsAvailable = false;
                            db.Entry(i).State = EntityState.Modified;
                            sUPTransaction.RenterID = getSUPUserID();
                            sUPTransaction.OwnerID = i.OwnerID;
                            db.SUPTransactions.Add(sUPTransaction);
                            //db.Entry(sUPTransaction).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("GetRentersTransactions");
                        }
                    }
                }
            }

            //If any of the above statements are false, send the user back to the Create transaction page to make corrections.
            ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPTransaction.ItemID);
            ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPTransaction.RenterID);
            return View(sUPTransaction);
        }

        //Check that start and end dates are in the proper format.
        public bool checkDateFormat(DateTime startDate, DateTime endDate)
        {
            return DateTime.TryParse(startDate.ToString(), out DateTime outputStartDate) && DateTime.TryParse(endDate.ToString(), out DateTime outputEndDate);
        }

        public bool checkTotalPrice(int totalPrice, SUPTransaction transaction)
        {
            return totalPrice == transaction.TotalPrice;
        }

        public static bool IsValidDate(DateTime startDate, DateTime endDate)
        {
            var current = DateTime.Today;
            return startDate >= current && endDate > startDate;
        }

        //// GET: SUPTransactions/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SUPTransaction sUPTransaction = db.SUPTransactions.Find(id);
        //    if (sUPTransaction == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPTransaction.ItemID);
        //    ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPTransaction.RenterID);
        //    return View(sUPTransaction);
        //}

        //// POST: SUPTransactions/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,TimeStamp,TotalPrice,RenterID,ItemID")] SUPTransaction sUPTransaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sUPTransaction).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPTransaction.ItemID);
        //    ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPTransaction.RenterID);
        //    return View(sUPTransaction);
        //}

        // GET: SUPTransactions/Delete/5
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPTransaction sUPTransaction = db.SUPTransactions.Find(id);
            db.SUPTransactions.Remove(sUPTransaction);
            db.SaveChanges();
            return RedirectToAction("getRentersTransactions");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Displays a list of all item listings the user has rented out from.
        /// </summary>
        /// <returns>List of all item listings the user has rented out from</returns>
        [Authorize]
        public ActionResult getRentersTransactions()
        {
            int id = getSUPUserID(); //Retrieve ID of current user.
            var transactions = db.SUPTransactions.Where(u => u.RenterID == id); //Find all item listings the user has rented out from.
            return View(transactions.ToList());
        }
    }
}
