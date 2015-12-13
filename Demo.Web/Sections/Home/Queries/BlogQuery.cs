using System;
using System.Collections.Generic;
using Demo.Web.Domain.Contracts.Queries;
using Demo.Web.Sections.Home.Models;

namespace Demo.Web.Sections.Home.Queries
{
    public class BlogQuery : IAsyncQuery<BlogModel>
    {
        public int? BlogID
        {
            get;
            set;
        }
    }
}
