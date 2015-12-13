using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Domain.Data;
using Demo.Web.Domain.Data.Extensions;
using Demo.Web.Domain.Data.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Test.Fluent.EntityTests
{
    [TestClass]
    public class BlogTests
    {
        private int? testBlogID;

        [TestCleanup]
        public void CleanupTest()
        {
            if (testBlogID != null)
            {
                using (StorageContext<Blog> storageContext = new StorageContext<Blog>())
                {
                    Blog blog = storageContext.Entities.FindByID(testBlogID.Value);
                    if (blog != null)
                    {
                        storageContext.Delete(blog);
                        storageContext.SaveChanges();
                    }
                }
            }
        }

        [TestMethod]
        public async Task CanReadWriteAndDeleteBlog()
        {
            Blog blog = new Blog("name");

            using (StorageContext<Blog> storageContext = new StorageContext<Blog>())
            {
                storageContext.FillWithTestData(blog);
                storageContext.Add(blog);
                await storageContext.SaveChangesAsync();
                testBlogID = blog.ID;
            }

            using (StorageContext<Blog> storageContext = new StorageContext<Blog>())
            {
                Blog result = await storageContext.Entities.FindByIDAsync(blog.ID);
                result.Should().NotBeNull();

                result.Name.Should().Be(blog.Name);

                storageContext.Delete(result);
                await storageContext.SaveChangesAsync();

                storageContext.Entities.FindByID(blog.ID).Should().BeNull();
            }
        }
    }
}
