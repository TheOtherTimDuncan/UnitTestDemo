using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Test.Fluent.EntityTests;
using Demo.Test.Fluent.Mocks;
using Demo.Web.Domain.Data.Models;
using Demo.Web.Sections.Home.Models;
using Demo.Web.Sections.Home.Queries;
using Demo.Web.Sections.Home.QueryHandlers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Test.Fluent.QueryHandlerTests
{
    [TestClass]
    public class BlogQueryHandlerTests
    {
        [TestMethod]
        public async Task ReturnsNullIfBlogNotFound()
        {
            MockStorageContext<Blog> mockBlogStorage = new MockStorageContext<Blog>(Enumerable.Empty<Blog>());

            BlogQueryHandler handler = new BlogQueryHandler(mockBlogStorage.Object);
            BlogModel model = await handler.ExecuteAsync(new BlogQuery()
            {
                BlogID = 1
            });
            model.Should().BeNull();
        }

        [TestMethod]
        public async Task ReturnsFirstFoundBlogAfterSortingByNameIfNoBlogIDRequested()
        {
            Blog blog1 = new Blog("zz-blog1");
            Blog blog2 = new Blog("aa-blog2");

            MockStorageContext<Blog> mockBlogStorage = new MockStorageContext<Blog>(blog1, blog2);

            BlogQueryHandler handler = new BlogQueryHandler(mockBlogStorage.Object);
            BlogModel model = await handler.ExecuteAsync(new BlogQuery());
            model.Should().NotBeNull();

            model.ID.Should().Be(blog2.ID, "it's the first by name");
            model.Name.Should().Be(blog2.Name);
        }

        [TestMethod]
        public async Task ReturnsRequestedBlog()
        {
            Blog blog = new Blog("blog").SetEntityID(x => x.ID);
            Post post = blog.AddPost("post").SetEntityID(x => x.ID);

            MockStorageContext<Blog> mockBlogStorage = new MockStorageContext<Blog>(blog);

            BlogQueryHandler handler = new BlogQueryHandler(mockBlogStorage.Object);
            BlogModel model = await handler.ExecuteAsync(new BlogQuery()
            {
                BlogID = blog.ID
            });
            model.Should().NotBeNull();

            model.ID.Should().Be(blog.ID);
            model.Name.Should().Be(blog.Name);

            model.Posts.Should().HaveCount(1);
            PostModel postModel = model.Posts.Single();
            postModel.ID.Should().Be(post.ID);
            postModel.Title.Should().Be(post.Title);
        }
    }
}
