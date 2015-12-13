using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Demo.Web.Domain.Data;
using Demo.Web.Domain.Data.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Test.Fluent.EntityTests
{
    [TestClass]
    public class BaseEntityTest
    {
        [TestInitialize]
        public void CleanDatabase()
        {
            using (StorageContext<Blog> dbContext = new StorageContext<Blog>())
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StorageContext<Blog>>());
                dbContext.Database.Initialize(force: true);

                IEnumerable<string> tableNames = new[]
                {
                    "Blog"
                };

                foreach (string tableName in tableNames)
                {
                    dbContext.Database.ExecuteSqlCommand("DELETE FROM [" + tableName + "];");
                }
            }
        }

        [TestMethod]
        public void AllEntityClassesShouldHaveDefaultEmptyConstructor()
        {
            var configurations =
                 from t in typeof(StorageContext<>).Assembly.GetExportedTypes()
                 let b = t.BaseType
                 where b != null && b.IsGenericType && b.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                 select b;

            configurations.Should().NotBeNullOrEmpty("doesn't help to verify entity classes if we can't find their configurations");

            var invalidTypes =
                from t in configurations
                from g in t.GetGenericArguments()
                let c = g.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null)
                where (c == null) || ((c.Attributes & MethodAttributes.Private) == MethodAttributes.Private)
                select g;

            invalidTypes.Should().BeNullOrEmpty("Entity Framework models need a public or protected parameterless constructor: " + string.Join(",", invalidTypes.Select(x => x.Name)));
        }
    }
}
