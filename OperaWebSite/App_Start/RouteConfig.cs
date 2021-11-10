using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OperaWebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Ruta personalizada para Login
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{name}/{role}",
                defaults: new { controller = "Test", action = "Login" }
                );

            //Ruta personalizada para SearchByTitle    
            routes.MapRoute(
                name: "SearchByTitle",
                url: "{controller}/search/{title}",
                defaults: new { controller = "Test", action = "SearchByTitle" }
                );

            //Ruta default
            //La ruta default siempre va debajo de las personalizadas
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
