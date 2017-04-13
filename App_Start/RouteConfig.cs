﻿using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Charge",
               url: "Charge/Index/{year}/{month}",
               defaults: new { controller = "Charge", action = "Index", year = UrlParameter.Optional, month = UrlParameter.Optional }

           );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Charge", action = "Index", id = UrlParameter.Optional }

           );          
         
        }
    }
}