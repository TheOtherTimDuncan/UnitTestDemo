using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Demo.Web.Domain.Contracts;
using EntityFramework.Testing.Moq;
using Moq;

namespace Demo.Test.Fluent.Mocks
{
    public class MockStorageContext<T>:Mock<IStorageContext<T>> where T :class
    {
        private MockDbSet<T> _dbset;

        public MockStorageContext()
        {
            _dbset = new MockDbSet<T>()
                .SetupAddAndRemove()
                .SetupLinq();

            this.Setup(x => x.Entities).Returns(_dbset.Object);
            this.Setup(x => x.GetAll()).Returns(() => _dbset.Object.ToList());
            this.Setup(x => x.GetAllAsync()).Returns(async () => await _dbset.Object.ToListAsync());
            this.Setup(x => x.Delete(It.IsAny<T>())).Callback((T entity) => _dbset.Object.Remove(entity));

            this.Setup(x => x.Add(It.IsAny<T>())).Callback((T entity) =>
            {
                _dbset.Object.Add(entity);

                if (OnAdd != null)
                {
                    OnAdd(entity);
                }
            });
        }

        public MockStorageContext(T entity)
            : this()
        {
            AddEntity(entity);
        }

        public MockStorageContext(params T[] entities)
            : this()
        {
            AddEntities(entities);
        }

        public MockStorageContext(IEnumerable<T> entities)
            : this()
        {
            AddEntities(entities);
        }

        public Action<T> OnAdd
        {
            get;
            set;
        }

        public void AddEntity(T entity)
        {
            AddEntities(new[] { entity });
        }

        public void AddEntities(IEnumerable<T> entities)
        {
            _dbset.SetupSeedData(entities);
        }
    }
}
