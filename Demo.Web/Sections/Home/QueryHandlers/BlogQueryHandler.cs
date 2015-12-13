using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Domain.Contracts;
using Demo.Web.Domain.Contracts.Queries;
using Demo.Web.Domain.Data.Extensions;
using Demo.Web.Domain.Data.Models;
using Demo.Web.Sections.Home.Models;
using Demo.Web.Sections.Home.Queries;

namespace Demo.Web.Sections.Home.QueryHandlers
{
    public class BlogQueryHandler : IAsyncQueryHandler<BlogQuery, BlogModel>
    {
        private IStorageContext<Blog> _storageContext;

        public BlogQueryHandler(IStorageContext<Blog> storageContext)
        {
            this._storageContext = storageContext;
        }

        public async Task<BlogModel> ExecuteAsync(BlogQuery query)
        {
            Blog blog;
            if (query.BlogID != null)
            {
                blog = await _storageContext.Entities.FindByIDAsync(query.BlogID.Value);
            }
            else
            {
                blog = await _storageContext.Entities.OrderBy(x => x.Name).FirstOrDefaultAsync();
            }

            if (blog == null)
            {
                return null;
            }

            BlogModel model = new BlogModel()
            {
                ID = blog.ID,
                Name = blog.Name,
                Posts = blog.Posts.OrderBy(x => x.WhenPosted).Select(x => new PostModel()
                {
                    ID = x.ID,
                    Title = x.Title
                }).ToList()
            };
            return model;
        }
    }
}
