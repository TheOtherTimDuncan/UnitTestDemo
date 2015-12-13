using System;
using System.Collections.Generic;

namespace Demo.Web.Domain.Contracts.Queries
{
    public interface IQuery<out T>
    {
    }

    public interface IAsyncQuery<out T>
    {
    }
}
