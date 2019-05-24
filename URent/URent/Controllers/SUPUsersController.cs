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
using URent.Abstract;

namespace URent.Controllers
{
    public class SUPUsersController : Controller
    {
        private SUPContext db = new SUPContext();


        private ISUPRepository repo;

        public SUPUsersController(ISUPRepository itemsRepository)
        {
            repo = itemsRepository;
        }

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

        public ActionResult ShowAllUserReviews(int id)
        {
            AllReviewsModel model = new AllReviewsModel();
            model.uId = id;
            model.uName = db.SUPUsers.Where(x => x.Id == id).Select(x => x.FirstName).FirstOrDefault().ToString();
            model.uReviews = db.SUPUserReviews.Where(x => x.UserBeingReviewedID == id).OrderByDescending(x => x.Timestamp).ToList();
            return View(model);
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
        //[Authorize]
        public ActionResult Details(int? id)
        {
            SUPUser sUPUser = db.SUPUsers.Find(id); //Finds user account with that ID.
            if (User.Identity.IsAuthenticated)
            {
                var userId = getSUPUserID();
                sUPUser = db.SUPUsers.Where(x => x.Id == userId).Select(y => y).FirstOrDefault();
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
                return RedirectToAction("Error_EditViewAnotherAccount", "Error");
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
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,StreetAddress,CityAddress,StateAddress,ZipCode,TimeStamp,Lat,Lng,NetUserId")] SUPUser sUPUser)
        {
            if (ModelState.IsValid) //Are required fields filled out?
            {
                db.Entry(sUPUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = sUPUser.Id });
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


        [Authorize]
        public ActionResult Notifications()
        {
            return View(); // return list of transactions that have this owner's id
        }

        [HttpGet]
        public ActionResult UserProfile(int? id)
        {
            ProfileViewModel profile = new ProfileViewModel();
            profile = ProfileHelper(profile, id);
            if (User.Identity.IsAuthenticated)
            {
                profile.UserDoingReviewID = getSUPUserID();
            }
            profile.sUPUserReviews = db.SUPUserReviews.Where(x => x.UserBeingReviewedID == id).OrderByDescending(x => x.Timestamp).Take(3).ToList();
            GetUserRatingStats(profile, id);

            //SUPUser sUPUser = db.SUPUsers.Find(id); //Finds user account with that ID.
            if (id == null) //No user ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (profile.UserBeingReviewedID == null) //Does a user account exist?
            {
                return HttpNotFound();
            }
            //returns the user with that SUPUser ID
            ViewBag.ReviewerName = db.SUPUsers.Where(x => x.Id == profile.UserDoingReviewID).Select(y => y.FirstName).ToString();
            return View(profile);
        }

        public ProfileViewModel ProfileHelper(ProfileViewModel profile, int? id)
        {
            profile.UserBeingReviewedID = id;
            profile.FirstName = repo.SUPUsers.Where(x => x.Id == id).Select(y => y.FirstName).FirstOrDefault();
            profile.LastName = repo.SUPUsers.Where(x => x.Id == id).Select(y => y.LastName).FirstOrDefault();
            return (profile);
        }

        public ProfileViewModel GetUserRatingStats(ProfileViewModel model, int? id)
        {
            int? ratingSum = 0;
            int? ratingCount = 0;
            double ratingAverage = 0;

            var ratings = db.SUPUserReviews.Where(d => d.UserBeingReviewedID == id).Select(d => d.Rating).ToList();
            if (ratings.Count() > 0)
            {
                ratingSum = ratings.Sum(d => d);

                ratingCount = ratings.Count();

                ratingAverage = (double)ratingSum / (double)ratingCount;
                model.RatingAverage = ratingAverage;
                model.RatingCount = ratingCount;

                return model;
            }
            else
            {
                model.RatingAverage = 0;
                model.RatingCount = 0;
                return model;
            }
        }


        //[HttpPost, Authorize]
        //public ActionResult UserProfile([Bind(Include = "Details, UserBeingReviewedId")] SUPUserReview sUPUserReview)
        //{
        //    var userDoingReview = getSUPUserID();
        //    sUPUserReview.UserDoingReviewID = userDoingReview;
        //    //profile.sUPUser = db.SUPUsers.Where(x => x.Id == sUPUserReview.UserBeingReviewedID).FirstOrDefault();

        //    if (ModelState.IsValid) //Are all required fields filled out and valid?
        //    {
        //        //Add the item listing to the database
        //        db.SUPUserReviews.Add(sUPUserReview);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("UserProfile", new { id = sUPUserReview.UserBeingReviewedID} );
        //}

        [HttpPost]
        public ActionResult UserProfile([Bind(Include = "Details, Ratings, UserBeingReviewedID, UserDoingReviewID")]ProfileViewModel userReview)
        {
            SUPUserReview review = new SUPUserReview { Details = userReview.Details, Rating = userReview.Ratings, UserBeingReviewedID = userReview.UserBeingReviewedID, UserDoingReviewID = userReview.UserDoingReviewID};
            db.SUPUserReviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("UserProfile", new { id = userReview.UserBeingReviewedID});
        }

    }
}
