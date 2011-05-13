using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class Repository<TKey, TEntity> : IKeyedRepository<TKey, TEntity> where TEntity : class, IKeyed<TKey>
    {
        private readonly InMemoryDbTable<TKey, TEntity> _table;
        private readonly PrivateKeySetter _privateKeySetter = new PrivateKeySetter();

        public Repository(InMemoryDbTable<TKey, TEntity> table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            _table = table;
        }

        public bool Add(TEntity entity)
        {
            if(_privateKeySetter.IsKeyPrivate(entity))
            {
                // This entity has a private key. It is
                //  expected the repository layer set a
                //  unique key.
                //
                object key = null;
                Type idType;
                if (entity.Id != null)
                {
                    idType = entity.Id.GetType();
                }
                else
                {
                    // Forced to use reflection.
                    //
                    Type type = entity.GetType();
                    PropertyInfo propertyInfo = type.GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    idType = propertyInfo.PropertyType;
                }
                if (idType != null)
                {
                    if (idType == typeof (int))
                    {
                        // Increment count
                        //
                        key = Count(); // 0-based index
                    }
                    else if (idType == typeof (Guid))
                    {
                        key = Guid.NewGuid();
                    }
                    else if (idType == typeof (String))
                    {
                        key = Guid.NewGuid().ToString();
                    }
                }
                if(key == null)
                {
                    throw new NotSupportedException("Invalid key type: " + idType);
                }
                _privateKeySetter.SetKey(entity, key);
            }
            return _table.Add(entity);
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            bool result = true;
            foreach (TEntity entity in items)
            {
                result = Add(entity) && result;
            }
            return result;
        }

        public bool Update(TEntity entity)
        {
            return _table.Update(entity);
        }

        public bool Delete(TEntity entity)
        {
            return _table.Delete(entity.Id);
        }

        public bool Delete(IEnumerable<TEntity> entities)
        {
            bool result = true;
            foreach (TEntity entity in entities)
            {
                result = Delete(entity) && result;
            }
            return result;
        }

        public TEntity FindBy(TKey id)
        {
            return _table.FindBy(id);
        }

        public IQueryable<TEntity> All()
        {
            return _table.All();
        }

        public int Count()
        {
            return _table.Count();
        }
    }

}
