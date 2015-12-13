using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Web.Sections.Home.Models
{
    public class BlogModel
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<PostModel> Posts
        {
            get;
            set;
        }
    }
}
