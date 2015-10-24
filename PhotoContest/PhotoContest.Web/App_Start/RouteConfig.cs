namespace PhotoContest.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PicturesDetails",
                url: "Contests/{id}/Pictures/{pictureId}",
                defaults: new
                {
                    controller = "Pictures",
                    action = "Details"
                },
                constraints: new { pictureId = @"\d+" }
            );

            routes.MapRoute(
                name: "Pictures",
                url: "Contests/{id}/Pictures",
                defaults: new
                {
                    controller = "Pictures",
                    action = "Index"
                },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Details",
                url: "{controller}/{id}/",
                defaults: new
                {
                    controller = "Contests",
                    action = "Details"
                },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
