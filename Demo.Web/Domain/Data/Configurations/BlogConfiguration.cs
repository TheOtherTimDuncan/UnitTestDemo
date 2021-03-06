﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Demo.Web.Domain.Data.Models;

namespace Demo.Web.Domain.Data.Configurations
{
    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        public BlogConfiguration()
        {
            HasKey(x => x.ID);

            HasMany(x => x.Posts).WithRequired(x => x.Blog).HasForeignKey(x => x.BlogID).WillCascadeOnDelete();

            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(50).IsRequired().IsUnicode();
        }
    }
}
