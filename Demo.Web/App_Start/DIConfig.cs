using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Demo.Web.Domain.Contracts;
using Demo.Web.Domain.Contracts.Commands;
using Demo.Web.Domain.Contracts.Queries;
using Demo.Web.Domain.Data;
using Demo.Web.Domain.Services.Dispatchers;
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

            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Scoped);
            container.Register<IQueryDispatcher, QueryDispatcher>(Lifestyle.Scoped);

            container.Register(typeof(IQueryHandler<,>), defaultAssemblies);
            container.Register(typeof(IAsyncQueryHandler<,>), defaultAssemblies);

            container.Register(typeof(ICommandHandler<>), defaultAssemblies);
            container.Register(typeof(IAsyncCommandHandler<>), defaultAssemblies);

            container.Register(typeof(IStorageContext<>), typeof(StorageContext<>), Lifestyle.Scoped);

            return container;
        }
    }
}