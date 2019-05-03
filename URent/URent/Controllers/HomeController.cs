using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using URent.Models;
using System.Device.Location;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using URent.Abstract;
using URent.Controllers;

namespace URent.Controllers
{
    public class HomeController : Controller
    {
        private SUPContext db = new SUPContext();

        private ISUPRepository repo;

        public HomeController(ISUPRepository itemsRepository)
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
            SUPUser supUser = repo.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }

        /// <summary>
        /// Displays the Index page with all listings
        /// </summary>
        /// <returns>List of all listings to the view</returns>
        [HttpGet]
        public ActionResult Index(double? lat, double? lng, int? radius)
        {
            List<SUPItem> sUPItems = null;
            sUPItems = GetListOfItems();

            if ((lat != null && lng != null) && radius != null)
            {
                GeoCoordinate userLocation = GetGeoCoordinateForCurrentUserLocation(lat, lng);
                sUPItems = GetItemsWithinRange(userLocation, radius);
                return View(sUPItems);
            }
            else
            {
                return View(sUPItems);
            }
        }

        public List<SUPItem> GetListOfItems()
        {
            
            List<SUPItem> newList = new List<SUPItem>(); //List of filtered item listings to return
            // Deleted .Include(s => s.SUPUser)
            if(User.Identity.IsAuthenticated)
            {
                var userId = getSUPUserID();
                newList = repo.SUPItems.Where(x => x.OwnerID != userId).Select(x => x).ToList();
            }
            else
            {
                newList = repo.SUPItems.Select(x => x).ToList(); //Gets all item listings
            }

            return newList;
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

        public List<SUPItem> GetItemsWithinRange(GeoCoordinate userLocation, int? radius)
        {
            GeoCoordinate itemLocation;
            double calculatedRadius = CalculateRadius(radius);
            List<SUPItem> sUPItems = GetListOfItems();
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