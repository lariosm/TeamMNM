using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;
using Microsoft.AspNet.Identity;
using URent.Models;
using URent.Abstract;

namespace URent.Controllers
{
    public class SUPItemsController : Controller
    {
        private SUPContext db = new SUPContext();

        private ISUPRepository repo;

        public SUPItemsController(ISUPRepository itemsRepository)
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

        /// <summary>
        /// Checks if a user is the owner of a listing.
        /// </summary>
        /// <param name="listingId">ID of an item listing</param>
        /// <returns>Whether a user is the owner of a listing.</returns>
        [Authorize]
        private bool checkUser(int? listingId)
        {
            int userId = getSUPUserID(); //Retrieves ID of current user
            int listingOwnerId = db.SUPItems.Find(listingId).OwnerID; //Finds owner of the listing
            if (userId == listingOwnerId) //checks if current user is the owner of the listing
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Displays the Error page
        /// </summary>
        /// <returns>View of Error.cshtml</returns>
        [Authorize]
        public ActionResult Error()
        {
            return View();
        }

        // GET: SUPItems
        //public ActionResult Index()
        //{
        //    var sUPItems = db.SUPItems.Include(s => s.SUPUser);
        //    return View(sUPItems.ToList());
        //}

        // GET: SUPItems/Details/5
        /// <summary>
        /// Displays details of an item listing
        /// </summary>
        /// <param name="id">ID of an item listing</param>
        /// <returns>Details of an item listing</returns>
        public ActionResult Details(int? id)
        {
            ItemDetailsViewModel model = new ItemDetailsViewModel();
            if (id == null) //No item listing ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.sUPItem = db.SUPItems.Find(id); //Finds the item listing associated with that ID
            List<SUPItemReview> reviews = db.SUPItemReviews.Where(x => x.ItemBeingReviewedID == id).ToList();
            model.sUPItemReviews = reviews;

         
            model = DetailsHelper(model, id);
            GetItemRatingStats(model, id);
            //model.ItemBeingReviewedID = id;
            if(User.Identity.IsAuthenticated)
            {
                model.UserDoingReviewID = getSUPUserID();

            }
            
            if (model.sUPItem == null) //Does the item listing exist?
            {
                return HttpNotFound();
            }
            SUPImage pid = db.SUPImages.Where(a => a.ItemID == id).FirstOrDefault(); //Finds an image associated with the listing.
            if (pid != null) //Display an image if it exists in the table.
            {
                ViewBag.Send = pid.Id;
            }
            return View(model);
        }

        //public List<SUPUser> GetNameOfReviewer(List<SUPItemReview> reviews)
        //{
        //    List<SUPUser> reviewerName = new List<SUPUser>();
        //    for (int i = 0; i < reviews.Count(); i++)
        //    {
        //        reviewerName.Add(db.SUPUsers.Where(x => x.Id == reviews[i].UserDoingReviewID).SelectMany(y => new { id = y.Id, first = y.FirstName, last = y.LastName )).FirstOrDefault());
        //    }
        //    return (reviewerName);
        //}

        public ItemDetailsViewModel GetItemRatingStats(ItemDetailsViewModel model, int? id)
        {
            int? ratingSum = 0;
            int? ratingCount = 0;
            double ratingAverage = 0;

            var ratings = db.SUPItemReviews.Where(d => d.ItemBeingReviewedID == id).Select(y => y.Rating).ToList();
            if (ratings.Count() > 0)
            {
                ratingSum = ratings.Sum(d => d);
                
                ratingCount = ratings.Count();

                ratingAverage = (double) ratingSum / (double) ratingCount;
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

        public ItemDetailsViewModel DetailsHelper(ItemDetailsViewModel model, int? id)
        {
            model.ItemBeingReviewedID = id;
            return (model);
        }

        [Authorize, HttpPost]
        public ActionResult Details([Bind(Include = "Details, Ratings, ItemBeingReviewedID, UserDoingReviewID")]ItemDetailsViewModel itemReview)
        {
            SUPItemReview review = new SUPItemReview { Details = itemReview.Details, Rating = itemReview.Ratings, ItemBeingReviewedID = itemReview.ItemBeingReviewedID, UserDoingReviewID = itemReview.UserDoingReviewID };
            db.SUPItemReviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = itemReview.ItemBeingReviewedID });
        }


        // GET: SUPItems/Create
        /// <summary>
        /// Displays the item listing Create page.
        /// </summary>
        /// <returns>View of Create.cshtml</returns>
        [Authorize]
        public ActionResult Create()
        {
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, supuser.Id, "FirstName");
            return View();
        }

        // POST: SUPItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates an item listing.
        /// </summary>
        /// <param name="sUPItem">The item listing to create</param>
        /// <param name="photoElementID">The photo ID this listing will be associated with</param>
        /// <returns>Whether creation of item listing was successful.</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemName,Description,IsAvailable,DailyPrice")] SUPItem sUPItem, int? photoElementID)
        {
            if (ModelState.IsValid) //Are all required fields filled out and valid?
            {
                sUPItem.OwnerID = getSUPUserID(); //Retrieve ID of current user to bind to item listing.
                sUPItem.Lat = db.SUPUsers.Where(x => x.Id.Equals(sUPItem.OwnerID)).Select(x => x.Lat).FirstOrDefault();
                sUPItem.Lng = db.SUPUsers.Where(x => x.Id.Equals(sUPItem.OwnerID)).Select(x => x.Lng).FirstOrDefault();


                if (photoElementID != null) //is a photo ID value present?
                {
                    //Add the item listing to the database
                    db.SUPItems.Add(sUPItem);
                    db.SaveChanges();

                    //Fetch the photo matching passed photoElementID value and link it to this listing.
                    SUPImage p = db.SUPImages.Find(photoElementID);
                    p.ItemID = sUPItem.Id;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else //create a listing without any photos
                {
                    sUPItem.OwnerID = getSUPUserID();
                    db.SUPItems.Add(sUPItem);
                    db.SaveChanges();
                }

                return RedirectToAction("GetUserItems");

            }
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);

            //If not, take the user back to the Create page.
            return View(sUPItem);
        }

        // GET: SUPItems/Edit/5
        /// <summary>
        /// Displays an item listing page.
        /// </summary>
        /// <param name="id">ID of an item listing</param>
        /// <returns>Whether access to the edit page of a listing is permitted.</returns>
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null) //No item listing ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItem sUPItem = db.SUPItems.Find(id); //Finds the item listing associated with that ID.
            if (sUPItem == null) //Does the item listing exist?
            {
                return HttpNotFound();
            }
            bool check = checkUser(id);

            //Is current user the owner of the requested item listing?
            if (check == false)
            {
                return RedirectToAction("Error"); //If not, show an error page.
            }
            else
            {
                return View(sUPItem); //Otherwise, allow user to edit listing.
            }
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);
        }

        // POST: SUPItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the item listing
        /// </summary>
        /// <param name="sUPItem">The item listing to modify</param>
        /// <returns>Whether edits to listing were successful.</returns>
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName,Description,TimeStamp,IsAvailable,DailyPrice")] SUPItem sUPItem, int? photoElementID)
        {
            if (ModelState.IsValid) //Are all required fields filled out and valid?
            {
                if (photoElementID != null) //is a photo ID value present?
                {
                    SUPImage sUPImage = db.SUPImages.Where(x => x.ItemID == sUPItem.Id).FirstOrDefault();
                    if (sUPImage != null)
                    {
                        db.SUPImages.Remove(sUPImage);
                    }
                    //Fetch the photo matching passed photoElementID value and link it to this listing.
                    SUPImage p = db.SUPImages.Find(photoElementID);
                    p.ItemID = sUPItem.Id;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else //create a listing without any photos
                {
                    sUPItem.OwnerID = getSUPUserID();
                    db.Entry(sUPItem).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("GetUserItems");

            }
            //ViewBag.OwnerID = new SelectList(db.SUPUsers, "Id", "FirstName", sUPItem.OwnerID);

            //If not, send the user back to the Edit page.
            return View(sUPItem);
        }

        // GET: SUPItems/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPItem sUPItem = db.SUPItems.Find(id);
            if (sUPItem == null)
            {
                return HttpNotFound();
            }
            bool check = checkUser(id);
            if (check == false)
            {
                return RedirectToAction("Error");
            }
            else
            {
                return View(sUPItem);
            }
        }

        // POST: SUPItems/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPItem sUPItem = db.SUPItems.Find(id);
            SUPImage sUPImage = db.SUPImages.Where(x => x.ItemID == id).FirstOrDefault();
            if (sUPImage != null)
            {
                db.SUPImages.Remove(sUPImage);
            }
            db.SUPItems.Remove(sUPItem);
            db.SaveChanges();
            return RedirectToAction("GetUserItems");
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
        /// Displays a list of all item listings created by current user.
        /// </summary>
        /// <returns>List of all item listings created current user.</returns>
        [Authorize]
        public ActionResult GetUserItems()
        {
            int userId = getSUPUserID(); //Retrieves ID of current user.
            var myItems = GetItemsByUserId(userId); //Find all listings created by that user.
            return View(myItems.ToList());
        }

        public List<SUPItem> GetItemsByUserId(int id)
        {
            List<SUPItem> myItems = repo.SUPItems.Where(u => u.OwnerID == id).ToList();
            return myItems;
        }

        /* DropZone Method called from Form element in View */
        /// <summary>
        /// Saves uploaded photo to database and returns it as JSON data.
        /// </summary>
        /// <returns>JSON photo data</returns>
        [Authorize]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            int pid = 0;

            try
            {
                // base instance of Image for saving information
                SUPImage i = new SUPImage();
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    //assign file name to filename
                    i.Filename = file.FileName;
                    // read in InputStream into input
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        i.Input = reader.ReadBytes((int)file.InputStream.Length);
                    }
                    //save file to local db
                    // !!!!!!!!! NOTE: it is saving into SUPUserTables database
                }
                db.SUPImages.Add(i);
                db.SaveChanges();
                pid = i.Id;
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully) //Was photo saved successfully?
            {
                //Return JSON data containing photo info.
                return Json(new { id = "PhotoID", value = pid });
            }
            else
            {
                //Return JSON data containing error message.
                return Json(new { Message = "Error in saving file" });
            }
        }

        /// <summary>
        /// Performs item listing search based on keywords entered from search box.
        /// </summary>
        /// <param name="query">Keywords to search</param>
        /// <returns>A list of item listings matching keywords entered</returns>
        [HttpGet]
        public ActionResult Search(string query, double? lat, double? lng, int? radius)
        {
            ViewBag.ShowError = false;

            if (!string.IsNullOrWhiteSpace(query)) //Makes sure query is not blank or contains only whitespaces.
            {
                List<SUPItem> sUPItems = new List<SUPItem>();
                sUPItems = GetListOfItemsWithQueryString(query);

                if (!sUPItems.Any()) //No matching results? The search was unsuccessful.
                {
                    ViewBag.ShowError = true; //Display unsuccessful search message.
                    return View();

                }
                else if ((lat != null && lng != null) && radius != null) //We got matching results. The search was a success!
                {
                    GeoCoordinate userLocation = GetGeoCoordinateForCurrentUserLocation(lat, lng);
                    sUPItems = GetItemsWithinRange(sUPItems, userLocation, radius);
                    ViewBag.ResultString = query; //Keywords to display to the view.
                    return View(sUPItems.ToList());
                }
                else
                {
                    ViewBag.ResultString = query; //Keywords to display to the view.
                    return View(sUPItems.ToList());

                }

            }
            else
            {
                ViewBag.ShowError = true; //Display unsuccessful search message.
                return View();
            }
        }

        public List<SUPItem> GetListOfItemsWithQueryString(string query)
        {
            //NOTE: change 'db' back to 'repo' when we find why Contains() LINQ function doesn't work correctly.
            var sUPItems = db.SUPItems.Where(s => s.ItemName.Contains(query)); //Performs the query

            return sUPItems.ToList();
        }

        public GeoCoordinate GetGeoCoordinateForCurrentUserLocation(double? lat, double? lng)
        {
            var userLocation = new GeoCoordinate((double)lat, (double)lng);
            return userLocation;
        }

        public double CalculateRadius(int? radiusInMiles)
        {
            double meters = 1609.344;
            double radiusInMeters = (double)radiusInMiles * meters;
            return radiusInMeters;
        }

        public List<SUPItem> GetItemsWithinRange(List<SUPItem> itemList, GeoCoordinate userLocation, int? radius)
        {
            GeoCoordinate itemLocation;
            double calculatedRadius = CalculateRadius(radius);
            List<SUPItem> sUPItems = itemList;
            List<SUPItem> newList = new List<SUPItem>();

            //Traverse through sUPItems list
            for (int i = 0; i < sUPItems.Count(); i++)
            {
                itemLocation = new GeoCoordinate(sUPItems[i].Lat, sUPItems[i].Lng);
                if (userLocation.GetDistanceTo(itemLocation) <= calculatedRadius) //Is an item within specified radius?
                {
                    newList.Add(sUPItems[i]); //If so, add to list.
                }
            }
            return newList;
        }

        public ActionResult makeAvailable(int itemId)
        {
            SUPItem i = db.SUPItems.Find(itemId);
            if (i.IsAvailable == false)
            {
                i.IsAvailable = true;
                db.Entry(i).State = EntityState.Modified;
                //db.Entry(sUPTransaction).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = itemId });
        }
    }
}
