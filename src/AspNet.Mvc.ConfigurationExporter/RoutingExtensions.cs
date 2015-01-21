using System.Web.Routing;

namespace AspNet.Mvc.ConfigurationExporter
{
    public static class RoutingExtensions
    {
        public static void MapConfigR(this RouteCollection routes)
        {
            routes.Add(new Route("configr", new ConfigrRouteHandler()));
        }
    }
}