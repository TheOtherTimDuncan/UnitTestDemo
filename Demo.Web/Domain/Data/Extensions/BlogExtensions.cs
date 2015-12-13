using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Domain.Data.Models;

namespace Demo.Web.Domain.Data.Extensions
{
    public static class BlogExtensions
    {
        public static IQueryable<Blog> MatchingID(this IQueryable<Blog> source, int blogID)
        {
            return source.Where(x => x.ID == blogID);
        }

        public async static Task<Blog> FindByIDAsync(this IQueryable<Blog> source, int blogID)
        {
            return await source.MatchingID(blogID).SingleOrDefaultAsync();
        }

        public static Blog FindByID(this IQueryable<Blog> source, int blogID)
        {
            return source.MatchingID(blogID).SingleOrDefault();
        }
    }
}
