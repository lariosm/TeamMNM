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
    public class SUPUsersController : Controller
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

        // GET: SUPUsers
        //public ActionResult Index()
        //{
        //    return View(db.SUPUsers.ToList());
        //}

        // GET: SUPUsers/Details/5
        /// <summary>
        /// Displays user account info of a user.
        /// </summary>
        /// <param name="id">ID of a user account</param>
        /// <returns>User account info of a user.</returns>
        [Authorize]
        public ActionResult Details(int? id)
        {
            SUPUser sUPUser = db.SUPUsers.Find(id); //Finds user account with that ID.
            if (User.Identity.IsAuthenticated)
            {
                sUPUser = db.SUPUsers.Find(getSUPUserID());
                return View(sUPUser);
            }
            if (id == null) //No user ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (sUPUser == null) //Does a user account exist?
            {
                return HttpNotFound();
            }
            return View(sUPUser);
        }

        //// GET: SUPUsers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SUPUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DateOfBirth,StreetAddress,CityAddress,StateAddress,ZipCode,TimeStamp,NetUserId")] SUPUser sUPUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SUPUsers.Add(sUPUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sUPUser);
        //}

        // GET: SUPUsers/Edit/5
        /// <summary>
        /// Displays user account edit page.
        /// </summary>
        /// <param name="id">ID of a user account</param>
        /// <returns>User account edit page</returns>
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPUser sUPUser = db.SUPUsers.Find(id);
            //sUPUser.DateOfBirth = dob;
            //sUPUser.NetUserId = netid;
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            if(id == getSUPUserID())
            {
                return View(sUPUser);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: SUPUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits user account
        /// </summary>
        /// <param name="sUPUser">User account to edit.</param>
        /// <returns>Whether edits to a user account was successful.</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,StreetAddress,CityAddress,StateAddress,ZipCode,TimeStamp")] SUPUser sUPUser)
        {
            if (ModelState.IsValid) //Are required fields filled out?
            {
                db.Entry(sUPUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = sUPUser.Id });
            }

            //If not, send user back to edit page to make corrections.
            return View(sUPUser);
        }

        // GET: SUPUsers/Delete/5
        /// <summary>
        /// Displays user account delete page.
        /// </summary>
        /// <param name="id">ID of a user account</param>
        /// <returns>User account delete page</returns>
        [Authorize]
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
        /// <summary>
        /// Deletes user account
        /// </summary>
        /// <param name="id">ID of a user account</param>
        /// <returns>Successful deletion of a user account.</returns>
        [Authorize]
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

        /// <summary>
        /// Displays error page
        /// </summary>
        /// <returns>View of Error.cshtml</returns>
        [Authorize]
        public ActionResult Error()
        {
            return View();
        }

        [Authorize]
        public ActionResult Notifications()
        {
            int id = getSUPUserID(); //Retrieve ID of current user.
            var notifications = db.SUPTransactions.Where(u => u.OwnerID == id).OrderByDescending(x => x.TimeStamp); //Find all item listings that is requested/rented from other users
            return View(notifications.ToList()); // return list of transactions that have this owner's id
        }

        [HttpGet]
        public new ActionResult Profile(int? id)
        {
            ProfileViewModel profile = new ProfileViewModel();
            profile.sUPUser = db.SUPUsers.Find(id);
            //SUPUser sUPUser = db.SUPUsers.Find(id); //Finds user account with that ID.
            if (id == null) //No user ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (profile.sUPUser == null) //Does a user account exist?
            {
                return HttpNotFound();
            }
            //returns the user with that SUPUser ID
            return View(profile);
        }

        [HttpPost, Authorize]
        public new ActionResult Profile([Bind(Include = "Id,Details,TimeStamp,UserDoingReviewID,UserBeingReviewedID")] SUPUserReview sUPUserReview, [Bind(Include = "FirstName,LastName")] SUPUser sUPuser)
        {
            ProfileViewModel profile = new ProfileViewModel();
            profile.sUPUser = db.SUPUsers.Where(x => x.Id == sUPUserReview.UserBeingReviewedID).Single();

            if (ModelState.IsValid) //Are all required fields filled out and valid?
            {
                //Add the item listing to the database
                db.SUPUserReviews.Add(sUPUserReview);
                db.SaveChanges();
            }

            return View(profile);
        }
    }
}
