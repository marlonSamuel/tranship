using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using NLog;

namespace TransShip {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            TransShip.LogBook.LogBook.TextLog = LogManager.GetLogger("TransShipLog");
            TransShip.LogBook.LogBook.EMail = LogManager.GetLogger("TransShipEMail");
            DependencyResolver.SetResolver(new TransShip.Infrastructure.Infrastructure());
        }
    }
}
