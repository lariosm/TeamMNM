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
        //private SUPContext db = new SUPContext();

        /// <summary>
        /// Retrieves user ID of current user from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current user from AspNetUsers table.</returns>
        //[Authorize]
        //private string getIdentityID()
        //{
        //    return User.Identity.GetUserId();
        //}

        /// <summary>
        /// Retrieves user ID of current user from SUPUsers table that is associated with user ID from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current user from SUPUsers table associated with user ID from AspNetUsers table.</returns>
        //[Authorize]
        //private int getSUPUserID()
        //{
        //    string id = getIdentityID();
        //    SUPUser supUser = db.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
        //    int supUserid = supUser.Id;
        //    return supUserid;
        //}

        //// GET: SUPRequests
        //public ActionResult Index()
        //{
        //    var sUPRequests = db.SUPRequests.Include(s => s.SUPItem).Include(s => s.SUPUser);
        //    return View(sUPRequests.ToList());
        //}

        // GET: SUPRequests/Details/5
        /// <summary>
        /// Displays details of an item request.
        /// </summary>
        /// <param name="id">ID of an item request</param>
        /// <returns>Details of an item request</returns>
        //[Authorize]
        //public ActionResult Details(int? id)
        //{
        //    if (id == null) //No item request ID?
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SUPRequest sUPRequest = db.SUPRequests.Find(id); //Finds item request associated with that ID.
        //    if (sUPRequest == null) //Does the item request exist?
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sUPRequest);
        //}

        // GET: SUPRequests/Create
        /// <summary>
        /// Displays the item request Create page.
        /// </summary>
        /// <param name="itemId">ID of an item listing</param>
        /// <returns>View of Create.cshtml</returns>
        //[Authorize]
        //public ActionResult Create(int? itemId)
        //{
        //    SUPRequest request = new SUPRequest();
        //    request.ItemID = itemId;

        //    ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName");
        //    ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName");
        //    return View(request);
        //}

        // POST: SUPRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates an item request
        /// </summary>
        /// <param name="sUPRequest">The item request to create</param>
        /// <returns>Whether item request was successful</returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,ItemID")] SUPRequest sUPRequest)
        //{
        //    if (ModelState.IsValid) //Are all required fields filled out?
        //    {
        //        sUPRequest.RenterID = getSUPUserID();
        //        sUPRequest.TimeStamp = DateTime.Now;
        //        db.SUPRequests.Add(sUPRequest);
        //        db.SaveChanges();
        //        return RedirectToAction("GetRentersRequests");
        //    }

        //    //If not, send user back to Create item request page to make corrections.
        //    ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPRequest.ItemID);
        //    ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPRequest.RenterID);
        //    return View(sUPRequest);
        //}

        //// GET: SUPRequests/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SUPRequest sUPRequest = db.SUPRequests.Find(id);
        //    if (sUPRequest == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPRequest.ItemID);
        //    ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPRequest.RenterID);
        //    return View(sUPRequest);
        //}

        //// POST: SUPRequests/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,TimeStamp,RenterID,ItemID")] SUPRequest sUPRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sUPRequest).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ItemID = new SelectList(db.SUPItems, "Id", "ItemName", sUPRequest.ItemID);
        //    ViewBag.RenterID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPRequest.RenterID);
        //    return View(sUPRequest);
        //}

        // GET: SUPRequests/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SUPRequest sUPRequest = db.SUPRequests.Find(id);
        //    if (sUPRequest == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sUPRequest);
        //}

        // POST: SUPRequests/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SUPRequest sUPRequest = db.SUPRequests.Find(id);
        //    db.SUPRequests.Remove(sUPRequest);
        //    db.SaveChanges();
        //    return RedirectToAction("getRentersRequests");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        /// <summary>
        /// Displays a list of all item listings the renter has requested from.
        /// </summary>
        /// <returns>List of all item listings the renter has requested from.</returns>
        //[Authorize]
        //public ActionResult GetRentersRequests()
        //{
        //    int id = getSUPUserID(); //Retrieves ID of current user
        //    var requests = db.SUPRequests.Where(u => u.RenterID == id); //Find all item requests created by that user.
        //    return View(requests.ToList());
        //}
    }
}
