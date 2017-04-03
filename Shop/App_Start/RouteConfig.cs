using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
{
        public class RouteConfig
        {
            public static void RegisterRoutes(RouteCollection routes)
            {
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "ProductsList",
                    url: "Category/{nameCategory}",
                    defaults: new { controller = "Products", action = "List" }
                );


            routes.MapRoute(
                    name: "StaticSite",
                    url: "sites/{name}.html",
                    defaults: new { controller = "Home", action = "StaticSite" }
                );


                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }

