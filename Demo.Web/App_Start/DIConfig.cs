using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace Demo.Web
{
    public class DIConfig
    {
        public static Container ConfigureDependencyContainer(Assembly additionalAssembly = null)
        {
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            List<Assembly> defaultAssemblies = new List<Assembly>();
            defaultAssemblies.Add(typeof(DIConfig).Assembly);
            if (additionalAssembly != null)
            {
                defaultAssemblies.Add(additionalAssembly);
            }

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            return container;
        }
    }
}