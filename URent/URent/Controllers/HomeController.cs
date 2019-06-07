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
            SUPUser supUser = repo.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }

        /// <summary>
        /// Home page with item listings within a specified radius (if any)
        /// </summary>
        /// <param name="lat">User's current latitude</param>
        /// <param name="lng">User's current longitude</param>
        /// <param name="radius">The radius being specified in miles</param>
        /// <returns>View with item listings within a specified radius (if any)</returns>
        [HttpGet]
        public ActionResult Index(double? lat, double? lng, int? radius)
        {
            List<SUPItem> sUPItems = null; //List of item listings to return to the view
            sUPItems = GetListOfItems(); //Queries database for item listings to add to list

            if ((lat != null && lng != null) && radius != null) //Are we given geocoordinates and radius to work with?
            {
                //Use the user's current longitude and latitude values to generate a GeoCoordinate value.
                GeoCoordinate userLocation = GetGeoCoordinateForCurrentUserLocation(lat, lng);
                
                //Add all item listings within a specified radius from the user's current location to the list.
                sUPItems = GetItemsWithinRange(userLocation, radius);
                return View(sUPItems);
            }
            else //If not, return a view with all item listings
            {
                return View(sUPItems);
            }
        }

        /// <summary>
        /// Queries database for item listings
        /// </summary>
        /// <returns>Item listings</returns>
        public List<SUPItem> GetListOfItems()
        { 
            List<SUPItem> newList = new List<SUPItem>(); //List of filtered item listings to return
            if (User.Identity.IsAuthenticated) //Is user is a logged in user?
            {
                var userId = getSUPUserID(); //Retrieve ID of current logged in user
                //Add all item listings to the list except the ones the logged in user has created.
                newList = repo.SUPItems.Where(x => x.OwnerID != userId).Select(x => x).ToList();
            }
            else
            {
                newList = repo.SUPItems.Select(x => x).ToList(); //Add all item listings to the list.
            }

            return newList;
        }

        /// <summary>
        /// Uses user's current longitude and latitude values to generate a GeoCoordinate value
        /// </summary>
        /// <param name="lat">User's current longitude</param>
        /// <param name="lng">User's current latitude</param>
        /// <returns>User's location as GeoCoordinate value</returns>
        public GeoCoordinate GetGeoCoordinateForCurrentUserLocation(double? lat, double? lng)
        {
            var userLocation = new GeoCoordinate((double)lat, (double)lng);
            return userLocation;
        }

        /* NOTE: Given GeoCoordinate classes only work with meter values when measuring the distance between
        two locations. This method helps make the necessary convertion from miles to meters for accuracy. */
        /// <summary>
        /// Converts radius input from miles to meters.
        /// </summary>
        /// <param name="radiusInMiles">Radius in miles</param>
        /// <returns>Radius in meters</returns>
        public double CalculateRadius(int? radiusInMiles)
        {
            double meters = 1609.344; //Meters in a mile
            double radiusInMeters = (double)radiusInMiles * meters; //Formula for converting from miles to meters
            return radiusInMeters;
        }

        /// <summary>
        /// Gets item listings within a specified radius
        /// </summary>
        /// <param name="userLocation">Geocoordinates of user's current location</param>
        /// <param name="radius">The radius being specified in miles</param>
        /// <returns>Item listings within specified radius</returns>
        public List<SUPItem> GetItemsWithinRange(GeoCoordinate userLocation, int? radius)
        {
            GeoCoordinate itemLocation; //Initalizes GeoCoordinate variable to be used later.
            double calculatedRadius = CalculateRadius(radius); //The radius in meters
            List<SUPItem> sUPItems = GetListOfItems(); //Queries database for item listings to add to list.
            List<SUPItem> newList = new List<SUPItem>(); //List of filtered item listings to return.

            //Traverse through sUPItems list
            for (int i = 0; i < sUPItems.Count(); i++)
            {
                itemLocation = new GeoCoordinate(sUPItems[i].Lat, sUPItems[i].Lng);
                if (userLocation.GetDistanceTo(itemLocation) <= calculatedRadius) //Is an item listing within specified radius?
                {
                    newList.Add(sUPItems[i]); //If so, add to list.
                }
            }
            return newList;
        }

        public ActionResult About()
        {
            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}