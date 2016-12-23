using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VoterRegistrationMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              "SearchAfterFound", // Route name
              "{controller}/{action}/{PetitionID}/{PetitionDetailID}", // URL with 
              new { controller = "VoterSearch", action = "SearchAfterFound", PetitionID = UrlParameter.Optional, PetitionDetailID = UrlParameter.Optional } // Parameter defaults
            );

            //routes.MapRoute(
            //   "SearchPage", // Route name
            //   "{controller}/{action}/{id}", // URL with 
            //   new { controller = "VoterSearch", action = "Search", id = UrlParameter.Optional } // Parameter defaults
            // );

            routes.MapRoute(
                "SearchPageAAfter", // Route name
                "{controller}/{action}/{id}", // URL with 
                new { controller = "VoterSearch", action = "Search", PetitionID = UrlParameter.Optional, PetitionDetailID = UrlParameter.Optional } // Parameter defaults
              );

            routes.MapRoute(
                "AddVoter", // Route name
                "{controller}/{action}/{VoterID}/{PetitionID}/{PetitionDetailID}", // URL with 
                new { controller = "VoterSearch", action = "AddVoter", VoterID = UrlParameter.Optional, PetitionID = UrlParameter.Optional, PetitionDetailID = UrlParameter.Optional } // Parameter defaults
              );


            routes.MapRoute(
                "RemoveVoter", // Route name
                "{controller}/{action}/{VoterID}/{PetitionID}/{PetitionDetailID}", // URL with 
                new { controller = "PetitionSignatureSearch", action = "RemoveVoter", VoterID = UrlParameter.Optional, PetitionID = UrlParameter.Optional, PetitionDetailID = UrlParameter.Optional } // Parameter defaults
              );




        }
    }
}
