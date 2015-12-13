using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Demo.Test.Fluent.EntityTests
{
    public static class EntityTestHelper
    {
        private static int _entityID = 1;

        /// <summary>
        /// Sets the writable properties of the given object to unique values. Numeric properties and DateTime properties have incrementing values.
        /// String values will be set to the name of the properties right-padded with * up to the configured maximum length of the database column
        /// Properties configured as identity properties are automatically skipped
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="ignoreProperties">The names of the properties  to not fill with test data</param>
        public static void FillWithTestData<T>(this DbContext dbContext, T entity, params string[] ignoreProperties)
        {
            byte number = 1;
            Boolean testBoolean = false;
            DateTime testDate = new DateTime(2000, 1, 1, 1, 1, 1);
            Decimal testDecimal = 1.1m;
            Double? testDouble = 1.1f;
            TimeSpan testTimeSpan = TimeSpan.FromHours(1);

            Type entityType = typeof(T);

            List<string> ignores = new List<string>();

            if (ignoreProperties != null)
            {
                ignores.AddRange(ignoreProperties);
            }

            IEnumerable<string> identities = dbContext.GetIdentityPropertyNames<T>();
            if (identities != null)
            {
                ignores.AddRange(identities);
            }

            IEnumerable<PropertyInfo> properties =
              from p in entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
              where p.CanWrite && !ignores.Contains(p.Name)
              select p;

            foreach (PropertyInfo property in properties)
            {
                object value = null;
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (propertyType == typeof(string))
                {
                    string testString = property.Name;

                    EdmProperty edmProperty = GetEntityMetadataProperty(dbContext, entityType, property.Name);
                    if (edmProperty == null)
                    {
                        throw new Exception("Entity configuration not found for " + entityType.Name + "." + property.Name);
                    }

                    int maxLength;
                    if (edmProperty.IsMaxLength)
                    {
                        maxLength = 4000;
                    }
                    else
                    {
                        if (edmProperty.MaxLength == null)
                        {
                            throw new Exception("MaxLength not configured for " + entityType.Name + "." + property.Name);
                        }
                        maxLength = edmProperty.MaxLength.Value;
                    }

                    if (testString.Length > maxLength)
                    {
                        testString = testString.Substring(0, maxLength);
                    }
                    else
                    {
                        testString = testString + new string('*', maxLength - testString.Length);
                    }

                    value = testString;
                }
                else if (propertyType == typeof(DateTime))
                {
                    value = testDate;
                    testDate = testDate.AddYears(1).AddMonths(1).AddDays(1).AddHours(1).AddMinutes(1);
                }
                else if (propertyType == typeof(Int16))
                {
                    value = Convert.ToInt16(number);
                    number++;
                }
                else if (propertyType == typeof(Int32))
                {
                    value = Convert.ToInt32(number);
                    number++;
                }
                else if (propertyType == typeof(Int64))
                {
                    value = Convert.ToInt64(number);
                    number++;
                }
                else if (propertyType == typeof(Byte))
                {
                    value = number;
                    number++;
                }
                else if (propertyType == typeof(Boolean))
                {
                    value = testBoolean;
                    testBoolean = !testBoolean;
                }
                else if (propertyType == typeof(Decimal))
                {
                    value = testDecimal;
                    testDecimal *= 2;
                }
                else if (propertyType == typeof(Double))
                {
                    value = testDouble;
                    testDouble *= 2;
                }
                else if (propertyType == typeof(TimeSpan))
                {
                    value = testTimeSpan;
                    testTimeSpan = TimeSpan.FromHours(testTimeSpan.Hours + 1);
                }

                if (value != null)
                {
                    property.SetValue(entity, value, null);
                }
            }
        }

        public static IEnumerable<string> GetKeyPropertyNames<T>(this DbContext dbContext)
        {
            return dbContext.GetKeyPropertyNames(typeof(T));
        }

        public static IEnumerable<string> GetKeyPropertyNames(this DbContext dbContext, Type entityType)
        {
            MetadataWorkspace metadata = ((IObjectContextAdapter)dbContext).ObjectContext.MetadataWorkspace;

            IEnumerable<string> keyProperties = metadata.GetItems(DataSpace.OSpace)
                .Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                .OfType<EntityType>()
                .Where(x => x.Name == entityType.Name)
                .SelectMany(x => x.KeyProperties)
                .Select(x => x.Name)
                .Distinct()
                .ToList();

            return keyProperties;
        }

        public static IEnumerable<string> GetIdentityPropertyNames<T>(this DbContext dbContext)
        {
            return dbContext.GetIdentityPropertyNames(typeof(T));
        }

        public static IEnumerable<string> GetIdentityPropertyNames(this DbContext dbContext, Type entityType)
        {
            MetadataWorkspace metadata = ((IObjectContextAdapter)dbContext).ObjectContext.MetadataWorkspace;

            IEnumerable<string> identityProperties = metadata.GetItems(DataSpace.SSpace)
                .Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                .OfType<EntityType>()
                .Where(x => x.Name == entityType.Name)
                .SelectMany(x => x.Properties)
                .Where(x => x.TypeUsage.Facets.Any(f => f.Name == "StoreGeneratedPattern" && ((((StoreGeneratedPattern)f.Value) == StoreGeneratedPattern.Identity) || (((StoreGeneratedPattern)f.Value) == StoreGeneratedPattern.Computed))))
                .Select(x => x.Name)
                .Distinct()
                .ToList();

            return identityProperties;
        }

        public static EdmProperty GetEntityMetadataProperty<T>(this DbContext dbContext, Expression<Func<T, object>> selector)
        {
            LambdaExpression lambdaExpression = selector as LambdaExpression;
            if (lambdaExpression == null)
            {
                throw new Exception("Expression is not a lambda expression");
            }

            MemberExpression memberExpression;
            switch (lambdaExpression.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand;
                    break;

                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambdaExpression.Body;
                    break;

                default:
                    throw new Exception("Unknown expression type");
            }

            return GetEntityMetadataProperty(dbContext, typeof(T), memberExpression.Member.Name);
        }

        public static EdmProperty GetEntityMetadataProperty(this DbContext dbContext, Type entityType, string propertyName)
        {
            MetadataWorkspace metadata = ((IObjectContextAdapter)dbContext).ObjectContext.MetadataWorkspace;

            EdmProperty edmProperty = metadata
                .GetItems(DataSpace.CSpace)
                .Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                .OfType<EntityType>()
                .Where(x => x.Name == entityType.Name)
                .Single()
                    .DeclaredMembers
                    .OfType<EdmProperty>()
                    .Where(x => x.Name == propertyName)
                    .Single();

            return edmProperty;
        }

        public static T SetEntityID<T>(this T source, Expression<Func<T, object>> selector) where T : class
        {
            LambdaExpression lambdaExpression = selector as LambdaExpression;
            MemberExpression memberExpression = (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand;
            PropertyInfo property = (PropertyInfo)memberExpression.Member;
            property.SetValue(source, _entityID);

            _entityID++;

            return source;
        }
    }
}
