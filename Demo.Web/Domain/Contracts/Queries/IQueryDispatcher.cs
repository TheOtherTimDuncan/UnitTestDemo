using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Web.Domain.Contracts.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IAsyncQuery<TResult> query);
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
