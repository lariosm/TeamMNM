using Microsoft.AspNet.Identity;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using URent.Models;

namespace URent.Controllers
{
    public class ResourcesController : Controller
    {
        SUPContext db = new SUPContext();

        /// <summary>
        /// Retrieves user ID of current logged in user from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current logged in user from AspNetUsers table.</returns>
        [Authorize]
        private string getIdentityID()
        {
            return User.Identity.GetUserId();
        }

        /// <summary>
        /// Retrieves user ID of current logged in user from SUPUsers table that is associated with user ID from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current logged in user from SUPUsers table associated with user ID from AspNetUsers table.</returns>
        [Authorize]
        private int getSUPUserID()
        {
            string id = getIdentityID();
            SUPUser supUser = db.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }


        /// <summary>
        /// Retrieves photo to display to view.
        /// </summary>
        /// <param name="id">ID of a photo to display.</param>
        /// <returns>Photo to display to view.</returns>
        // GET: Resources
        public FileResult Photo(int? id)
        {
            SUPImage p = db.SUPImages.Find(id); //Locates the photo with that ID and saves it to variable
            //Stream stream = new MemoryStream(p.Input);
            //Image file = Image.FromStream(stream);
            return File(p.Input, "image");
        }

        /// <summary>
        /// Helper method to display item photo on home and search pages
        /// </summary>
        /// <param name="id">ID of the item photo to display</param>
        /// <returns>Item photo to display</returns>
        public FileResult HomePhoto(int? id)
        {
            SUPImage pid = db.SUPImages.Where(a => a.ItemID == id).FirstOrDefault(); //Finds an image associated with the listing.
            if (pid == null) // if there is no uploaded photo with the listing, then show the default "No photo to display" photo
            {
                return base.File("/Content/Img/default.png", "image");
            }
            return File(pid.Input, "image");
        }

        /// <summary>
        /// Gets transaction data in JSON format for an owner.
        /// </summary>
        /// <returns>Transaction data in JSON format for an owner.</returns>
        [Authorize]
        public JsonResult NotificationRequest()
        {
            int id = getSUPUserID(); //Retrieve ID of current user.

            //Transaction data for an owner
            var notifications = db.SUPTransactions.Join(db.SUPUsers, t => t.RenterID, u => u.Id, (t, u) => new { t, u })
                                                   .Join(db.SUPItems, x => x.t.ItemID, i => i.Id, (x, i) => new { x.t, x.u, i })
                                                   .Where(x => x.t.OwnerID == id)
                                                   .Select(y => new { y.i.ItemName, y.u.FirstName, y.u.LastName, y.t.StartDate, y.t.EndDate, y.t.TotalPrice, y.t.TimeStamp }).ToList();


            return Json(notifications, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns client's current location information in JSON form.
        /// </summary>
        /// <returns>Client's current location info</returns>
        public JsonResult CurrentLocation()
        {
            //URI to contact IPLocate's servers using client's current IP address
            string uri = "https://www.iplocate.io/api/lookup/" + Request.UserHostAddress;

            //Create a web request
            WebRequest dataRequest = WebRequest.Create(uri);

            //Get JSON data 
            Stream dataStream = dataRequest.GetResponse().GetResponseStream();

            //Parse the received JSON data
            var parsedData = new System.Web.Script.Serialization.JavaScriptSerializer()
                                  .DeserializeObject(new StreamReader(dataStream)
                                  .ReadToEnd());

            //return JSON data
            return Json(parsedData, JsonRequestBehavior.AllowGet);
        }
    }
}