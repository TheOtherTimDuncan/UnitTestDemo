using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Demo.Web.Domain.Data.Models;

namespace Demo.Web.Domain.Data.Configurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            HasKey(x => new
            {
                x.ID,
                x.BlogID
            });

            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Title).HasMaxLength(100).IsRequired().IsUnicode();
        }
    }
}
