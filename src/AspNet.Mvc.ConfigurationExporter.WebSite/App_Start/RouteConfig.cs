using System.Web.Mvc;
using System.Web.Routing;

namespace AspNet.Mvc.ConfigurationExporter.WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapConfigExporter();

            //Exporter.Instance.RegisterType<ITestConfiguration>(type => DependencyResolver.Current.GetService(type), BindingFlags.Public | BindingFlags.Instance);
            Exporter.Instance.RegisterType<ITestConfiguration>(type => (ITestConfiguration)new TestConfiguration());

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}