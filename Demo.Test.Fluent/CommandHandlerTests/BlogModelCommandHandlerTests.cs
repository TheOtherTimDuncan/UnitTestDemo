using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Test.Fluent.EntityTests;
using Demo.Test.Fluent.Mocks;
using Demo.Web.Domain.Data.Models;
using Demo.Web.Domain.Services.Commands;
using Demo.Web.Sections.Home.CommandHandlers;
using Demo.Web.Sections.Home.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Demo.Test.Fluent.CommandHandlerTests
{
    [TestClass]
    public class BlogModelCommandHandlerTests
    {
        [TestMethod]
        public async Task ReturnsFailedIfBlogNotFound()
        {
            MockStorageContext<Blog> mockBlogStorage = new MockStorageContext<Blog>(Enumerable.Empty<Blog>());

            BlogCommandHandler handler = new BlogCommandHandler(mockBlogStorage.Object);
            CommandResult commandResult = await handler.ExecuteAsync(new BlogModel()
            {
                ID = 1
            });
            commandResult.Succeeded.Should().BeFalse();
            commandResult.Errors.Should().HaveCount(1);
            commandResult.Errors.Single().Should().Be("Blog ID 1 not found");

            mockBlogStorage.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [TestMethod]
        public async Task UpdatesBlogAndReturnsSucceededIfBlogExists()
        {
            Blog blog = new Blog("old").SetEntityID(x => x.ID);

            MockStorageContext<Blog> mockBlogStorage = new MockStorageContext<Blog>(blog);

            BlogModel model = new BlogModel()
            {
                ID = blog.ID,
                Name = "new name"
            };

            BlogCommandHandler handler = new BlogCommandHandler(mockBlogStorage.Object);
            CommandResult commandResult = await handler.ExecuteAsync(model);
            commandResult.Succeeded.Should().BeTrue();

            blog.Name.Should().Be(model.Name);

            mockBlogStorage.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
