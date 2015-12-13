using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector.Integration.Web.Mvc;

namespace Demo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = DIConfig.ConfigureDependencyContainer();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngineConfig.ConfigureViewEngines(ViewEngines.Engines);
        }
    }
}
