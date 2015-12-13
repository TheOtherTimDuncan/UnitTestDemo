using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Domain.Common;

namespace Demo.Web.Domain.Data.Models
{
    public class Blog
    {
        protected Blog()
        {
        }

        public Blog(string name)
        {
            ThrowIf.Argument.IsNullOrEmpty(name, "name");

            this.Name = name;
        }

        public int ID
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
