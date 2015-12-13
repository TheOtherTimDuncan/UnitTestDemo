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
    public class BlogTests : BaseEntityTest
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
        public void ThrowsExceptionIfCreatedWithNullName()
        {
            Action action = () => new Blog(null);
            action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("name");
        }

        [TestMethod]
        public void ThrowsExceptionIfCreatedWithEmptyName()
        {
            Action action = () => new Blog("");
            action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("name");
        }

        [TestMethod]
        public void ThrowsExceptionIfCreatePostWithNullName()
        {
            Blog blog = new Blog("blog");
            Action action = () => blog.AddPost(null);
            action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("title");
        }

        [TestMethod]
        public void ThrowsExceptionIfCreatePostWithEmptyName()
        {
            Blog blog = new Blog("blog");
            Action action = () => blog.AddPost("");
            action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("title");
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

        [TestMethod]
        public async Task CanReadWriteAndDeletePostAndLazyLoadPost()
        {
            Blog blog = new Blog("name");
            Post post = blog.AddPost("post");

            using (StorageContext<Blog> storageContext = new StorageContext<Blog>())
            {
                storageContext.FillWithTestData(post);
                storageContext.Add(blog);
                await storageContext.SaveChangesAsync();
                testBlogID = blog.ID;
            }

            using (StorageContext<Blog> storageContext = new StorageContext<Blog>())
            {
                Blog result = await storageContext.Entities.FindByIDAsync(blog.ID);
                result.Should().NotBeNull();

                result.Posts.Should().HaveCount(1);
                Post resultPost = result.Posts.Single();

                resultPost.Title.Should().Be(post.Title);
                resultPost.WhenPosted.Should().Be(post.WhenPosted);

                result.Posts.Remove(resultPost);
                Action action = () => storageContext.SaveChanges();
                action.ShouldNotThrow("removing child from parent should trigger deleting child instead of orphaning child");

                storageContext.Delete(result);
                await storageContext.SaveChangesAsync();

                storageContext.Entities.FindByID(blog.ID).Should().BeNull();
            }
        }
    }
}
