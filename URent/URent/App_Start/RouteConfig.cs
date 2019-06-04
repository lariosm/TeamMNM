using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace URent
{
    /// <summary>
    /// Registers route patterns for mapping incoming requests to a specific
    /// controller action method.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Route patterns to be registered
        /// </summary>
        /// <param name="routes">The route patterns being registered</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
