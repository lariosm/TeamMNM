using Microsoft.AspNet.Identity;
using System;
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
        /// Retrieves photo to display to view.
        /// </summary>
        /// <param name="id">ID of a photo to display.</param>
        /// <returns>Photo to display to view.</returns>
        // GET: Resources
        public FileResult Photo(int? id)
        {
            SUPImage p = db.SUPImages.Find(id);
            //Stream stream = new MemoryStream(p.Input);
            //Image file = Image.FromStream(stream);
            return File(p.Input, "image");
        }

        public JsonResult NotificationRequest()
        {
            int id = getSUPUserID(); //Retrieve ID of current user.
            Console.WriteLine(id);
            var notifications = db.SUPTransactions.Where(u => u.OwnerID == id)
                                                  .Select(i => new {  item = GetItemName(i.ItemID), renter = GetRenterName(i.RenterID), start = i.StartDate, end = i.EndDate, time = i.TimeStamp, price = i.TotalPrice})
                                                  .OrderBy(b => b.time)
                                                  .ToList(); //Find all item listings that is requested/rented from other users
            Console.WriteLine(notifications);
            return Json(notifications, JsonRequestBehavior.AllowGet);
        }

        public IQueryable<string> GetItemName(int? id)
        {
            var name = db.SUPItems.Where(x => x.Id == id).Select(y => y.ItemName);
            return (name);
        }

        public IQueryable<string> GetRenterName(int? id)
        {
            var name = db.SUPUsers.Where(x => x.Id == id).Select(y => y.FirstName);
            return (name);
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