using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Domain.Common;
using Demo.Web.Domain.Contracts.Queries;
using SimpleInjector;

namespace Demo.Web.Domain.Services.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private Container _container;

        public QueryDispatcher(Container container)
        {
            ThrowIf.Argument.IsNull(container, "container");
            this._container = container;
        }

        public async Task<TResult> DispatchAsync<TResult>(IAsyncQuery<TResult> query)
        {
            Type handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _container.GetInstance(handlerType);
            return await handler.ExecuteAsync((dynamic)query);
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Execute((dynamic)query);
        }
    }
}
