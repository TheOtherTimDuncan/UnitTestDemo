using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Demo.Web.Domain.Contracts;
using Demo.Web.Domain.Data.Configurations;

namespace Demo.Web.Domain.Data
{
    [DbConfigurationType(typeof(EntityFrameworkConfiguration))]
    public class StorageContext<T> : DbContext, IStorageContext<T> where T : class
    {
        public StorageContext()
            : base("name=UnitTestDemo")
        {
            Database.SetInitializer<StorageContext<T>>(null);   

#if DEBUG
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(Database.Connection.ConnectionString);
                Database.Log = (string value) => Debug.WriteLine(value);
            }
            else
            {
                Console.WriteLine(Database.Connection.ConnectionString);
                Database.Log = Console.WriteLine;
            }
#endif
        }

        public virtual IDbSet<T> _Entities
        {
            get;
            set;
        }

        public IQueryable<T> Entities
        {
            get
            {
                return _Entities;
            }
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return _Entities.Where(predicate).SingleOrDefault();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _Entities.Where(predicate).SingleOrDefaultAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public virtual void Add(T entity)
        {
            _Entities.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _Entities.Remove(entity);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(BlogConfiguration).Assembly);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                int result = base.SaveChanges();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                throw new DbEntityValidationDetailException(ex);
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                int result = await base.SaveChangesAsync();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                throw new DbEntityValidationDetailException(ex);
            }
        }
    }
}
