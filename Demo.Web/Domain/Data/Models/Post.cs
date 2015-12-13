using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Domain.Common;

namespace Demo.Web.Domain.Data.Models
{
    public class Post
    {
        protected Post()
        {
        }

        internal Post(Blog blog, string title)
        {
            ThrowIf.Argument.IsNull(blog, "blog");
            ThrowIf.Argument.IsNullOrEmpty(title, "title");

            this.Title = title;
            this.WhenPosted = DateTimeOffset.Now;

            this.Blog = blog;
            this.BlogID = blog.ID;
        }

        public int ID
        {
            get;
            private set;
        }

        public int BlogID
        {
            get;
            private set;
        }

        public virtual  Blog Blog
        {
            get;
            private set;
        }

        public DateTimeOffset WhenPosted
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
    }
}
