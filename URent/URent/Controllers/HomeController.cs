using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using URent.Models;
using System.Device.Location;
using Microsoft.AspNet.Identity;

namespace URent.Controllers
{
    public class HomeController : Controller
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

        /// <summary>
        /// Displays the Index page with all listings
        /// </summary>
        /// <returns>List of all listings to the view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<SUPItem> sUPItems = null;
            sUPItems = db.SUPItems.Include(s => s.SUPUser).ToList();
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if(GetItemWithinRange() != null)
                {
                    sUPItems = GetItemWithinRange();
                    return View(sUPItems);
                }
                else
                {
                    return View(sUPItems);
                }
            }
            else
            {
                return View(sUPItems);
            }
        }

        public List<SUPItem> GetItemWithinRange()
        {
            var sampleDistance = 40000;
            List<SUPItem> newList = new List<SUPItem>();
            GeoCoordinate itemLocation;
            var supUser = getSUPUserID();
            var userLat = db.SUPUsers.Where(x => x.Id.Equals(supUser)).Select(x => x.Lat).FirstOrDefault();
            var userLng = db.SUPUsers.Where(x => x.Id.Equals(supUser)).Select(x => x.Lng).FirstOrDefault();
            var userLocation = new GeoCoordinate((double)userLat, (double)userLng);
            var sUPItems = db.SUPItems.Include(s => s.SUPUser).ToList();
            for(int i = 0; i < sUPItems.Count(); i++)
            {
                itemLocation = new GeoCoordinate(sUPItems[i].Lat, sUPItems[i].Lng);
                if(userLocation.GetDistanceTo(itemLocation) <= sampleDistance)
                {
                    newList.Add(sUPItems[i]);
                }
            }
            return newList;
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}