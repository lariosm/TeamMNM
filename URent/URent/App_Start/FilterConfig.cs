using System.Web;
using System.Web.Mvc;

namespace URent
{
    /// <summary>
    /// Creates and registers global MVC filters which are applied
    /// to all actions and controllers.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Global MVC filters to be registered.
        /// </summary>
        /// <param name="filters">The global MVC filters being registered.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
